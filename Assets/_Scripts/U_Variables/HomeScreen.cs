using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeScreen : MonoBehaviour
{
    public static HomeScreen homeScreen;
    // Codigo apenas para startar variaveis(COLOCAR APENAS NO START)
    public void Start()
    {
        homeScreen = this;
        UniversalVariables.Money = 100;
    }

}
