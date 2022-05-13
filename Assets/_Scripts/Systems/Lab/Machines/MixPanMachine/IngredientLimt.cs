using System;
using UnityEngine;

namespace _Scripts.Systems.Lab.Machines.MixPanMachine
{
    public class IngredientLimt : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("oi");
            if (!other.transform.TryGetComponent<IngredientObj>(out var ingredient)) return;
            MixPan.OnIngredientAddCall(ingredient);
        }
    }
}

