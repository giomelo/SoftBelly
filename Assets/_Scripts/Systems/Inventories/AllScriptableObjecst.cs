using System;
using System.Collections.Generic;
using _Scripts.Singleton;
using _Scripts.Systems.Item;
using System.Linq;
using _Scripts.Enums;
using _Scripts.SaveSystem;
using _Scripts.Systems.Lab;
using _Scripts.Systems.Lab.Machines.MixPanMachine;
using _Scripts.Systems.Lab.Machines.PestleMachine;
using _Scripts.Systems.Plants.Bases;
using UnityEditor;
using UnityEngine;

namespace _Scripts.Systems.Inventories
{
    public class AllScriptableObjecst : MonoSingleton<AllScriptableObjecst> , DataObject
    {
        public List<BaseMirrorItem> scripotable = new List<BaseMirrorItem>();


        public ItemBehaviour MirrorToItem(string obj)
        {
            var i = scripotable.Find(c => c.ItemId == obj);
            ItemBehaviour item = ScriptableObject.CreateInstance<ItemBehaviour>();
            switch (i.ItemType)
            {
                case ItemType.Seed:
                    var seed = ScriptableObject.CreateInstance<SeedBase>();
                    SeedBaseMirror mseed = i as SeedBaseMirror;
                    GameObject[] o = new GameObject[3];
                    for (int j = 0; j < mseed.PlantDisplayObjs.Length; j++)
                    {
                        o[j] = (GameObject)AssetDatabase.LoadAssetAtPath(mseed.PlantDisplayObjs[j], typeof(GameObject));
                    }
                    
                    seed.Init(mseed.ItemId, mseed.ItemType, (Sprite)AssetDatabase.LoadAssetAtPath(mseed.ImageDisplay, typeof(Sprite)), mseed.Price,
                        (GameObject)AssetDatabase.LoadAssetAtPath(mseed.ItemProprieties.ItemProprietiesGO, typeof(GameObject)), mseed.ItemProprieties.ItemProprietiesDescription,
                        o, mseed.GrowTime, mseed.WaterCicles, mseed.PlantBase.MirrorToBase());
                    item = seed;
                    break;
                case ItemType.Plant:
                    var plant = ScriptableObject.CreateInstance<PlantBase>();
                    BasePlantMirror msplant = i as BasePlantMirror;
                    plant.Init(msplant.ItemId, msplant.ItemType, (Sprite)AssetDatabase.LoadAssetAtPath(msplant.ImageDisplay, typeof(Sprite)), msplant.Price,
                        (GameObject)AssetDatabase.LoadAssetAtPath(msplant.ItemProprieties.ItemProprietiesGO, typeof(GameObject)), msplant.ItemProprieties.ItemProprietiesDescription, (Sprite)AssetDatabase.LoadAssetAtPath(msplant.BurnedPlant, typeof(Sprite)), (Sprite)AssetDatabase.LoadAssetAtPath(msplant.MixedPlant, typeof(Sprite)), (Sprite)AssetDatabase.LoadAssetAtPath(msplant.DriedPlant, typeof(Sprite)), (Sprite)AssetDatabase.LoadAssetAtPath(msplant.PotionStuff, typeof(Sprite)),(Sprite)AssetDatabase.LoadAssetAtPath(msplant.SmashedPlant, typeof(Sprite)), msplant.MedicalSymptoms, msplant.plantTemperature, msplant.MachineList);
                    item = plant;
                    break;
                case ItemType.Potion:
                    var potion = ScriptableObject.CreateInstance<PotionBase>();
                    PotionMirror p = i as PotionMirror;
                    potion.Init(p.ItemId, p.ItemType, (Sprite)AssetDatabase.LoadAssetAtPath(p.ImageDisplay, typeof(Sprite)), p.Price,
                        (GameObject)AssetDatabase.LoadAssetAtPath(p.ItemProprieties.ItemProprietiesGO, typeof(GameObject)), p.ItemProprieties.ItemProprietiesDescription, p.Cure, p.PotionType);
                    item = potion;
                    break;
                case ItemType.Ingredient:
                    break;
                case ItemType.Other:
                    break;
                case ItemType.Garbage:
                    var garbage = ScriptableObject.CreateInstance<GarbageItem>();
                    BaseMirrorItem g = i;
                    garbage.Init(g.ItemId, g.ItemType, (Sprite)AssetDatabase.LoadAssetAtPath(g.ImageDisplay, typeof(Sprite)), g.Price,
                        (GameObject)AssetDatabase.LoadAssetAtPath(g.ItemProprieties.ItemProprietiesGO, typeof(GameObject)), g.ItemProprieties.ItemProprietiesDescription);
                    item = garbage;
                    break;
                case ItemType.MixedPlant:
                    var mix = ScriptableObject.CreateInstance<BaseMixedItem>();
                    BaseMixedItemMirror m = i as BaseMixedItemMirror;
                    mix.Init(m.ItemId, m.ItemType, (Sprite)AssetDatabase.LoadAssetAtPath(m.ImageDisplay, typeof(Sprite)), m.Price,
                        (GameObject)AssetDatabase.LoadAssetAtPath(m.ItemProprieties.ItemProprietiesGO, typeof(GameObject)), m.ItemProprieties.ItemProprietiesDescription, m.IngredientsList, m.BasePlant.MirrorToBase());
                    item = mix;
                    break;
                case ItemType.Burned:
                    var plant2 = ScriptableObject.CreateInstance<PlantBase>();
                    BasePlantMirror msplant2 = i as BasePlantMirror;
                    plant2.Init(msplant2.ItemId, ItemType.Burned, (Sprite)AssetDatabase.LoadAssetAtPath(msplant2.ImageDisplay, typeof(Sprite)), msplant2.Price,
                        (GameObject)AssetDatabase.LoadAssetAtPath(msplant2.ItemProprieties.ItemProprietiesGO, typeof(GameObject)), msplant2.ItemProprieties.ItemProprietiesDescription + " Burned", (Sprite)AssetDatabase.LoadAssetAtPath(msplant2.BurnedPlant, typeof(Sprite)), (Sprite)AssetDatabase.LoadAssetAtPath(msplant2.MixedPlant, typeof(Sprite)), (Sprite)AssetDatabase.LoadAssetAtPath(msplant2.DriedPlant, typeof(Sprite)), (Sprite)AssetDatabase.LoadAssetAtPath(msplant2.PotionStuff, typeof(Sprite)), (Sprite)AssetDatabase.LoadAssetAtPath(msplant2.SmashedPlant, typeof(Sprite)), msplant2.MedicalSymptoms, msplant2.plantTemperature, msplant2.MachineList);
                    item = plant2;
                    break;
                case ItemType.Dryed:
                    var plant3 = ScriptableObject.CreateInstance<PlantBase>();
                    BasePlantMirror msplant3 = i as BasePlantMirror;
                    plant3.Init(msplant3.ItemId, ItemType.Dryed, (Sprite)AssetDatabase.LoadAssetAtPath(msplant3.ImageDisplay, typeof(Sprite)), msplant3.Price,
                        (GameObject)AssetDatabase.LoadAssetAtPath(msplant3.ItemProprieties.ItemProprietiesGO, typeof(GameObject)), msplant3.ItemProprieties.ItemProprietiesDescription + " Dryed", (Sprite)AssetDatabase.LoadAssetAtPath(msplant3.BurnedPlant, typeof(Sprite)), (Sprite)AssetDatabase.LoadAssetAtPath(msplant3.MixedPlant, typeof(Sprite)), (Sprite)AssetDatabase.LoadAssetAtPath(msplant3.DriedPlant, typeof(Sprite)), (Sprite)AssetDatabase.LoadAssetAtPath(msplant3.PotionStuff, typeof(Sprite)), (Sprite)AssetDatabase.LoadAssetAtPath(msplant3.SmashedPlant, typeof(Sprite)), msplant3.MedicalSymptoms, msplant3.plantTemperature, msplant3.MachineList);
                    item = plant3;
                    break;
                case ItemType.Smashed:
                    var smahsed = ScriptableObject.CreateInstance<SmashedItemBase>();
                    SmashedPlantMirror smasehdd = i as SmashedPlantMirror;
                    smahsed.Init(smasehdd.ItemId, smasehdd.ItemType, (Sprite)AssetDatabase.LoadAssetAtPath(smasehdd.ImageDisplay, typeof(Sprite)), smasehdd.Price,
                        (GameObject)AssetDatabase.LoadAssetAtPath(smasehdd.ItemProprieties.ItemProprietiesGO, typeof(GameObject)), smasehdd.ItemProprieties.ItemProprietiesDescription, smasehdd.BasePlant.MirrorToBase());
                    item = smahsed;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return item;
        }

        public Dictionary<int, ItemObj> ConvertList(Dictionary<int, MirrorItem> item)
        {
            Dictionary<int, ItemObj> lista = new Dictionary<int, ItemObj>();

            foreach (var items in item)
            {
                ItemObj s = new ItemObj(MirrorToItem(items.Value.Name.ItemId), items.Value.Amount);
                lista.Add(items.Key,s);
            }

            return lista;
        }
        public Dictionary<int, MirrorItem> ConvertListInverse(Dictionary<int, ItemObj> item)
        {
            Dictionary<int, MirrorItem> lista = new Dictionary<int, MirrorItem>();

            foreach (var items in item)
            {
                BaseMirrorItem mirror = new BaseMirrorItem(items.Value.item.ItemId, items.Value.item.ItemType, items.Value.item.ImageDisplay,items.Value.item.Price,items.Value.item.ItemProprieties.ItemProprietiesGO, items.Value.item.ItemProprieties.ItemProprietiesDescription);
                MirrorItem s = new MirrorItem(items.Value.amount, mirror);
                lista.Add(items.Key,s);
            }

            return lista;
        }

        private void Awake()
        {
            base.Awake();
            Load();
        }
        public void AddInLisit(BaseMirrorItem item)
        {
            if (scripotable.Contains(item)) return;
            scripotable.Add(item);
        }

        public void Load()
        {
            SaveScript d = (SaveScript)Savesystem.Load(GetType().ToString());
            // if (IsNewGame)
            // {
            //     //Developer.ClearSaves();
            //     // clear save
            //     Debug.Log("NewGame");
            //     return;
            // }
            if (d != null)
            {
                // /*variavell*/ = /*variavel*/ = data./*variavel*/;

                scripotable = d.Items;

            }
        }

        public void Save()
        {
            SaveData data = new SaveScript(scripotable);
            Savesystem.Save(data, GetType().ToString());
        }
    }
}