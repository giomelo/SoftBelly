using UnityEngine;

namespace _Scripts.Systems.Lab
{
    public class Book : MonoBehaviour
    {
        [SerializeField]
        private GameObject bookLayer;


        public void OpenBook()
        {
            bookLayer.SetActive(true);
        }
        public void CloseBook()
        {
            bookLayer.SetActive(false);
        }
    }
}
