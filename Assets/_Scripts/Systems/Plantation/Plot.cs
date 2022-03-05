using _Scripts.Systems.Plants.Bases;
using Systems.Plantation;
using UnityEngine;

namespace _Scripts.Systems.Plantation
{
    public class Plot : MonoBehaviour
    {
        public int PlotId;
        public PlantBase currentPlant;

        public void ChangePlant(PlantBase plant)
        {
            currentPlant = plant;
        }
        
        /// <summary>
        /// Create plant in the plot
        /// </summary>
        /// <param name="id"></param>
        private void Display(Plot id)
        {
            if (id.PlotId != this.PlotId) return;
            currentPlant = PlantEvents.CurrentPlant;
            var newDisplay = Instantiate(currentPlant.PlantDisplayObjs[(int)currentPlant.PlantState],
                this.transform.position, Quaternion.identity);
            newDisplay.transform.parent = this.transform;
        }
        private void OnEnable()
        {
            PlotId = GridSystem.PlotsId++;
            PlantEvents.OnPlanted += Display;
        }

        private void OnDisable()
        {
            PlantEvents.OnPlanted -= Display;
        }

        public bool CheckAvailable()
        {
            return currentPlant == null;
        }
    }
}
