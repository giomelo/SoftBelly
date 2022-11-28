using System;
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
          
           BinaryFormatter formatter = new BinaryFormatter();
           if (!Directory.Exists(Application.persistentDataPath + "/Saves"))
           {
               Directory.CreateDirectory(Application.persistentDataPath + "/Saves");
           }
           string path = Application.persistentDataPath +"/Saves" + obj;
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
            string path = Application.persistentDataPath +"/Saves" + obj;
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
            string[] filePaths = Directory.GetFiles( Application.persistentDataPath);
            Console.WriteLine(filePaths.Length);
            return filePaths.Length > 2;
        }

        public static void ClearSave()
        {
            try
            {
                if (!CheckIfSaveExists()) return;
     
                string[] files = Directory.GetFiles(Application.persistentDataPath);
        
                foreach(string s in files){
                    File.Delete(s);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
       
        }
    }
}
