using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveandLoad : MonoBehaviour
{
    public void Save()
    {
        //Savesystem.Save(this);
    }

    public void Load()
    {
        SaveData data = Savesystem.Load();
        if (data != null)
        {
            // /*variavell*/ = /*variavel*/ = data./*variavel*/;
        }
    }
}
