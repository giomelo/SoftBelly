using System;
using TMPro;
using UnityEngine;

namespace _Scripts.UI
{
    public class ItemProprieties : MonoBehaviour
    {
        public TextMeshProUGUI text;
        public string description;

        private void Start()
        {
            text.text = description;
        }
    }
}
