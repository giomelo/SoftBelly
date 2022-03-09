using System.Collections;
using System.Collections.Generic;
using _Scripts.Enums;
using _Scripts.Systems.Plants.Bases;
using Systems.Plantation;
using UnityEngine;

namespace _Scripts.Systems.Plantation
{
    public class Plot : MonoBehaviour
    {
        public int PlotId;
        private SeedBase currentPlant;
        [SerializeField]
        private float currentGrowthTime;
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
        private void Display(Plot id)
        {
            if (id.PlotId != this.PlotId) return;
            
            currentPlant = PlantEvents.CurrentPlant;
            currentGrowthTime = currentPlant.GrowTime;
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
            var newDisplay = Instantiate(currentPlant.PlantDisplayObjs[(int)PlantState],
                this.transform.position, Quaternion.identity);
            newDisplay.transform.parent = this.transform;
        }
        private void OnEnable()
        {
            PlotId = GridSystem.PlotsId++;
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
            currentGrowthTime -= 1;
            if (currentGrowthTime <= currentPlant.GrowTime / 2 && PlantState == PlantState.Seed)
            {
                SetState();
                CreatePlant();
                
            }
            if (currentGrowthTime <= 0)
            {
                SetState();
                CreatePlant();
            }
            else
            {
                StartCoroutine(Grow());
            }

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
