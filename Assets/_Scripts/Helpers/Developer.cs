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
            string path = Application.persistentDataPath;
            //File.SetAttributes(path, FileAttributes.Normal);
            //File.Delete(path);
            DirectoryInfo d = new DirectoryInfo(path);
            
            foreach (var file in d.GetFiles("*.txt"))
            {
               // Directory.Move(file.FullName, path + "\\TextFiles\\" + file.Name);
                Debug.Log(file.FullName);
                File.Delete(file.FullName);
            }
        }
    }
}
