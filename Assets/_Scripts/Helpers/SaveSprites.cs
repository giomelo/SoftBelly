using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Scripts.Helpers
{
    [Serializable]
    public struct PlantAndGameObject
    {
        public string id;
        public GameObject[] fase;
    }
    
    [CreateAssetMenu(fileName = "SaveSprites", menuName = "SaveSprites")]
    public class SaveSprites : ScriptableObject
    {
        public List<Sprite> allSprites;
        public List<PlantAndGameObject> plantsAndGameObjects;

        public int FindIndex(Sprite sprite)
        {
            return allSprites.IndexOf(sprite);
        }

        public Sprite FindSprite(int index)
        {
            return allSprites[index];
        }
        
        public GameObject[] FindGameObject(string plant)
        {
            return plantsAndGameObjects.First(c => c.id == plant).fase;
        }
    }
}