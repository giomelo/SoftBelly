using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniversalVariables : MonoBehaviour
{
    public static UniversalVariables universalVariables;

    //Variavél de dinheiro para a loja e venda das curas para os pacientes
    public static int Money = 100;
   public void Start()
   {
        universalVariables = this;
        //DontDestroyOnLoad(this.gameObject);
   }
}
