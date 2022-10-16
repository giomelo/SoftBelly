using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PosComSlider : MonoBehaviour
{
    public GameObject info;
    public Image info_icon;
    public Text info_longName;
    public Text info_description;
    public TextMeshProUGUI info_cures;
    public Text info_value;

    public GameObject[] items;
    Vector3 vec;

    private void Start()
    {
        vec = transform.position;
        for (int i = 0; i < items.Length; i++)
        {
            ItemInterface inter = items[i].GetComponent<ItemInterface>();
            if (inter)
            {
                if (i == 0) { items[i].GetComponent<ItemBehavior>().clicked = true; }
                Vector3 posToLocal = transform.TransformPoint(-56, 50 + i * -160, 0);
                GameObject newItem = Instantiate(items[i], posToLocal, Quaternion.identity, this.transform);
                newItem.name = "[" + (i+1) + "] - " + newItem.name.Substring(2, newItem.name.Length - 9);
            }
        }

        if(items[0])
            AtualizaInterface(items[0]);
    }

    public void MudaPosY(float pos)
    {
        vec.y = 250.8667f + pos; //ugh
        transform.SetPositionAndRotation(vec, Quaternion.identity);
    }

    public void AtualizaInterface(GameObject item)
    {
        if (item.GetComponent<ItemInterface>())
        {
            info.SetActive(true);
            ItemInterface interf = item.GetComponent<ItemInterface>();

            info_icon.sprite = interf.icon;
            info_longName.text = interf.longName;
            info_description.text = interf.description;
            info_cures.text = interf.cures;
            info_value.text = "$ " + interf.value;
        }
    }
}
