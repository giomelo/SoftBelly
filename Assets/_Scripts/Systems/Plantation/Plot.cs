using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Systems.Plants.Bases;
using Systems.Plantation;
using UnityEngine;

public class Plot : MonoBehaviour
{
    public int PlotId;
    public PlantBase currentPlant;

    public void ChangePlant(PlantBase plant)
    {
        currentPlant = plant;
    }
    public void Display(int id)
    {
        if (id != this.PlotId) return;
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
