using System;
using _Scripts.Enums;
using _Scripts.Singleton;
using _Scripts.Systems.Lab;
using _Scripts.Systems.Lab.Machines;
using _Scripts.Systems.Lab.Machines.Base;
using _Scripts.Systems.Patients;
using _Scripts.Systems.Plantation;
using _Scripts.Systems.Plants.Bases;
using _Scripts.U_Variables;
using _Scripts.UI;
using UnityEngine;

namespace _Scripts.Entities.Player
{
    /// <summary>
    /// Handles the user inputs and send to other classes
    /// </summary>
    public class PlayerInputHandler : MonoBehaviour
    {
        #region Fields
    
        [Header("Movement")]
        [SerializeField]
        private PlayerMovement playerMovement;

        [Header("PlantInputs")]

        private const int CollisionLayer = 1 << 6;

        private const float Radius = 7f;

        [Header("Water Inputs")] 
        private bool _isHolding;

        private const float DistanceWaterCan = 3f;

        [SerializeField]
        private Transform waterCanObj;
        [SerializeField]
        private Transform waterCanPlace;
        private static bool disableInput;
        #endregion

        public static void DisableInputCall()
        {
            disableInput = true;
            Debug.LogWarning("disble");
        }
        public static void EnableInputCall()
        {
            disableInput = false;
            Debug.LogWarning("enable");
        }

        private void Start()
        {
            InvokeRepeating(nameof(PutWaterInput), 1,0.1f);
            InvokeRepeating(nameof(PlantInput), 1,0.05f); 
        }
        // Update is called once per frame
        private void Update()
        {
            if(!disableInput)
                 HandleMovementInput();
            WaterInput();
        }
    
        /// <summary>
        /// Get the user inputs and send to movement script
        /// </summary>
        private void HandleMovementInput()
        {
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");

            if (horizontal == 0 && vertical == 0) return;
            playerMovement.ProcessInput(horizontal, vertical);
        }
    
        /// <summary>
        /// Check if the mouse is down to plant a seed in the plot
        /// </summary>
        private void PlantInput()
        {
            
            if (!Input.GetMouseButton(0)) return;
            Ray direction = GameManager.Instance.MainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
        
            if (!Physics.Raycast(GameManager.Instance.MainCamera.transform.position, direction.direction, out hit,5000, CollisionLayer)) return;
            Debug.Log("Ray");
            if (!CheckDistanceFromPlayer(hit.transform, Radius)) return;
 
                switch (hit.transform.tag)
                {
                    case "Plot":
                        
                        if (hit.transform.TryGetComponent<Plot>(out var plotScript))
                        {
                            if (!plotScript.IsLocked)
                            {
                                if (plotScript.CheckAvailable())
                                {
                                    //Inventory id
                                    PlantEvents.OnPlotSelected(0);
                                    PlantEvents.CurrentPlot = plotScript;
                                }
                                else
                                {
                                    //Check if the plant is ready to harvest
                                    if(!plotScript.CheckIfReady() && !plotScript.IsDead) return;
                                    PlantEvents.OnHarvestCall(plotScript);
                                }
                            }
                            else
                            {
                                //checar se tem o dinheiro
                                if (UniversalVariables.Instance.Money > plotScript.PriceToUnlock)
                                {
                                    // se tem comprar
                                    HUD_Controller.Instance.ShowBuyPopup(PlantEvents.OnbuyConfirm, plotScript.PlotId, true);
                                    
                                }

                            }
                        }
                        break;
                    case "Chest":
                        if (LabEvents.IsOnMachine) return;
                        LabEvents.OnChestSelectedCall(1);
                        break;
                    case "Machine":
                        if (LabEvents.IsOnMachine) return;
                        if (!hit.transform.TryGetComponent<MachineHolder>(out var machineScript)) return;
                        if (machineScript.CurrentMachine.IsLocked)
                        {
                            //checar se tem o dinheiro
                            if (UniversalVariables.Instance.Money > machineScript.CurrentMachine.PriceToUnlock)
                            {
                                // se tem comprar
                                HUD_Controller.Instance.ShowBuyPopup(PlantEvents.OnbuyConfirm, machineScript.CurrentMachine.MachineId, false);
                                    
                            }
                        }
                        else
                        {
                            if (LabEvents.CurrentMachine == machineScript.CurrentMachine) return;
                            if (machineScript.CurrentMachine.MachineState == MachineState.Working) return;
                            if (machineScript.CurrentMachine.IsLocked) return;
                            LabEvents.OnMachineSelectedCall(machineScript.CurrentMachine);
                           
                        }
                        break;
                    case "Patient":
                        Debug.Log("oi");
                        if (!hit.transform.TryGetComponent<Patient>(out var patientScript)) return;
                        
                        PatientsEvents.OnOrderDeliveredCall(patientScript);
                        break;
                    case "Bed":
                        GameManager.Instance.SleepCall();
                        break;
                    // case "Book":
                    //     if (!hit.transform.TryGetComponent<Book>(out var bookScript)) return;
                    //     bookScript.OpenBook();
                    //     break;
                }
                
            }

        /// <summary>
        /// Check if plant input is pressed for puting water
        /// </summary>
        private void PutWaterInput()
        {
            if (!Input.GetMouseButton(1)) return;
            if (!_isHolding) return;
            Ray direction = GameManager.Instance.MainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
        
            if (!Physics.Raycast(GameManager.Instance.MainCamera.transform.position, direction.direction, out hit,5000, CollisionLayer)) return;
            if (!CheckDistanceFromPlayer(hit.transform, Radius)) return;
            if (!hit.transform.TryGetComponent<Plot>(out var plotScript)) return;
            if (plotScript.CheckAvailable()) return;
            if (!plotScript.CheckIfThirsty()) return;

            plotScript.ResetThirsty();

        }
        
        /// <summary>
        /// Check if the input key for getting the water can and water plants
        /// </summary>
        private void WaterInput()
        {
            if (!Input.GetKeyDown(KeyCode.E)) return;
            if (_isHolding)
            {
                PutWaterCan(false);
            }else
            {
               
                if (!CheckDistanceFromPlayer(waterCanObj, DistanceWaterCan)) return;
                PutWaterCan(true);
            }
        }

        private void PutWaterCan(bool option)
        {
            if (option)
            {
                _isHolding = true;
                waterCanObj.SetParent(waterCanPlace);
                waterCanObj.position = waterCanPlace.position;
                waterCanObj.GetComponent<Rigidbody>().isKinematic = true;
            }
            else
            {
                _isHolding = false;
                waterCanObj.SetParent(null);
                waterCanObj.GetComponent<Rigidbody>().isKinematic = false;
            }
          
        }
        private bool CheckDistanceFromPlayer(Transform obj, float radius)
        {
            return Vector3.Distance(obj.position ,transform.position) <= radius;
        }
    }
}
