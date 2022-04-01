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
    [Serializable]
    public struct ExposedDic
    {
        public int key;
        public PlantPlot value;
    }

    public class PlantTimeController : MonoSingleton<PlantTimeController>
    {
        public Dictionary<int, PlantPlot> PlantTimer { get; } = new Dictionary<int,PlantPlot>();
        public List<ExposedDic> dic = new List<ExposedDic>();
        public void AddTime(int plotId, float time, SeedBase plant, float thirstTime, bool isThirsty)
        {
            PlantTimer.Add(plotId, new PlantPlot(plant, time, thirstTime, isThirsty));
        }

        public void ClearSlot(int id)
        {
            PlantTimer.Remove(id);
        }

        private void Start()
        {
            DontDestroyOnLoad(this.gameObject);
            InvokeRepeating("SetExpoesed", 1, 0.5f);
        }

        private void SetExpoesed()
        {
            dic.Clear();
            for (int i = 0; i < PlantTimer.Count; i++)
            {
                ExposedDic aux = new ExposedDic();
                aux.key = PlantTimer.ElementAt(i).Key;
                aux.value = PlantTimer.ElementAt(i).Value;

                dic.Add(aux);
            }
        }

        public void CreatPlants()
        {
            if (PlantTimer.Count == 0)  return;
            for (int i = 0; i < PlantTimer.Count; i++)
            {
                foreach (var plot in GridSystem.Instance.Plots.Where(t => PlantTimer.ElementAt(i).Key == t.PlotId))
                {
                    // if (PlantTimer[plot.PlotId].ThristTime >= PlantTimer[plot.PlotId].Plant.WaterCicles)
                    // {
                    //     plot.SetThirsty(true);
                    // }

                    PlantEvents.CurrentPlant = PlantTimer.ElementAt(i).Value.Plant;
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
            
            IEnumerable<Plot> scenePlot = null;
            //plot.SetThirsty(true);
            if (GridSystem.Instance != null)
            {
                scenePlot = GridSystem.Instance.Plots.Where(p => p.PlotId == plot.PlotId);
                foreach(Plot plotAux in scenePlot)
                {
                    plot = plotAux;
                }
            }
            
            var aux = PlantTimer[plot.PlotId].Time;
            var auxThirsty = PlantTimer[plot.PlotId].ThristTime;
            aux -= 1;
            auxThirsty += 1;
            Debug.Log("Sede :" + auxThirsty);
            Debug.Log("Plot ID :" + plot.PlotId);
            var p = new PlantPlot(plot.CurrentPlant, aux, auxThirsty, false);
            PlantTimer[plot.PlotId] = p;
            Debug.LogWarning("Grow");
            
            
            //thrist cicle 
            Debug.Log("Thirst time: " + PlantTimer[plot.PlotId].ThristTime);
            if (PlantTimer[plot.PlotId].ThristTime >= plot.CurrentPlant.WaterCicles && !plot.IsThirsty)
            {  
                auxThirsty = 0;
                p = new PlantPlot(plot.CurrentPlant, aux, auxThirsty, true);
                PlantTimer[plot.PlotId] = p;
                if (GridSystem.Instance != null)
                {
                    scenePlot = GridSystem.Instance.Plots.Where(p => p.PlotId == plot.PlotId);
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
            Debug.LogWarning("Thist");
            var p = new PlantPlot(plot.CurrentPlant, aux, auxThirsty, true);
            PlantTimer[plot.PlotId] = p;
        
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
                // else
            // {
            //     if (plot.IsThirsty)
            //     {
            //         if (PlantTimer[plot.PlotId].ThristTime >= plot.CurrentPlant.WaterCicles * 3)
            //         {
            //             plot.SetDead(true);
            //             yield break;
            //         }
            //         StartCoroutine(Thirst(plot));
            //     }
            //     else
            //     {
            //         StartCoroutine(Grow(plot));
            //     }
            // }
        }

        private void SetDeadPlot(Plot plot)
        {
            
        }
    }
}