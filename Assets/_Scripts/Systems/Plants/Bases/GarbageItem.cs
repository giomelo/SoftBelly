using _Scripts.Systems.Inventories;
using _Scripts.Systems.Item;
using UnityEngine;

namespace _Scripts.Systems.Plants.Bases
{
    [CreateAssetMenu(fileName = "Garbage", menuName = "Item/Garbage")]
    public class GarbageItem : ItemBehaviour
    {
        public override void Initialized()
        {
            BaseMirrorItem plant = new BaseMirrorItem(ItemId, ItemType, ImageDisplay, Price, ItemProprietiesDescription);
            AllScriptableObjecst.Instance.AddInLisit(plant);
        }
    }
}