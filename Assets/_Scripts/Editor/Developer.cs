using System.IO;
using UnityEditor;
using UnityEngine;

namespace _Scripts.Editor
{
    /// <summary>
    /// Class for doing stuff in unity editor
    /// </summary>
    public static class Developer
    {
        [MenuItem("Developer/Clear Saves")]
        public static void ClearSaves()
        {
            string[] files = Directory.GetFiles(Application.persistentDataPath);
            Debug.Log(files.Length);
            Debug.Log(Application.persistentDataPath);
            foreach(string s in files){
                File.Delete(s);
                Debug.Log("Apagado: " + s);
              
            }
        }
    }
}
