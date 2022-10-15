using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehavior : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 8;
    public bool clicked = false;
    Vector3 vLerp;

    private void Start()
    {
        vLerp = transform.position;
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
        if (transform.localPosition.x != -5 || transform.localPosition.x > -56)
        {
            if(clicked)
                vLerp.x = Mathf.Lerp(vLerp.x, 178.78f, Time.deltaTime * moveSpeed);
            else
                vLerp.x = Mathf.Lerp(vLerp.x, 145.29f, Time.deltaTime * moveSpeed);
            transform.SetPositionAndRotation(vLerp, Quaternion.identity);
        }
    }
}
