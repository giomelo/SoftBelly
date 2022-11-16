using System.IO;
using UnityEditor;
using UnityEngine;

namespace _Scripts.Helpers
{
    /// <summary>
    /// Class for doing stuff in unity editor
    /// </summary>
    public static class Developer
    {
        [MenuItem("Developer/Clear Saves")]
        public static void ClearSaves()
        {
            string[] filePaths = Directory.GetFiles(Application.persistentDataPath);
            Debug.Log(filePaths.Length);
            foreach (string filePath in filePaths)
                File.Delete(filePath);
        }
    }
}
