
using System;
using System.IO;
using _Scripts.Helpers;
using _Scripts.Singleton;
using _Scripts.Systems.Patients;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.UI
{
    public class UIAssistant : MonoSingleton<UIAssistant>
    {
        public TextWriterSingle textWriterSingle;

        public void OnClickDialog()
        {
            if (textWriterSingle != null && textWriterSingle.IsActive())
            {
                textWriterSingle.WriteAllAndDestroy();
            }
            else
            {
                //gerar uma possivel nova mensagem
            }
        }
    }
}