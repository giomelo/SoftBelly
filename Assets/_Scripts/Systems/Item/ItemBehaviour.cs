using _Scripts.Editor.FlagsAtributeEditor;
using _Scripts.Enums;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.Systems.Item
{
    /// <summary>
    /// Base item class behavior for all items
    /// </summary>
    public abstract class ItemBehaviour : ScriptableObject
    {
        [Header("Item Stuff")]
        public string ItemId;
        [EnumFlagsAtribute]
        public ItemType ItemType;

        public Sprite ImageDisplay;

        // public ItemBehaviour(string id, ItemType itemType)
        // {
        //     ItemId = id;
        //     ItemType = itemType;
        // }

        public void Init(string id, ItemType itemType)
        {
            ItemId = id;
            ItemType = itemType;
        }
    }
}
