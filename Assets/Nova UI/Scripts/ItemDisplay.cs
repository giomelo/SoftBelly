using UnityEngine;
using UnityEngine.UI;
using _Scripts.Systems.Item;

public class ItemDisplay : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 8;

    public ItemBehaviour item;
    public bool clicked = false;
    Vector3 vLerp;

    private void Start()
    {
        vLerp = transform.localPosition;

        Image img = gameObject.GetComponentsInChildren<Image>()[1];
        Text txt = gameObject.GetComponentInChildren<Text>();
        img.sprite = item.ImageDisplay;
        txt.text = item.ItemId;

        Button btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(ButtonListener);
    }

    void ButtonListener()
    {
        NewStoreInterface itemDesc = GameObject.Find("Itens").GetComponent<NewStoreInterface>();
        itemDesc.AtualizaInterface(gameObject);
    }

    public void BtnClick()
    {
        foreach (ItemDisplay item in transform.parent.GetComponentsInChildren<ItemDisplay>())
            if (item != this) { item.clicked = false; }
        clicked = !clicked;
    }

    public void Update()
    {
        vLerp.y = transform.localPosition.y;
        if (transform.localPosition.x != -12 || transform.localPosition.x > -56)
        {
            if(clicked)
                vLerp.x = Mathf.Lerp(vLerp.x, -12, Time.deltaTime * moveSpeed);
            else
                vLerp.x = Mathf.Lerp(vLerp.x, -56, Time.deltaTime * moveSpeed);
            transform.localPosition = vLerp;
        }
    }
}
