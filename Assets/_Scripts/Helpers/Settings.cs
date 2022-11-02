using UnityEngine;
using TMPro;

namespace _Scripts.Helpers
{
    public class Settings : MonoBehaviour
    {
        public TMP_Dropdown dropdown;

        private void Start()
        {
            dropdown.value = QualitySettings.GetQualityLevel();
        }
        
        public void Graphics(int value)
        {
            QualitySettings.SetQualityLevel(value);
        }
    }
}
