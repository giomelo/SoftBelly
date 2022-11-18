using System;
using System.Collections.Generic;
using _Scripts.Singleton;
using _Scripts.Systems.Item;
using System.Linq;
using _Scripts.Enums;
using _Scripts.Helpers;
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

        public SaveSprites sprites;
        public GameObject ItemProprietiesGo;
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
                    sprites.FindGameObject(mseed.ItemId);
                    seed.Init(mseed.ItemId, mseed.ItemType, sprites.FindSprite(mseed.ImageDisplay), mseed.Price, mseed.ItemProprietiesDescription,
                        o, mseed.GrowTime, mseed.WaterCicles, mseed.PlantBase.MirrorToBase());
                    item = seed;
                    break;
                case ItemType.Plant:
                    var plant = ScriptableObject.CreateInstance<PlantBase>();
                    BasePlantMirror msplant = i as BasePlantMirror;
                    plant.Init(msplant.ItemId, msplant.ItemType, sprites.FindSprite(msplant.ImageDisplay), msplant.Price, msplant.ItemProprietiesDescription,
                        sprites.FindSprite(msplant.BurnedPlant), sprites.FindSprite(msplant.MixedPlant), sprites.FindSprite(msplant.DriedPlant), sprites.FindSprite(msplant.PotionStuff),sprites.FindSprite(msplant.SmashedPlant), msplant.MedicalSymptoms, msplant.plantTemperature, msplant.MachineList);
                    item = plant;
                    break;
                case ItemType.Potion:
                    var potion = ScriptableObject.CreateInstance<PotionBase>();
                    PotionMirror p = i as PotionMirror;
                    potion.Init(p.ItemId, p.ItemType, sprites.FindSprite(p.ImageDisplay), p.Price, p.ItemProprietiesDescription, p.Cure, p.PotionType);
                    item = potion;
                    break;
                case ItemType.Ingredient:
                    break;
                case ItemType.Other:
                    break;
                case ItemType.Garbage:
                    var garbage = ScriptableObject.CreateInstance<GarbageItem>();
                    BaseMirrorItem g = i;
                    garbage.Init(g.ItemId, g.ItemType, sprites.FindSprite(g.ImageDisplay), g.Price, g.ItemProprietiesDescription);
                    item = garbage;
                    break;
                case ItemType.MixedPlant:
                    var mix = ScriptableObject.CreateInstance<BaseMixedItem>();
                    BaseMixedItemMirror m = i as BaseMixedItemMirror;
                    mix.Init(m.ItemId, m.ItemType, sprites.FindSprite(m.ImageDisplay), m.Price, m.ItemProprietiesDescription, m.IngredientsList, m.BasePlant.MirrorToBase());
                    item = mix;
                    break;
                case ItemType.Burned:
                    var plant2 = ScriptableObject.CreateInstance<PlantBase>();
                    BasePlantMirror msplant2 = i as BasePlantMirror;
                    plant2.Init(msplant2.ItemId, ItemType.Burned, sprites.FindSprite(msplant2.ImageDisplay), msplant2.Price, msplant2.ItemProprietiesDescription + " Burned", sprites.FindSprite(msplant2.BurnedPlant), sprites.FindSprite(msplant2.MixedPlant), sprites.FindSprite(msplant2.DriedPlant), sprites.FindSprite(msplant2.PotionStuff), sprites.FindSprite(msplant2.SmashedPlant), msplant2.MedicalSymptoms, msplant2.plantTemperature, msplant2.MachineList);
                    item = plant2;
                    break;
                case ItemType.Dryed:
                    var plant3 = ScriptableObject.CreateInstance<PlantBase>();
                    BasePlantMirror msplant3 = i as BasePlantMirror;
                    plant3.Init(msplant3.ItemId, ItemType.Dryed, sprites.FindSprite(msplant3.ImageDisplay), msplant3.Price, msplant3.ItemProprietiesDescription + " Dryed", sprites.FindSprite(msplant3.BurnedPlant), sprites.FindSprite(msplant3.MixedPlant), sprites.FindSprite(msplant3.DriedPlant), sprites.FindSprite(msplant3.PotionStuff),sprites.FindSprite(msplant3.SmashedPlant),msplant3.MedicalSymptoms, msplant3.plantTemperature, msplant3.MachineList);
                    item = plant3;
                    break;
                case ItemType.Smashed:
                    var smahsed = ScriptableObject.CreateInstance<SmashedItemBase>();
                    SmashedPlantMirror smasehdd = i as SmashedPlantMirror;
                    smahsed.Init(smasehdd.ItemId, smasehdd.ItemType, sprites.FindSprite(smasehdd.ImageDisplay), smasehdd.Price, smasehdd.ItemProprietiesDescription, smasehdd.BasePlant.MirrorToBase());
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
                BaseMirrorItem mirror = new BaseMirrorItem(items.Value.item.ItemId, items.Value.item.ItemType, items.Value.item.ImageDisplay,items.Value.item.Price, items.Value.item.ItemProprietiesDescription);
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