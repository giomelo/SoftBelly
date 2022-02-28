using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Systems.Plants.Bases;
using UnityEngine;

/// <summary>
/// Handles the user inputs and send to other classes
/// </summary>
public class PlayerInputHandler : MonoBehaviour
{
    #region Fields
    
    [Header("Movement")]
  
    [SerializeField]
    private PlayerMovement playerMovement;
    
    [SerializeField]
    private Camera mainCamera;
    
    [Header("PlantInputs")]
    [SerializeField]
    private PlantationInteraction plantInteraction;

    private const int CollisionLayer = 1 << 6;

    public PlantBase currentPlantTest;
    private float _radius = 7f;

    #endregion
    // Update is called once per frame
    private void Update()
    {
        HandleMovementInput();
        PlantInput();
    }
    
    /// <summary>
    /// Get the user inputs and send to movement script
    /// </summary>
    private void HandleMovementInput()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (horizontal == 0 && vertical == 0) return;

        playerMovement.ProcessInput(horizontal, vertical);
    }
    
    /// <summary>
    /// Check if the mouse is down to plant a seed in the plot
    /// </summary>
    private void PlantInput()
    {
        if (!Input.GetMouseButton(0)) return;
        Ray direction = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        
        if (!Physics.Raycast(mainCamera.transform.position, direction.direction, out hit, CollisionLayer)) return;
        if (!hit.transform.TryGetComponent<Plot>(out var plotScript)) return;
        if (!plotScript.CheckAvaible()) return;
        if (!CheckDistanceFromPlayer(hit.transform)) return;
        plotScript.ChangePlant(currentPlantTest);
        PlantEvents.OnPlantedCall(plotScript.PlotId);
    }

    private bool CheckDistanceFromPlayer(Transform plot)
    {
        return Vector3.Distance(plot.position ,transform.position) <= _radius;
    }
}
