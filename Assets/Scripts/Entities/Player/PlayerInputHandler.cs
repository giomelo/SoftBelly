using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the user inputs and send to other classes
/// </summary>
public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField]
    private PlayerMovement playerMovement;
    

    // Update is called once per frame
    private void Update()
    {
        HandleMovementInput();
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
}
