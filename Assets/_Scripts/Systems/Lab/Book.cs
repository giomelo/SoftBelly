using System.Collections.Generic;
using _Scripts.Singleton;
using UnityEngine;

namespace _Scripts.Systems.Lab
{
    public class Book : MonoSingleton<Book>
    {
        [SerializeField]
        private List<GameObject> pags;
        [SerializeField]
        private GameObject bookLayer;
        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.Tab)) return;
            BookLayer();
        }

        public void BookLayer()
        {
            bookLayer.SetActive(!bookLayer.activeInHierarchy);
        }
    }
}
