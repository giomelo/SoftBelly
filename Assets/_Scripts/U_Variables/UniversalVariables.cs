using System;
using _Scripts.Singleton;
using _Scripts.UI;

namespace _Scripts.U_Variables
{
    public class UniversalVariables : MonoSingleton<UniversalVariables>
    {
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
            private set {  }
        }
        public float SocialAlignment { get; set; } = 50; // pra perto de 0 esta alinhado aos humildes mais perto de 100 aos ricos
        public float Nivel { get; set; } = 1; // de  1 a 5 acaba o jogo
        
        public void Start()
        {
            DontDestroyOnLoad(this.gameObject);
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

    }
}
