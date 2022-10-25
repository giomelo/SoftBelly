using System.Collections.Generic;
using System.Linq;
using _Scripts.Enums;
using _Scripts.Systems.Item;
using _Scripts.Systems.Lab.Machines.MixPanMachine;
using _Scripts.Systems.Plants.Bases;
using UnityEngine;

namespace _Scripts.Systems.Lab
{
    [CreateAssetMenu(fileName = "Potion", menuName = "Item/Potion")]
    public class PotionBase : ItemBehaviour
    {
        public List<SymptomsNivel> Cure = new List<SymptomsNivel>();
        public PotionType PotionType;
        public void Init(string id, ItemType itemType, Sprite sprite, float price, GameObject itemProprietiesGo, string itemDescription, List<SymptomsNivel> cure, PotionType potionType)
        {
            ItemId = id;
            ItemType = itemType;
            ImageDisplay = sprite;
            Price = price;
            ItemProprieties.ItemProprietiesGO = itemProprietiesGo;
            ItemProprieties.ItemProprietiesDescription = itemDescription;
            Cure = cure;
            PotionType = potionType;
        }
        
        // a planta que tiver o sintoma vai se transformar na poção que cura o sintoma
        /// <summary>
        /// vai servir para checar a poção cura o sintoma cura o sintoma pedido
        /// </summary>
        /// <param name="symptom"></param>
        /// <returns></returns>
        public bool CheckIfPortionHasCure(MedicalSymptoms symptom)
        {
            return Cure.Any(s => s.Symptoms == symptom);
        }

        public bool CheckIfPotionIsType(PotionType type)
        {
            return PotionType == type;
        }
    }
}