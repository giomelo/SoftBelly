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
        vLerp = transform.position;

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
        vLerp.y = transform.position.y;
        if (transform.localPosition.x != -12 || transform.localPosition.x > -56)
        {
            if(clicked)
                vLerp.x = Mathf.Lerp(vLerp.x, 174.1833f, Time.deltaTime * moveSpeed);
            else
                vLerp.x = Mathf.Lerp(vLerp.x, 145.29f, Time.deltaTime * moveSpeed);
            transform.SetPositionAndRotation(vLerp, Quaternion.identity);
        }
    }
}
