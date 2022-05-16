using System;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.Helpers
{
    public class ProgressBar : MonoBehaviour
    {
        #if UNITY_EDITOR
        [MenuItem("GameObject/UI/LinearProgressBar")]
        public static void AddLinearProgressBar()
        {
            GameObject obj = Instantiate(Resources.Load<GameObject>("Prefabs/UI/ProgressBar"));
            obj.transform.SetParent(Selection.activeGameObject.transform, false);  
        }
        [MenuItem("GameObject/UI/RadialProgressBar")]
        public static void AddRadialProgressBar()
        {
            GameObject obj = Instantiate(Resources.Load<GameObject>("Prefabs/UI/Radial"));
            obj.transform.SetParent(Selection.activeGameObject.transform, false);  
        }
        
        #endif
        
        [SerializeField]
        private float Maximum = 100f;
        [SerializeField]
        private float Minimum = 0f;
        public float Current;
        [SerializeField]
        private Image _mask;
        [SerializeField]
        private Image fill;
        [SerializeField]
        private Color color;

        public void GetCurrentFill()
        {
            float currentOffset = Current - Minimum;
            float maximumOffset = Maximum - Minimum;
            float fillAmount = currentOffset / maximumOffset;
            _mask.fillAmount = fillAmount;
        }

        private void FillColor()
        {
            fill.color = color;
        }

        public void SetCurrentValue(float value)
        {
            Current = value;
            GetCurrentFill();
        }
        
        private void Update()
        {
        }
    }
}
