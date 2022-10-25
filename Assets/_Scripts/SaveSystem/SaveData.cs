using System.Collections;
using System.Collections.Generic;
using _Scripts.U_Variables;
using UnityEngine;

public class SaveData
{
    private float money;
    private int reputation;

    public float Money
    {
        get => money;
        set { money = value; }
    }
    public float Reputation
    {
        get => reputation;
        set { money = value; }
    }
    
    public SaveData(_Scripts.U_Variables.UniversalVariables universalVariables)
    {
        Money = universalVariables.Money;
        Reputation = universalVariables.Reputation;
    }
}
