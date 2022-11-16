using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using _Scripts.Systems.Inventories;
using _Scripts.U_Variables;
using UnityEngine;

namespace _Scripts.SaveSystem
{
    public class Savesystem
    {
        public static void Save(SaveData universalVariables, string obj)
        {
           // UniversalVariables.Instance.IsNewGame = false;
            Directory.CreateDirectory (Application.persistentDataPath + "FilesToDelete"); 
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + obj;
            FileStream stream = new FileStream(path, FileMode.Create);

            //SaveData data = new SaveData(universalVariables);
            Debug.Log(universalVariables);
            formatter.Serialize(stream, universalVariables);
            stream.Close();
        }
        public static void SaveJson(SaveData universalVariables, string obj)
        {
          //  UniversalVariables.Instance.IsNewGame = false;
            string s = JsonUtility.ToJson(universalVariables);
            
            string path = Application.persistentDataPath + obj.GetType();
            File.WriteAllText(path + "/save.json",s);
        }
        public static SaveData LoadJson(string obj)
        {
            string path = Application.persistentDataPath + obj.GetType() + "/save.json";
            if (File.Exists(path))
            {
                string s = File.ReadAllText(path);
                SaveData saved = JsonUtility.FromJson<SaveData>(s);
                return saved;
            }
        
            Debug.LogError("Save não encontrado em " + path);
            return null;
        }

        public static SaveData Load(string obj)
        {
            string path = Application.persistentDataPath + obj;
            if (File.Exists(path))
            {
                Debug.Log("Path " + path);
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);

                SaveData data = formatter.Deserialize(stream) as SaveData;

                stream.Close();
                return data;
            }

            Debug.LogWarning("Save não encontrado em " + path);
            return null;

        }
        

        public static bool CheckIfSaveExists()
        {
            string[] filePaths = Directory.GetFiles(Application.persistentDataPath);
            return filePaths.Length > 0;
        }

        public static void ClearSave()
        {
            string[] filePaths = Directory.GetFiles(Application.persistentDataPath);
            foreach (string filePath in filePaths)
                File.Delete(filePath);
        }
    }
}
