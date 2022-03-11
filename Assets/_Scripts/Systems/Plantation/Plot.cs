using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Enums;
using _Scripts.Singleton;
using _Scripts.Systems.Plants.Bases;
using Systems.Plantation;
using UnityEngine;

namespace _Scripts.Systems.Plantation
{
    public class Plot : MonoBehaviour
    {
        public int PlotId;
        private SeedBase currentPlant;
        public PlantState PlantState { get; private set; }  = PlantState.Seed;
        public void ChangePlant(Plot id)
        {
            if (id.PlotId != this.PlotId) return;
            currentPlant = PlantEvents.CurrentPlant;
        }
        
        /// <summary>
        /// Create plant in the plot
        /// </summary>
        /// <param name="id"></param>
        public void Display(Plot id)
        {
            if (id.PlotId != this.PlotId) return;
            
            currentPlant = PlantEvents.CurrentPlant;
            if (!PlantTimeController.Instance.PlantTimer.ContainsKey(PlotId))
            {
                PlantTimeController.Instance.PlantTimer.Add(PlotId, new PlantPlot(currentPlant, currentPlant.GrowTime));
            }
         
            StartCoroutine(Grow());
            
           CreatePlant();
        }

        private void CreatePlant()
        {
            //Create plant obj
            if (transform.childCount > 0)
            {
                Destroy(transform.GetChild(0).gameObject);
            }

            if(PlantTimeController.Instance.PlantTimer[PlotId].Time <= currentPlant.GrowTime / 2 &&
                PlantState == PlantState.Seed)
            {
                SetState();
            }

            CheckState();
            var newDisplay = Instantiate(currentPlant.PlantDisplayObjs[(int)PlantState],
                this.transform.position, Quaternion.identity);
            newDisplay.transform.parent = this.transform;
        }
        private void OnEnable()
        {
            PlantEvents.OnPlanted += Display;
            PlantEvents.OnHarvest += Harvest;
        }
        private void OnDisable()
        {
            PlantEvents.OnPlanted -= Display;
            PlantEvents.OnHarvest -= Harvest;
        }

        public bool CheckAvailable()
        {
            return currentPlant == null;
        }

        private IEnumerator Grow()
        {
            yield return new WaitForSeconds(1f);
            var aux = PlantTimeController.Instance.PlantTimer[PlotId].Time;
            aux -= 1;
            var p = new PlantPlot(currentPlant, aux);
            PlantTimeController.Instance.PlantTimer[PlotId] = p;
            if (PlantTimeController.Instance.PlantTimer[PlotId].Time <= currentPlant.GrowTime / 2 && PlantState == PlantState.Seed)
            {
                SetState();
                CreatePlant();
                
            }
            if (PlantTimeController.Instance.PlantTimer[PlotId].Time <= 0)
            {
                SetState();
                CreatePlant();
                PlantTimeController.Instance.ClearSlot(PlotId);
            }
            else
            {
                StartCoroutine(Grow());
            }

        }

        private void CheckState()
        {
            if (PlantTimeController.Instance.PlantTimer[PlotId].Time <= currentPlant.GrowTime / 2 &&
                PlantState == PlantState.Seed)
            {
                SetState();
            }

            if (!(PlantTimeController.Instance.PlantTimer[PlotId].Time <= 0)) return;
            SetState();
            PlantTimeController.Instance.ClearSlot(PlotId);
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
            PlantEvents.PlantCollected = id.currentPlant.PlantBase;
            PlantEvents.OnLabInventoryAction(1);
            StartCoroutine(ClearPlot());
        }

        private IEnumerator ClearPlot()
        {
            yield return new WaitForSeconds(0.1f);
            currentPlant = null;
            PlantState = PlantState.Seed;
        }
    }
}
