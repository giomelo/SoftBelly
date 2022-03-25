using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections;
using System.Linq;
using _Scripts.Enums;
using _Scripts.Systems.Item;
using _Scripts.Systems.Plantation;
using _Scripts.Systems.Plants.Bases;
using Systems.Plantation;
using Unity.VisualScripting;

namespace _Scripts.Singleton
{
    public class PlantTimeController : MonoSingleton<PlantTimeController>
    {
        public Dictionary<int, PlantPlot> PlantTimer { get; } = new Dictionary<int,PlantPlot>();

        public void AddTime(int plotId, float time, ItemBehaviour plant, float thirstTime)
        {
            PlantTimer.Add(plotId, new PlantPlot(plant, time, thirstTime));
        }

        public void ClearSlot(int id)
        {
            PlantTimer.Remove(id);
        }

        private void Start()
        {
            DontDestroyOnLoad(this.gameObject);
        }

        public void CreatPlants()
        {
            if (PlantTimer.Count == 0)  return;
            for (int i = 0; i < PlantTimer.Count; i++)
            {
                foreach (var plot in GridSystem.Instance.Plots.Where(t => PlantTimer.ElementAt(i).Key == t.PlotId))
                {
                    PlantEvents.CurrentPlant = (SeedBase) PlantTimer.ElementAt(i).Value.Plant;
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
            var aux = PlantTimer[plot.PlotId].Time;
            aux -= 1;
            var p = new PlantPlot(plot.CurrentPlant, aux, 0);
            PlantTimer[plot.PlotId] = p;
            Debug.Log("Grow");
            //thrist cicle 
            if (PlantTimer[plot.PlotId].Time <= plot.CurrentPlant.GrowTime /plot.CurrentPlant.WaterCicles  || PlantTimer[plot.PlotId].Time <= 0)
            {
                plot.SetThirsty(true);
                StartCoroutine(Thirst(plot));
                yield break;
            }
            
            if (PlantTimer[plot.PlotId].Time <= plot.CurrentPlant.GrowTime / 2 && plot.PlantState == PlantState.Seed)
            {
                if (!plot.IsDestroyed)
                {
                    plot.SetState(PlantState.Growing);
                    plot.CreatePlant();
                }
            }
            if (PlantTimer[plot.PlotId].Time <= 0)
            {
                if (plot.IsDestroyed) yield break;
                plot.SetState(PlantState.Ready);
                plot.CreatePlant();
            }
            else
            {
                StartCoroutine(Grow(plot));
            }

        }

        public IEnumerator Thirst(Plot plot)
        {
            yield return new WaitForSeconds(1f);
            Debug.Log("Thirst");
            var aux = PlantTimer[plot.PlotId].Time;
            var auxThirsty = PlantTimer[plot.PlotId].ThristTime;
            auxThirsty += 1;
            var p = new PlantPlot(plot.CurrentPlant, aux, auxThirsty);
            PlantTimer[plot.PlotId] = p;
          
            Debug.Log(PlantTimer[plot.PlotId].ThristTime); 
            if (plot.IsThirsty)
            {
                if (PlantTimer[plot.PlotId].ThristTime >= plot.CurrentPlant.GrowTime + plot.CurrentPlant.GrowTime/plot.CurrentPlant.WaterCicles)
                {
                    if (plot.IsDestroyed) yield break;
                    plot.SetDead(true);
                    yield break;
                }
                StartCoroutine(Thirst(plot));
            }
            else
            {
                StartCoroutine(Grow(plot));
            }
        }
    }
}