using System;
using _Scripts.Helpers;
using _Scripts.SaveSystem;
using _Scripts.Singleton;
using _Scripts.UI;
using UnityEngine;

namespace _Scripts.U_Variables
{
    public class UniversalVariables : MonoSingleton<UniversalVariables> , DataObject
    {
        public bool IsNewGame = true;
        //Variavél de dinheiro para a loja e venda das curas para os pacientes
        private float money = 100;

        public float Money
        {
            get => money;
            set { money = value; }
        }
        private int reputation = 30;
        public int Reputation
        {
            get => reputation;
            set {reputation = value; }
        }
        public float SocialAlignment { get; set; } = 50; // pra perto de 0 esta alinhado aos humildes mais perto de 100 aos ricos
        public int Nivel { get; set; } = 1; // de  1 a 5 acaba o jogo

        void Awake()
        {
            base.Awake();
            Load();
        }
        
        public void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        public void ModifyMoney(float amount, bool add)
        {
            if (add)
                Money += amount;
            else
                Money -= amount;
            if (Money < 0)
            {
                Money = 0;
            }
            HUD_Controller.Instance.UpdateMoneyText();
        }
        
        public void ModifyReputation(int amount, bool add)
        {

            if (add)
            {
                Reputation += amount;
                Nivel += 10;
                SetNivel();
            }
                
            else
                Reputation -= amount;

            if (Reputation <= 0)
            {
                Reputation = 0;
                //game over
            }
            HUD_Controller.Instance.UpdateReputationText();
        }

        public void ModifySocialAligment(int amount, bool add)
        {
            if (add)
                SocialAlignment += amount;
            else
                SocialAlignment -= amount;

            if (SocialAlignment <= 0)
            {
                SocialAlignment = 0;
            }
            
            HUD_Controller.Instance.UpdateAlignment();
        }


        private void SetNivel()
        {
            HUD_Controller.Instance.UpdateNivel();
        }

        public void Load()
        {
            SaveHudVariables d = (SaveHudVariables)Savesystem.Load(this);
            // if (IsNewGame)
            // {
            //     //Developer.ClearSaves();
            //     // clear save
            //     Debug.Log("NewGame");
            //     return;
            // }
            if (d != null)
            {
                // /*variavell*/ = /*variavel*/ = data./*variavel*/;
                Money = d.Money;
               Reputation = d.Reputation;
               SocialAlignment = d.SocialAlignment;
               Nivel = d.Nivel;

            }
        }

        public void Save()
        {
            SaveData data = new SaveHudVariables(Money, Reputation, SocialAlignment, Nivel);
            Savesystem.Save(data, this);
        }
    }
}
