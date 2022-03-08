using System.Collections.Generic;
using System.Linq;
using _Scripts.Singleton;
using _Scripts.Systems.Inventories;
using _Scripts.Systems.Item;
using _Scripts.Systems.Plantation;
using _Scripts.Systems.Plants.Bases;
using UnityEngine;


namespace _Scripts.UI
{
    /// <summary>
    /// Controller for the ui for the inventory
    /// </summary>
    public class UIController : MonoBehaviour
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
        private GameObject slotPrefab;
        [SerializeField]
        private Transform slotDisplay;
        [SerializeField]
        private PlantProprieties proprietiesDisplay;
        
        [SerializeField]
        private int inventoryId;

        private int _currentSlot = 0;

        private void Start()
        {
            if (_slotsCreated) return;
        }
        private void OnEnable()
        {
            PlantEvents.OnPlotSelected += DisplayPlantInventory;
        }

        private void OnDisable()
        {
            PlantEvents.OnPlotSelected -= DisplayPlantInventory;
        }
        /// <summary>
        /// Active the inventory
        /// </summary>
        private void DisplayPlantInventory(int id)
        {
            Debug.Log(id);
            Debug.Log(inventoryId);
            if (inventoryId != id) return;
            inventoryObject.SetActive(true);
            if (_slotsCreated)
            {
                UpdateInventory();
                return;
            }

            CreateSlots();
            
        }
        /// <summary>
        /// Instantiate and update slots of the inventory first time
        /// </summary>
        private void CreateSlots()
        {
            int index = 0;
            _slotsCreated = true;
 
            for (int i = 0; i < storageHolder.Storage.Height; i++)
            {
                for (int j = 0; j < storageHolder.Storage.Width; j++)
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
            if (!slot.TryGetComponent<SlotBase>(out var slotScript)) return;
            slotScript.AddSubject(this);
            if (index >= storageHolder.Storage.Slots.Count) return;
            if (storageHolder.Storage.Slots.ElementAt(index).Value.amount > 0)
            {
                slotScript.uiSlot.amount.text = storageHolder.Storage.Slots.ElementAt(index).Value.amount.ToString();
                slotScript.uiSlot.item = storageHolder.Storage.Slots.ElementAt(index).Value.item;
                slotScript.uiSlot.itemImage.sprite = storageHolder.Storage.Slots.ElementAt(index).Value.item.ImageDisplay;
                if (_slotsCreated) return;
                slotScript.uiSlot.slotId = _currentSlot;
                _currentSlot++;
            }
            else
            {
                ResetSlot(slot, index);
            }
        }
        
        /// <summary>
        /// Reset the slot to the original state(blank item)
        /// </summary>
        /// <param name="slot"></param>
        /// <param name="index"></param>
        private void ResetSlot(Transform slot, int index)
        {
            if (!slot.TryGetComponent<SlotBase>(out var slotScript)) return;
            var prefabScript = slotPrefab.GetComponent<SlotBase>();
            slotScript.uiSlot.amount.text = prefabScript.uiSlot.amount.text;
            slotScript.uiSlot.item = null;
            slotScript.uiSlot.itemImage.sprite = prefabScript.uiSlot.itemImage.sprite;
        }
        
        /// <summary>
        /// Foreach slot update the slot with the new information in the dictionary
        /// </summary>
        private void UpdateInventory()
        {
            for (int i = 0; i < slotDisplay.childCount; i++)
            {
                UpdateSlots(slotDisplay.GetChild(i), i);
            }
        }
        
        /// <summary>
        /// Dispose the inventory
        /// </summary>
        public void DisposeInventory()
        {
            inventoryObject.SetActive(false);
        }
        
        /// <summary>
        /// Display plant proprieties for now
        /// </summary>
        /// <param name="item"></param>
        public void DisplayCurrentProprieties(SeedBase item)
        {
            proprietiesDisplay.ScientificName.text = item.ScientificName;
            proprietiesDisplay.PlantName.text = item.ItemId;
            proprietiesDisplay.ProprietiesText.text = item.PlantProprieties;
        }

        public void ResetCurrentProprieties()
        {
            proprietiesDisplay.ScientificName.text = "";
            proprietiesDisplay.PlantName.text = "";
            proprietiesDisplay.ProprietiesText.text = "";
        }
    }
}
