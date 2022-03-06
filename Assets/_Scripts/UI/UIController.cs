using System.Collections.Generic;
using System.Linq;
using _Scripts.Singleton;
using _Scripts.Systems.Plantation;
using _Scripts.Systems.Plants.Bases;
using UnityEngine;


namespace _Scripts.UI
{
   

    public class UIController : MonoSingleton<UIController>
    {
        [SerializeField]
        private GameObject inventoryObject;
        public StorageHolder storageHolder;

        private bool _slotsCreated = false;
        private const float YOffset = 80f;
        private const float XOffset = 50f;

        [SerializeField]
        private Transform startPosition;
        [SerializeField]
        private List<UISlot> uiSlots;
        [SerializeField]
        private GameObject slotPrefab;
        [SerializeField]
        private Transform slotDisplay;

        private void Start()
        {
            if (_slotsCreated) return;
            uiSlots = new List<UISlot>(storageHolder.Storage.Width * storageHolder.Storage.Height);
        }
        private void OnEnable()
        {
            PlantEvents.OnPlotSelected += DisplayPlantInventory;
        }

        private void OnDisable()
        {
            PlantEvents.OnPlotSelected -= DisplayPlantInventory;
        }
        private void DisplayPlantInventory()
        {
            inventoryObject.SetActive(true);
            if (_slotsCreated)
            {
                UpdateInventory();
                return;
            }

            CreateSlots();
            
        }

        private void CreateSlots()
        {
            int index = 0;
            _slotsCreated = true;
            for (int i = 0; i < storageHolder.Storage.Width; i++)
            {
                for (int j = 0; j < storageHolder.Storage.Height; j++)
                {
                    var position = startPosition.position;
                    var pos = new Vector3(position.x + XOffset * j,position.y - YOffset * i, position.z);

                    var slotInstance = Instantiate(slotPrefab, pos, Quaternion.identity, slotDisplay);
                    UpdateSlots(slotInstance.transform, index);
                    index++;
                }
            }
        }

        /// <summary>
        /// Void for update the information of the slot
        /// </summary>
        /// <param name="slot"></param>
        /// <param name="index"></param>
        private void UpdateSlots(Transform slot, int index)
        {
            if (!slot.TryGetComponent<PlantSlot>(out var slotScript)) return;
            if (index >= storageHolder.Storage.Slots.Count) return;
            if (storageHolder.Storage.Slots.ElementAt(index).Value > 0)
            {
                slotScript.uiSlot.amount.text = storageHolder.Storage.Slots.ElementAt(index).Value.ToString();
                slotScript.uiSlot.item = storageHolder.Storage.Slots.ElementAt(index).Key;
                slotScript.uiSlot.itemImage.sprite = storageHolder.Storage.Slots.ElementAt(index).Key.ImageDisplay;
            }
            else
            {
                ResetSlot(slot, index);
            }
        }

        private void ResetSlot(Transform slot, int index)
        {
            if (!slot.TryGetComponent<PlantSlot>(out var slotScript)) return;
            var prefabScript = slotPrefab.GetComponent<PlantSlot>();
            slotScript.uiSlot.amount.text = prefabScript.uiSlot.amount.text;
            slotScript.uiSlot.item = null;
            slotScript.uiSlot.itemImage.sprite = prefabScript.uiSlot.itemImage.sprite;
        }

        private void UpdateInventory()
        {
            for (int i = 0; i < slotDisplay.childCount; i++)
            {
                UpdateSlots(slotDisplay.GetChild(i), i);
            }
        }

        public void DisposeInventory()
        {
            inventoryObject.SetActive(false);
        }
    }
}
