using System.Collections;
using _Scripts.Helpers;
using UnityEngine;

namespace _Scripts.Systems.Lab.Machines.MixPanMachine
{
    public class IngredientObj : DragObject
    {
        public IngredientToppingSO Ingredient;

        public override void Start()
        {
            base.Start();
            rb = GetComponent<Rigidbody>();
        }

        public override void OnMouseUp()
        {
            base.OnMouseUp();
            rb.isKinematic = true;
            StartCoroutine(BackRigidbody());
        }
    }
}