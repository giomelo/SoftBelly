
using System.Collections.Generic;
using System.Linq;
using _Scripts.Entities.Player;
using _Scripts.Enums;
using _Scripts.Singleton;
using _Scripts.Systems.Inventories;
using _Scripts.Systems.Item;
using _Scripts.Systems.Lab;
using _Scripts.Systems.Lab.Machines.Base;
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
        public StorageHolder StorageHolder { get; private set; }

        private bool _slotsCreated = false;
        [Header("Slots start position")]
        [SerializeField]
        private Transform startPosition;
        [SerializeField]
        private GameObject slotPrefab;
        
        [Header("Place where the slots are gonna be created")]
        [SerializeField]
        private Transform slotDisplay;


        [Tooltip("Each inventory in the scene has to have one unique inventoryId")]
        [SerializeField]
        public int inventoryId;

        private int _currentSlot;
        
        [SerializeField]
        private InventoryType inventoryType;
        private GameObject proprietiesObj;
        [SerializeField]
        private List<GameObject> poolProprieties = new List<GameObject>();
        
        [SerializeField]
        [Header("Offset for display proprieties")]
        private float xOffsetProprieties = 210f;
        [SerializeField]
        private float yOffsetProprieties = 80f;
        [Header("Show invenotry button")]
        [SerializeField]
        private GameObject showButton; // show invenotry button
        [SerializeField]
        private List<SlotBase> allSlots = new List<SlotBase>();

        private void Start()
        {
            if (_slotsCreated) return;
            var storagesInScene = GameObject.FindObjectsOfType<StorageHolder>();
            // ReSharper disable once SuggestVarOrType_SimpleTypes
            foreach (StorageHolder s in storagesInScene)
            {
                if (s.Storage.InventoryType == inventoryType)
                {
                    StorageHolder = s;
                }
            }
        }
        private void OnEnable()
        {
            PlantEvents.OnPlotSelected += DisplayInventory;
            PlantEvents.LabInventoryAction += AddHarvestedPlant;
            LabEvents.OnChestSelected += DisplayInventory;
            LabEvents.OnItemRemoved += RemoveItemAllInventoryTypes;
        }

        private void OnDisable()
        {
            PlantEvents.OnPlotSelected -= DisplayInventory;
            PlantEvents.LabInventoryAction -= AddHarvestedPlant;
            LabEvents.OnChestSelected -= DisplayInventory;
            LabEvents.OnItemRemoved -= RemoveItemAllInventoryTypes;
        }
        /// <summary>
        /// Active the inventory
        /// </summary>
        public void DisplayInventory(int id)
        {
            if (inventoryId != id) return;
            inventoryObject.SetActive(true);
            if (_slotsCreated)
            {
                UpdateInventory();
                return;
            }
            
            CreateSlots();
            UpdateInventory();
        }
        /// <summary>
        /// Instantiate and update slots of the inventory first time
        /// </summary>
        private void CreateSlots()
        {
            int index = 0;
            
            // for (int i = 0; i < Height; i++)
            // {
            //     for (int j = 0; j < Width; j++)
            //     {
            //         var position = startPosition.position;
            //         var pos = new Vector3(position.x + XOffset * j,position.y - YOffset * i, position.z);
            //
            //         var slotInstance = Instantiate(slotPrefab, pos, Quaternion.identity);
            //         slotInstance.transform.SetParent(slotDisplay);
            //         UpdateSlots(slotInstance.transform, index);
            //         index++;
            //     }
            // }
            
            for (int i = 0; i < StorageHolder.Storage.Size; i++)
            {
                var position = startPosition.position;
                var pos = new Vector3(position.x ,position.y, position.z);
            
                var slotInstance = Instantiate(slotPrefab, pos, Quaternion.identity);
                slotInstance.transform.SetParent(slotDisplay);
                if (slotInstance.TryGetComponent<SlotBase>(out var slotScript))
                {
                    allSlots.Add(slotScript);
                    UpdateSlots(slotScript, index);
                }
                index++;
            }
            _slotsCreated = true;

        }

        /// <summary>
        /// Void for update the information of the slot
        /// </summary>
        /// <param name="slot"></param>
        /// <param name="index"></param>
        private void UpdateSlots(SlotBase slot, int index)
        {
            slot.AddSubject(this);
        
            if (index >= StorageHolder.Storage.Slots.Count) return; //not update an empty slot index is the slot position

            if (StorageHolder.Storage.Slots.ElementAt(index).Value.amount > 0)
            {
                slot.uiSlot.amount.text = StorageHolder.Storage.Slots.ElementAt(index).Value.amount.ToString();
                slot.uiSlot.item = StorageHolder.Storage.Slots.ElementAt(index).Value.item;
                slot.uiSlot.itemImage.sprite = StorageHolder.Storage.Slots.ElementAt(index).Value.item.ImageDisplay;
                if (_slotsCreated) return;
                
                slot.uiSlot.slotId = _currentSlot;
                _currentSlot++;
            }
            else
            {
                ResetSlot(slot);
            }
        }

        /// <summary>
        /// Reset the slot to the original state(blank item)
        /// </summary>
        /// <param name="slot"></param>
        /// <param name="index"></param>
        private void ResetSlot(SlotBase slot)
        {
            //if (!slot.TryGetComponent<SlotBase>(out var slotScript)) return;
            var prefabScript = slotPrefab.GetComponent<SlotBase>();
            slot.uiSlot.amount.text = prefabScript.uiSlot.amount.text;
            slot.uiSlot.item = null;
            slot.uiSlot.itemImage.sprite = prefabScript.uiSlot.itemImage.sprite;
        }

        /// <summary>
        /// Foreach slot update the slot with the new information in the dictionary
        /// </summary>
        public void UpdateInventory()
        {
            for (int i = 0; i < allSlots.Count; i++)
            {
                UpdateSlots(allSlots[i], i);
            }
        }
        
        /// <summary>
        /// Dispose the inventory
        /// </summary>
        public void DisposeInventory()
        {
            ResetCurrentProprieties();
            inventoryObject.SetActive(false);
            LabEvents.OnMachineDisposeCall(LabEvents.CurrentMachine);
        }
        
        /// <summary>
        /// Display plant proprieties for now
        /// </summary>
        /// <param name="item"></param>
        public void DisplayCurrentProprieties(GameObject item, Transform local, ItemBehaviour itemS)
        {
            // //proprietiesDisplay.ScientificName.text = item.ScientificName;
            // proprietiesDisplay.PlantName.text = item.ItemId;
            // proprietiesDisplay.ProprietiesText.text = item.Proprieties;
            GameObject aux = null;
            var position = local.transform.position;
            var pos = new Vector3(position.x + xOffsetProprieties, position.y - yOffsetProprieties, 0);
            foreach (GameObject go in poolProprieties)
            {
                if (go.name == $"{item.name}(Clone)")
                {
                    aux = go;
                }
                
            }
            
            if (aux == null)
            {
                proprietiesObj = Instantiate(item, pos, Quaternion.identity, inventoryObject.transform);
                
                //proprietiesObj.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
                proprietiesObj.SetActive(true);
                poolProprieties.Add(proprietiesObj);
            }else
            {
                aux.transform.position = pos;
                aux.SetActive(true);
                aux.transform.GetComponent<ItemProprieties>().text.text = itemS.ItemProprieties.ItemProprietiesDescription;
            }
    
        }

        public void ResetCurrentProprieties()
        {
            foreach (GameObject go in poolProprieties)
            {
                go.SetActive(false);

            }
        }

        private void AddHarvestedPlant(int id)
        {
            if (StorageHolder.Storage.storageId != id) return;
            Debug.LogWarning("AddedPLant");
            StorageHolder.Storage.AddItem(1, PlantEvents.PlantCollected);
            StorageHolder.UpdateExposedInventory();
        }
        
        //remove em todos os inventários da cena o item
        private void RemoveItemAllInventoryTypes(int key, int amount, InventoryType type)
        {
            if (inventoryType != type) return;

            StorageHolder.Storage.RemoveItem(key, amount);
            UpdateInventory();
        }

        public void HideInventory()
        {
            inventoryObject.SetActive(false);
            showButton.SetActive(true);
        }

        public void ShowInventory()
        {
            inventoryObject.SetActive(true);
            showButton.SetActive(false);
        }

        public void ShowItemsAvailable(BaseMachine machineSlot)
        {
            Debug.LogWarning("Items");
            var slot = machineSlot.IngredientsSlots[0].Slot;
            
            for (int i = 0; i < allSlots.Count; i++)
            {
                if (allSlots[i].uiSlot.item == null) continue;
                allSlots[i].uiSlot.itemImage.color = Color.white;
                if (!slot.itemRequired.HasFlag(allSlots[i].uiSlot.item.ItemType))
                {
                    allSlots[i].uiSlot.itemImage.color = Color.black;
                }
            }
        }
    }
}
