using UnityEngine;
using UnityEngine.UI;

public class ItemBehavior : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 8;
    [SerializeField]
    ItemInterface interf;

    public bool clicked = false;
    Vector3 vLerp;

    private void Start()
    {
        vLerp = transform.position;

        Image img = gameObject.GetComponentsInChildren<Image>()[1];
        Text txt = gameObject.GetComponentInChildren<Text>();
        img.sprite = interf.icon;
        txt.text = interf.shortName;

        Button btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(ButtonListener);
    }

    void ButtonListener()
    {
        PosComSlider itemDesc = GameObject.Find("Itens").GetComponent<PosComSlider>();
        itemDesc.AtualizaInterface(this.gameObject);
    }

    public void BtnClick()
    {
        foreach (ItemBehavior item in transform.parent.GetComponentsInChildren<ItemBehavior>())
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
