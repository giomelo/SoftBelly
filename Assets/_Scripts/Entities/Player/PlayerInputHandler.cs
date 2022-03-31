using _Scripts.Enums;
using _Scripts.Singleton;
using _Scripts.Systems.Lab;
using _Scripts.Systems.Lab.Machines;
using _Scripts.Systems.Lab.Machines.Base;
using _Scripts.Systems.Plantation;
using _Scripts.Systems.Plants.Bases;
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


        private readonly float _radius = 7f;

        [Header("Water Inputs")] 
        private bool _isHolding;

        private readonly float _distanceWaterCan = 3f;
        
        [SerializeField]
        private Transform waterCanObj;
        [SerializeField]
        private Transform waterCanPlace;
        

        #endregion

        private void Start()
        {
            InvokeRepeating(nameof(PutWaterInput), 1,0.1f); 
           
            InvokeRepeating(nameof(PlantInput), 1,0.1f); 
        }
        // Update is called once per frame
        private void Update()
        {
            HandleMovementInput();
            WaterInput();
        }
    
        /// <summary>
        /// Get the user inputs and send to movement script
        /// </summary>
        private void HandleMovementInput()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

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
            if (!CheckDistanceFromPlayer(hit.transform, _radius)) return;
            if (hit.transform.TryGetComponent<Plot>(out var plotScript))
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
                switch (hit.transform.tag)
                {
                    case "Chest":
                        LabEvents.OnChestSelectedCall(1);
                        break;
                    case "Machine":
                        if (!hit.transform.TryGetComponent<MachineHolder>(out var machineScript)) return;
                        if (machineScript.CurrentMachine.MachineState == MachineState.Working) return;
                        LabEvents.OnMachineSelectedCall(machineScript.CurrentMachine);
                        break;
                }
                
            }
            
        }

        private void PutWaterInput()
        {
            if (!Input.GetMouseButton(1)) return;
            Ray direction = GameManager.Instance.MainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
        
            if (!Physics.Raycast(GameManager.Instance.MainCamera.transform.position, direction.direction, out hit,5000, CollisionLayer)) return;
            if (!CheckDistanceFromPlayer(hit.transform, _radius)) return;
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
               
                if (!CheckDistanceFromPlayer(waterCanObj, _distanceWaterCan)) return;
               
                PutWaterCan(true);
            }
        }

        private void PutWaterCan(bool option)
        {
            if (option)
            {
                
                _isHolding = true;
                Debug.Log( _isHolding);
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
