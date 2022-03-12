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
        public bool IsDestroyed { get; private set; }

        public void ChangePlant(Plot id)
        {
            if (id.PlotId != this.PlotId) return;
            CurrentPlant = PlantEvents.CurrentPlant;
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
                PlantTimeController.Instance.PlantTimer.Add(PlotId, new PlantPlot(CurrentPlant, CurrentPlant.GrowTime));
            }
         
            StartCoroutine(PlantTimeController.Instance.Grow(this));
            
           CreatePlant();
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
            IsDestroyed = false;
        }
        private void OnDisable()
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
            if (PlantTimeController.Instance.PlantTimer[PlotId].Time <= CurrentPlant.GrowTime / 2 &&
                PlantState == PlantState.Seed)
            {
                SetState();
            }

            if (!(PlantTimeController.Instance.PlantTimer[PlotId].Time <= 0)) return;
            SetState();
        }
        
        public void SetState()
        {
            switch (PlantState)
            {
                case PlantState.Seed:
                    PlantState = PlantState.Growing;
                    break;
                case PlantState.Growing:
                    PlantState = PlantState.Ready;
                    break;
            }
        }

        public bool CheckIfReady()
        {
            return PlantState == PlantState.Ready;
        }

        private void Harvest(Plot id)
        {
            if (id.PlotId != this.PlotId) return;
            if (transform.childCount <= 0) return;
            
            Destroy(transform.GetChild(0).gameObject);
            PlantEvents.PlantCollected = id.CurrentPlant.PlantBase;
            PlantEvents.OnLabInventoryAction(1);
            StartCoroutine(ClearPlot());
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
