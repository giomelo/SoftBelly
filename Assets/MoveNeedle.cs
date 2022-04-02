using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveNeedle : MonoBehaviour
{
    public float needleTime;

    public GameObject check;

    Vector2 needlePos;

    private void Start()
    {
        needlePos = transform.localPosition;
        StartCoroutine(StartNeedle());
    }

    public IEnumerator StartNeedle()
    {
        yield return new WaitForSeconds(0.9f);
        while (true)
        {
            if (needlePos.x >= -0.34f)
            {
                needlePos.x -= (0.7f / needleTime);
                transform.localPosition = needlePos;
            } else
            {
                Animator anim = transform.parent.gameObject.GetComponent<Animator>();
                check.SetActive(true);
                anim.Play("default.TH_CheckFades");
                yield return new WaitForSeconds(5);
                anim.Play("default.TH_FadeOut");
                yield return new WaitForSeconds(1);
                Destroy(transform.parent.gameObject);
            }
            yield return new WaitForSeconds(1);
        }
    }
}
