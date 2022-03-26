using Systems.Plantation;
using UnityEditor;
using UnityEngine;

namespace _Scripts.Editor
{
    /// <summary>
    /// Editor script for instantiate grid without play mode
    /// </summary>

    [CustomEditor(typeof(GridSystem))]
    public class GridEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GridSystem grid = (GridSystem) target;

            if (GUILayout.Button("GenerateGrid"))
            {
                grid.CreatGrid();
            }
        }
    }
}
