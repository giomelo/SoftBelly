
using System.Collections.Generic;
using System.Linq;
using _Scripts.Entities.Player;
using _Scripts.Enums;
using _Scripts.Singleton;
using _Scripts.Systems.Inventories;
using _Scripts.Systems.Item;
using _Scripts.Systems.Lab;
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
        [Header("Slots offsets")]
        [SerializeField]
        private float YOffset = 50f;
        [SerializeField]
        private float XOffset = 30f;
        [Header("Slots start position")]
        [SerializeField]
        private Transform startPosition;
        [SerializeField]
        private GameObject slotPrefab;
        
        [Header("Place where the slots are gonna be created")]
        [SerializeField]
        private Transform slotDisplay;
        [Header("Proprieties")]
        [SerializeField]
        private PlantProprieties proprietiesDisplay;
        
        [Tooltip("Each inventory in the scene has to have one unique inventoryId")]
        [SerializeField]
        public int inventoryId;

        private int _currentSlot;
        
        [SerializeField]
        private InventoryType inventoryType;

        public int Width;
        public int Height;
        private GameObject proprietiesObj;
        [SerializeField]
        private List<GameObject> poolProprieties = new List<GameObject>();

        private float xOffsetProprieties = 210f;
        private float yOffsetProprieties = 80f;

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
        }

        private void OnDisable()
        {
            PlantEvents.OnPlotSelected -= DisplayInventory;
            PlantEvents.LabInventoryAction -= AddHarvestedPlant;
            LabEvents.OnChestSelected -= DisplayInventory;
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
        }
        /// <summary>
        /// Instantiate and update slots of the inventory first time
        /// </summary>
        private void CreateSlots()
        {
            int index = 0;
            
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    var position = startPosition.position;
                    var pos = new Vector3(position.x + XOffset * j,position.y - YOffset * i, position.z);

                    var slotInstance = Instantiate(slotPrefab, pos, Quaternion.identity);
                    slotInstance.transform.SetParent(slotDisplay);
                    UpdateSlots(slotInstance.transform, index);
                    index++;
                }
            }
            _slotsCreated = true;

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
        
            if (index >= StorageHolder.Storage.Slots.Count) return; //not update an empty slot index is the slot position

            if (StorageHolder.Storage.Slots.ElementAt(index).Value.amount > 0)
            {
                slotScript.uiSlot.amount.text = StorageHolder.Storage.Slots.ElementAt(index).Value.amount.ToString();
                slotScript.uiSlot.item = StorageHolder.Storage.Slots.ElementAt(index).Value.item;
                slotScript.uiSlot.itemImage.sprite = StorageHolder.Storage.Slots.ElementAt(index).Value.item.ImageDisplay;
                if (_slotsCreated) return;
                
                slotScript.uiSlot.slotId = _currentSlot;
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
        private void ResetSlot(Transform slot)
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
        public void UpdateInventory()
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
            ResetCurrentProprieties();
            inventoryObject.SetActive(false);
            LabEvents.OnMachineDisposeCall(LabEvents.CurrentMachine);
        }
        
        /// <summary>
        /// Display plant proprieties for now
        /// </summary>
        /// <param name="item"></param>
        public void DisplayCurrentProprieties(GameObject item, Transform local)
        {
            // //proprietiesDisplay.ScientificName.text = item.ScientificName;
            // proprietiesDisplay.PlantName.text = item.ItemId;
            // proprietiesDisplay.ProprietiesText.text = item.Proprieties;
            GameObject aux = null;
            var position = local.transform.position;
            Debug.Log(position);
            var pos = new Vector3(position.x + xOffsetProprieties, position.y - yOffsetProprieties, 0);
            foreach (GameObject go in poolProprieties)
            {
                Debug.Log(go);
                Debug.Log(item);
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
            }
    
        }

        public void ResetCurrentProprieties()
        {
            // proprietiesDisplay.ScientificName.text = "";
            // proprietiesDisplay.PlantName.text = "";
            // proprietiesDisplay.ProprietiesText.text = "";
            foreach (GameObject go in poolProprieties)
            {
                go.SetActive(false);

            }
        }

        private void AddHarvestedPlant(int id)
        {
            if (StorageHolder.Storage.storageId != id) return;
            StorageHolder.Storage.AddItem(1, PlantEvents.PlantCollected);
            StorageHolder.UpdateExposedInventory();
        }
    }
}
