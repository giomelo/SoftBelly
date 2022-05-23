using _Scripts.Singleton;
using _Scripts.UI;

namespace _Scripts.U_Variables
{
    public class UniversalVariables : MonoSingleton<UniversalVariables>
    {

        //Variavél de dinheiro para a loja e venda das curas para os pacientes
        public static int Money = 100;

        public void Start()
        {
            DontDestroyOnLoad(this.gameObject);
        }

        public void ModifyMoney(int amount, bool add)
        {
            if (add)
                Money += amount;
            else
                Money -= amount;

            HUD_Controller.Instance.UpdateMoneyText();
        }
    }
}
