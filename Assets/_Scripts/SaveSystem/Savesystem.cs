
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class Savesystem
{
    public static void Save(_Scripts.U_Variables.UniversalVariables universalVariables)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/character.features";
        FileStream stream = new FileStream(path, FileMode.Create);

        SaveData data = new SaveData(universalVariables);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static SaveData Load()
    {
        string path = Application.persistentDataPath + "/character.features";
        if (File.Exists(path))
        {
            Debug.Log("Path " + path);


            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveData data = formatter.Deserialize(stream) as SaveData;

            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("Save de Personagem não encontrado em " + path);
            return null;
        }

    }
}
