using System.Collections.Generic;
using _Scripts.Singleton;
using _Scripts.Systems.Plantation;
using UnityEngine;

namespace Systems.Plantation
{
    /// <summary>
    /// System for creat a plot grid
    /// </summary>
    public class GridSystem : MonoSingleton<GridSystem>
    {
        [SerializeField]
        private int Width = 10;
        [SerializeField]
        private int Height = 10;

        [SerializeField]
        private Transform startPosition;
        
        [SerializeField]
        private float OffsetX = 10f;
        [SerializeField]
        private float OffsetZ = 10f;

        [SerializeField]
        private GameObject plot;

        public static int PlotsId = 0;

        public List<Plot> Plots = new List<Plot>();

        public void CreatGrid()
        {
            Plots.Clear();
            DeleteGrid();
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    var position = startPosition.position;
                    var pos = new Vector3(position.x + OffsetX * j, position.y, position.z+ OffsetZ * i);
                    var plotInstance = Instantiate(plot, pos, Quaternion.identity);
                    plotInstance.transform.parent = this.transform;
                    Plots.Add(plotInstance.transform.GetChild(0).GetComponent<Plot>());
                }
            }
        }

        private void DeleteGrid()
        {
            int len = transform.childCount;
            for (int i = 0; i < len; i++)
            {
                DestroyImmediate(transform.GetChild(0).gameObject);
            }
        }

        private void Start()
        {
            PlotsId = 0;
            foreach (var plot in Plots)
            {
                plot.PlotId = PlotsId;
                PlotsId++;
            }
            PlantTimeController.Instance.CreatPlants();
        }
    }
}
