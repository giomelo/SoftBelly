using TMPro;
using UnityEngine;
using UnityEngine.UI;
using _Scripts.Systems.Item;
using _Scripts.Systems.Plants.Bases;

public class NewStoreInterface : MonoBehaviour
{
    public GameObject info;
    public ItemBehaviour info_item;
    public Image info_icon;
    public Text info_longName;
    public Text info_description;
    public Text info_extraInfo;
    public TextMeshProUGUI info_extrasText;
    public Text info_value;
    [SerializeField]
    SetSliderMax slider;

    public GameObject[] items;
    Vector3 vec;
    float YPos;

    private void Start()
    {
        YPos = transform.localPosition.y;
        vec = transform.localPosition;
        for (int i = 0; i < items.Length; i++)
        {
            ItemDisplay item = items[i].GetComponent<ItemDisplay>();
            if (item)
            {
                if (i == 0) { item.clicked = true; }
                Vector3 posToLocal = transform.TransformPoint(-56, 50 + i * -160, 0);
                GameObject newItem = Instantiate(items[i], posToLocal, Quaternion.identity, this.transform);
                newItem.name = "[" + (i+1) + "] - " + newItem.name.Substring(2, newItem.name.Length - 9);
            }
        }

        if (items[0]) 
        {
            if (items.Length >= 5) { slider.SetMax(); }
            AtualizaInterface(items[0]);
        }
            
    }

    public void MudaPosY(float pos)
    {
        vec.y = YPos + pos;
        transform.localPosition = vec;
        //transform.SetPositionAndRotation(vec, Quaternion.identity);
    }

    public void AtualizaInterface(GameObject item)
    {
        if (item.GetComponent<ItemDisplay>())
        {
            info.SetActive(true);
            ItemDisplay display = item.GetComponent<ItemDisplay>();

            info_item = display.item;                                                           //Item
            info_icon.sprite = display.item.ImageDisplay;                                       //Imagem

            if (display.item.ItemLongID != "")                                                  //Nome longo, se necess�rio (para �tens com nomes maiores que seu display na loja)
                info_longName.text = display.item.ItemLongID;
            else
                info_longName.text = display.item.ItemId;

            if (display.item.ShopDescription != "")
                info_description.text = display.item.ShopDescription;                           //Descri��o do item pr�pria da loja. Na falta desta usamos a descri��o padr�o
            else
                info_description.text = display.item.ItemProprietiesDescription;    

            info_value.text = "$ " + display.item.Price;                                        //Pre�o

            switch (display.item.ItemType)                                                      //Info extra (ex. o que uma planta cura)
            {
                case _Scripts.Enums.ItemType.Plant:     //Plantas
                    info_extraInfo.text = "Cura para:";
                    PlantBase plant = (PlantBase)display.item;
                    info_extrasText.text = "";
                    foreach (SymptomsNivel symptoms in plant.MedicalSymptoms)
                    {
                        info_extrasText.text += ">" + symptoms.Symptoms.ToString() + "  ";
                    }
                    break;


                default:
                    info_extraInfo.text = "";
                    info_extrasText.text = "";
                    return;
            }

        }
    }
}
