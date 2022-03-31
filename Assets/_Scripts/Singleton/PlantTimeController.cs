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
            var auxThirsty = PlantTimer[plot.PlotId].ThristTime;
            aux -= 1;
            auxThirsty += 1;
            var p = new PlantPlot(plot.CurrentPlant, aux, auxThirsty);
            PlantTimer[plot.PlotId] = p;
            Debug.LogWarning("Grow");
            //thrist cicle 
            Debug.Log("Thirst time: " + PlantTimer[plot.PlotId].ThristTime);
            if (PlantTimer[plot.PlotId].ThristTime >= plot.CurrentPlant.WaterCicles)
            {  
                auxThirsty = 0;
                p = new PlantPlot(plot.CurrentPlant, aux, auxThirsty);
                PlantTimer[plot.PlotId] = p;
                if (GridSystem.Instance != null)
                {
                    var scenePlot = GridSystem.Instance.Plots.Where(p => p.PlotId == plot.PlotId);
                    foreach(var plotAux in scenePlot)
                    {
                        plotAux.SetThirsty(true);
                    }
                }else{
                    plot.IsThirsty = true;
                }
                
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
                plot.SetState(PlantState.Ready);
                if (!plot.IsDestroyed)
                {   
                    plot.CreatePlant();
                }
                //StartCoroutine(Grow(plot));
            
            }
            
            StartCoroutine(Grow(plot));

        }
        public IEnumerator Thirst(Plot plot)
        {
            yield return new WaitForSeconds(1f);
            Debug.LogWarning("Thist");
            IEnumerable<Plot> scenePlot = null;
            Plot auxPlot = null;
            //plot.SetThirsty(true);
            if (GridSystem.Instance != null)
            {
                scenePlot = GridSystem.Instance.Plots.Where(p => p.PlotId == plot.PlotId);
                foreach(Plot plotAux in scenePlot)
                {
                    auxPlot = plotAux;
                    plot = auxPlot;
                }
            }
            if (!PlantTimer.ContainsKey(plot.PlotId))yield break;
            var aux = PlantTimer[plot.PlotId].Time;
            var auxThirsty = PlantTimer[plot.PlotId].ThristTime;
            auxThirsty += 1;
            var p = new PlantPlot(plot.CurrentPlant, aux, auxThirsty);
            PlantTimer[plot.PlotId] = p;
          
            if (scenePlot != null)
            {
                if (auxPlot.IsThirsty)
                {
                    if (PlantTimer[plot.PlotId].ThristTime >= plot.CurrentPlant.WaterCicles * 3)
                    {
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
            else
            {
                if (plot.IsThirsty)
                {
                    if (PlantTimer[plot.PlotId].ThristTime >= plot.CurrentPlant.WaterCicles * 3)
                    {
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

        private void SetDeadPlot(Plot plot)
        {
            
        }
    }
}