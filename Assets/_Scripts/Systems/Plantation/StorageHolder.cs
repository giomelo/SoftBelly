using System.Collections;
using System.Collections.Generic;
using _Scripts.Systems.Plantation;
using UnityEngine;

public class StorageHolder : MonoBehaviour
{
    private void Start()
    {
        Dictionary<string, int> slots = new Dictionary<string, int>(10);
        StorageBehaviour storage = new PlantationInventory(slots);
        storage.AddItem("oi", 10);
        storage.AddItem("oi2", 11);
        storage.RemoveItem("oi", 9);
        storage.AddItem("oi2", 5);
        storage.Display();
    }
}
