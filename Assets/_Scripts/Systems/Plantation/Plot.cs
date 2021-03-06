using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Enums;
using _Scripts.Singleton;
using _Scripts.Systems.Plants.Bases;
using Systems.Plantation;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Scripts.Systems.Plantation
{
    public class Plot : MonoBehaviour
    {
        public int PlotId;
        public SeedBase CurrentPlant;
        public PlantState PlantState = PlantState.Seed;
        public bool IsThirsty { get; private set; }
        public bool IsDead { get; private set; }
        public bool IsDestroyed { get;private set;}
        [SerializeField]
        private GameObject thirstyObj;
        [SerializeField]
        private GameObject deathObj;

        private void Start()
        {
            IsDestroyed = false;
            Debug.LogWarning(IsDestroyed);
        }
        public void ChangePlant(Plot id)
        {
            if (id.PlotId != this.PlotId) return;
            CurrentPlant = PlantEvents.CurrentPlant;
        }

        public bool GetIsDestroyed()
        {
            return IsDestroyed;
        }
        
        /// <summary>
        /// Create plant in the plot
        /// </summary>
        /// <param name="id"></param>
        public void Display(Plot id)
        {
            if (id.PlotId != this.PlotId) return;
            
            CurrentPlant = PlantEvents.CurrentPlant;
            if (!PlantTimeController.Instance.PlantTimer.ContainsKey(PlotId))
            {
                PlantTimeController.Instance.PlantTimer.Add(PlotId, new PlantPlot(CurrentPlant, CurrentPlant.GrowTime, 0));
                 StartCoroutine(PlantTimeController.Instance.Grow(this));
            }
            
            CreatePlant();
        }

        public bool CheckIfThirsty()
        {
            return IsThirsty;
        }

        public void ResetThirsty()
        {
            IsThirsty = false;
            StopCoroutine(PlantTimeController.Instance.Thirst(this));
            thirstyObj.SetActive(false);
            var p = new PlantPlot(CurrentPlant, PlantTimeController.Instance.PlantTimer[PlotId].Time, 0);
            PlantTimeController.Instance.PlantTimer[PlotId] = p;
           
            //StartCoroutine(PlantTimeController.Instance.Grow(this));
        }
        public void CreatePlant()
        {
            //Create plant obj
            if (transform.childCount > 0)
            {
                Destroy(transform.GetChild(0).gameObject);
            }
            
            CheckState();
            var newDisplay = Instantiate(CurrentPlant.PlantDisplayObjs[(int)PlantState],
                this.transform.position, Quaternion.identity);
            newDisplay.transform.parent = this.transform;
        }
        private void OnEnable()
        {
            PlantEvents.OnPlanted += Display;
            PlantEvents.OnHarvest += Harvest;
         
        }
        private void OnDestroy()
        {
            PlantEvents.OnPlanted -= Display;
            PlantEvents.OnHarvest -= Harvest;
            IsDestroyed = true;
        }

        public bool CheckAvailable()
        {
            return CurrentPlant == null;
        }

        private void CheckState()
        {
            if (PlantTimeController.Instance.PlantTimer[PlotId].ThristTime >= CurrentPlant.WaterCicles && !IsDead)
            {
                SetThirsty(true); 
            }
            if (PlantTimeController.Instance.PlantTimer[PlotId].ThristTime >= CurrentPlant.WaterCicles * 3)
            {
                SetDead(true);
            }
            if (PlantTimeController.Instance.PlantTimer[PlotId].Time <= CurrentPlant.GrowTime / 2 &&
                PlantState == PlantState.Seed)
            {
                SetState(PlantState.Growing);
            }
            if(PlantTimeController.Instance.PlantTimer[PlotId].Time >= CurrentPlant.GrowTime / 2)
            {
                 SetState(PlantState.Seed);
            }
            if (PlantTimeController.Instance.PlantTimer[PlotId].Time <= 0)
            {
                SetState(PlantState.Ready);
            }
          
        }
        
        public void SetState(PlantState state)
        {
            PlantState = state;
        }

        public void SetThirsty(bool value)
        {
            IsThirsty = value;
            if (thirstyObj == null) return;
            thirstyObj.SetActive(value);
        }
        public void SetDead(bool value)
        {
            IsDead = value;
            if (thirstyObj == null) return;
            thirstyObj.SetActive(false);
            deathObj.SetActive(value);
        }

        public bool CheckIfReady()
        {
            return PlantState == PlantState.Ready;
        }

        private void Harvest(Plot id)
        {
            if (id.PlotId != this.PlotId) return;
            if (transform.childCount <= 0) return;
            
            IsThirsty = false;
            thirstyObj.SetActive(false);
            deathObj.SetActive(false);
            Destroy(transform.GetChild(0).gameObject);
            PlantEvents.PlantCollected = id.CurrentPlant.PlantBase;
            StopCoroutine(PlantTimeController.Instance.Thirst(this));
            PlantTimeController.Instance.ClearSlot(PlotId);
            StartCoroutine(ClearPlot());

            if (!IsDead)
            {
                PlantEvents.OnLabInventoryAction(1);
            }
            
        }
        
        /// <summary>
        /// Clear the current plant in plot
        /// </summary>
        /// <returns></returns>
        private IEnumerator ClearPlot()
        {
            yield return new WaitForSeconds(0.1f);
            CurrentPlant = null;
            PlantState = PlantState.Seed;
        }
    }
}
