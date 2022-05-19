using System.Collections.Generic;
using _Scripts.Enums;
using _Scripts.Systems.Item;
using _Scripts.Systems.Lab.Machines.MixPanMachine;
using UnityEngine;

namespace _Scripts.Systems.Lab
{
    [CreateAssetMenu(fileName = "Potion", menuName = "Item/Potion")]
    public class PotionBase : ItemBehaviour
    {
        public List<IngredientsList> IngredientsList = new List<IngredientsList>();
        public MedicalSymptoms Cure;
        public PotionType PotionType;
        public void Init(string id, ItemType itemType, Sprite sprite, float price, GameObject itemProprietiesGo, string itemDescription, List<IngredientsList> list, MedicalSymptoms cure)
        {
            ItemId = id;
            ItemType = itemType;
            ImageDisplay = sprite;
            Price = price;
            ItemProprieties.ItemProprietiesGO = itemProprietiesGo;
            ItemProprieties.ItemProprietiesDescription = itemDescription;
            Cure = cure;
        }
        
        // a planta que tiver o sintoma vai se transformar na poção que cura o sintoma
        /// <summary>
        /// vai servir para checar a poção cura o sintoma cura o sintoma pedido
        /// </summary>
        /// <param name="symptom"></param>
        /// <returns></returns>
        public bool CheckIfPortionHasCure(MedicalSymptoms symptom)
        {
            return Cure.HasFlag(symptom);
        }
    }
}