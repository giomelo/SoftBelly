using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using _Scripts.U_Variables;
using UnityEngine;

namespace _Scripts.SaveSystem
{
    public class Savesystem
    {
        public static void Save(SaveData universalVariables, DataObject obj)
        {
            UniversalVariables.Instance.IsNewGame = false;
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + obj.GetType();
            FileStream stream = new FileStream(path, FileMode.Create);

            //SaveData data = new SaveData(universalVariables);
            Debug.Log(universalVariables);
            formatter.Serialize(stream, universalVariables);
            stream.Close();
        }

        public static SaveData Load(DataObject obj)
        {
            string path = Application.persistentDataPath + obj.GetType();
            if (File.Exists(path))
            {
                Debug.Log("Path " + path);
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);

                SaveData data = formatter.Deserialize(stream) as SaveData;

                stream.Close();
                return data;
            }

            Debug.LogError("Save não encontrado em " + path);
            return null;

        }

        private bool CheckIfSaveExists()
        {
            return false;
        }

        public static void ClearSave()
        {
            //string path = Application.persistentDataPath + obj.GetType();
           // File.Delete(path);
   
            DirectoryInfo di = new DirectoryInfo(Application.persistentDataPath);
            
            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete(); 
            }
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                dir.Delete(true); 
            }
            
        }
    }
}
