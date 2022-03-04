using UnityEngine;

namespace _Scripts.Systems.Item
{
    /// <summary>
    /// Base item class behavior for all items
    /// </summary>
    public abstract class ItemBehaviour : ScriptableObject
    {
        public string ItemId;

        public ItemBehaviour(string id)
        {
            ItemId = id;
        }

        public void Init(string id)
        {
            ItemId = id;
        }
    }
}
