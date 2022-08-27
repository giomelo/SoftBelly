using System.Collections;
using System.Collections.Generic;
using _Scripts.Enums;
using _Scripts.Singleton;
using _Scripts.Systems.Inventories;
using _Scripts.Systems.Item;
using _Scripts.U_Variables;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


namespace _Scripts.Store
{
    public class StoreController : MonoSingleton<StoreController>
    {
        private StorageHolder StorageHolder;
        public GameObject StorageObject;
        
        
        Text textoBtn;
        
        [SerializeField] private List<ItemBehaviour> seeds;
        [SerializeField] private List<Button> buttons;
        [SerializeField] Button a;
        
        private void Start()
        {
            a.onClick.AddListener(UpdateItem);
            UpdateItem();
            
            //pick the plantation storage
            var storagesInScene = FindObjectsOfType<StorageHolder>();
            // ReSharper disable once SuggestVarOrType_SimpleTypes
            foreach (StorageHolder s in storagesInScene)
            {
                if (s.Storage.InventoryType == InventoryType.Plantation)
                {
                    StorageHolder = s;
                }
            }
        }
        
        public void UpdateItem()
        {
            var item = Random.Range(0, 3);
     
            switch (item)
            {
                case 0:
                    buttons[0].onClick.AddListener(delegate{ClickBuy(seeds[0],seeds[0].Price);});
                    NameAndPriceBtn(0,seeds[0].name, seeds[0].Price);
                    buttons[1].onClick.AddListener(delegate{ClickBuy(seeds[1],seeds[1].Price);});
                    NameAndPriceBtn(1,seeds[1].name, seeds[1].Price);
                    buttons[2].onClick.AddListener(delegate{ClickBuy(seeds[2],seeds[2].Price);});
                    NameAndPriceBtn(2,seeds[2].name, seeds[2].Price);
                    buttons[3].onClick.AddListener(delegate{ClickBuy(seeds[3],seeds[3].Price);});
                    NameAndPriceBtn(3,seeds[3].name, seeds[3].Price);
                    buttons[4].onClick.AddListener(delegate{ClickBuy(seeds[4],seeds[4].Price);});
                    NameAndPriceBtn(4,seeds[4].name, seeds[4].Price);
                    break;
                case 1:
                    buttons[1].onClick.AddListener(delegate{ClickBuy(seeds[0],seeds[0].Price);});
                    NameAndPriceBtn(1,seeds[0].name, seeds[0].Price);
                    buttons[0].onClick.AddListener(delegate{ClickBuy(seeds[1],seeds[1].Price);});
                    NameAndPriceBtn(0,seeds[1].name, seeds[1].Price);
                    buttons[3].onClick.AddListener(delegate{ClickBuy(seeds[2],seeds[2].Price);});
                    NameAndPriceBtn(3,seeds[2].name, seeds[2].Price);
                    buttons[2].onClick.AddListener(delegate{ClickBuy(seeds[3],seeds[3].Price);});
                    NameAndPriceBtn(2,seeds[3].name, seeds[3].Price);
                    buttons[4].onClick.AddListener(delegate{ClickBuy(seeds[4],seeds[4].Price);});
                    NameAndPriceBtn(4,seeds[4].name, seeds[4].Price);
                    break;
                case 2:
                    buttons[4].onClick.AddListener(delegate{ClickBuy(seeds[0],seeds[0].Price);});
                    NameAndPriceBtn(4,seeds[4].name, seeds[4].Price);
                    buttons[3].onClick.AddListener(delegate{ClickBuy(seeds[1],seeds[1].Price);});
                    NameAndPriceBtn(3,seeds[1].name, seeds[1].Price);
                    buttons[2].onClick.AddListener(delegate{ClickBuy(seeds[2],seeds[2].Price);});
                    NameAndPriceBtn(2,seeds[2].name, seeds[2].Price);
                    buttons[1].onClick.AddListener(delegate{ClickBuy(seeds[3],seeds[3].Price);});
                    NameAndPriceBtn(1,seeds[3].name, seeds[3].Price);
                    buttons[0].onClick.AddListener(delegate{ClickBuy(seeds[4],seeds[4].Price);});
                    NameAndPriceBtn(0,seeds[4].name, seeds[4].Price);
                    break;
            }
            
        }
        
        private void NameAndPriceBtn(int index,string name, float price)
        {
            textoBtn = buttons[index].transform.GetChild(0).GetComponent<Text>();
            textoBtn.text = name + " - Price: " + price;
        }

     
        public void ClickBuy(ItemBehaviour _item, float price)
        {
            
            if (UniversalVariables.Money >= price)
            {
                UniversalVariables.Instance.ModifyMoney(price, false);
                ControllerMoneyTXT.controllerMoneyTxt.TxtMoney.text = "Money: " + UniversalVariables.Money;
                ControllerMoneyTXT.controllerMoneyTxt.PainelAviso.SetActive(false);
                StoreController.Instance.AddItem(_item);
            }
            else
            {
                ControllerMoneyTXT.controllerMoneyTxt.TxtAvisoSemMoney.text = "Insufficient money!";
                ControllerMoneyTXT.controllerMoneyTxt.PainelAviso.SetActive(true);
                StartCoroutine(EsperarAviso());
            }
        }
        private IEnumerator EsperarAviso()
        {
            yield return new WaitForSeconds(1f);
            ControllerMoneyTXT.controllerMoneyTxt.PainelAviso.SetActive(false);
        }
        public void AddItem(ItemBehaviour item)
        {
            StorageHolder.Storage.AddItem(1,item);
        }
    }
}