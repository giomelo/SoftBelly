using _Scripts.Systems.Plantation;
using _Scripts.Systems.Plants.Bases;
using UnityEngine;

namespace _Scripts.Entities.Player
{
   public class PlayerPlantationInteraction : MonoBehaviour
   {
      public PlantBase currentPlantTest;
      public void HandleInput(Plot plotScript)
      {
         plotScript.ChangePlant(currentPlantTest);
         PlantEvents.OnPlantedCall(plotScript);
      }
   }
}
