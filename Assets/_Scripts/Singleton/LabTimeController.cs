using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Enums;
using _Scripts.Systems.Item;
using _Scripts.Systems.Lab.Machines;
using _Scripts.Systems.Plantation;
using _Scripts.Systems.Plants.Bases;
using Systems.Plantation;
using UnityEngine;

namespace _Scripts.Singleton
{
    /// <summary>
    /// Timer controller for machines that need a timer
    /// </summary>
    public class LabTimeController : MonoSingleton<LabTimeController>
    {
        public Dictionary<int, PlantPlot> LabTimer { get; } = new Dictionary<int,PlantPlot>();
        public static int MachinesId = 0;
        [SerializeField]
        public List<MachineHolder> allMachines = new List<MachineHolder>();
        public void AddTime(int plotId, float time, ItemBehaviour plant)
        {
            LabTimer.Add(plotId, new PlantPlot(plant, time));
        }

        public void ClearSlot(int id)
        {
            LabTimer.Remove(id);
        }

        private void Start()
        {
            DontDestroyOnLoad(this.gameObject);
            MachinesId = 0;
            foreach (var machine in allMachines)
            {
                machine.MachineId = MachinesId;
                MachinesId++;
            }
        }
        

        public void CreatPlants()
        {
            if (LabTimer.Count == 0)  return;
            for (int i = 0; i < LabTimer.Count; i++)
            {
                foreach (var plot in GridSystem.Instance.Plots.Where(t => LabTimer.ElementAt(i).Key == t.PlotId))
                {
                    PlantEvents.CurrentPlant = (SeedBase) LabTimer.ElementAt(i).Value.Plant;
                    plot.Display(plot);
                }
            }
        }

        /// <summary>
        /// Grow plant in plot
        /// </summary>
        /// <param name="plot"></param>
        /// <returns></returns>
        public IEnumerator Grow(Plot plot)
        {
            yield return new WaitForSeconds(1f);
            var aux = LabTimer[plot.PlotId].Time;
            aux -= 1;
            var p = new PlantPlot(plot.CurrentPlant, aux);
            LabTimer[plot.PlotId] = p;

            if (LabTimer[plot.PlotId].Time <= plot.CurrentPlant.GrowTime / 2 && plot.PlantState == PlantState.Seed)
            {
                if (!plot.IsDestroyed)
                {
                    plot.SetState();
                    plot.CreatePlant();
                }
            }

            if (LabTimer[plot.PlotId].Time <= 0)
            {
                if (plot.IsDestroyed) yield break;
                plot.SetState();
                plot.CreatePlant();
            }
            else
            {
                StartCoroutine(Grow(plot));
            }
        }
    }
}