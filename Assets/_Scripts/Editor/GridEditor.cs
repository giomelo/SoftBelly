using System.Collections;
using System.Collections.Generic;
using Systems.Plantation;
using UnityEngine;
using UnityEditor;

/// <summary>
/// Editor script for instantiate grid without play mode
/// </summary>

[CustomEditor(typeof(GridSystem))]
public class GridEditor : Editor
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
