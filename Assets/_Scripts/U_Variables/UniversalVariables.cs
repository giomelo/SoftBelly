using _Scripts.Singleton;
using _Scripts.UI;

namespace _Scripts.U_Variables
{
    public class UniversalVariables : MonoSingleton<UniversalVariables>
    {
        //Variavél de dinheiro para a loja e venda das curas para os pacientes
        public static float Money { get; set; } = 100;
        public static int Reputation { get; set; } = 30;
        public static float SocialAlignment { get; set; } = 50; // pra perto de 0 esta alinhado aos humildes mais perto de 100 aos ricos
        
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

            HUD_Controller.Instance.UpdateMoneyText();
        }
        
        public void ModifyReputation(int amount, bool add)
        {
            
            if (add)
                Reputation += amount;
            else
                Reputation -= amount;

            if (Reputation <= 0)
            {
                Reputation = 0;
            }
            
            HUD_Controller.Instance.UpdateReputationText();
        }
    }
}
