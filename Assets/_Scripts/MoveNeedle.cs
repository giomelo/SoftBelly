using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveNeedle : MonoBehaviour
{
    public float needleTime = 0;

    public GameObject check;

    Vector2 needlePos;
    private Animator anim;

    private void Start()
    {
        needlePos = transform.localPosition;
        anim = transform.parent.gameObject.GetComponent<Animator>();
    }

    public IEnumerator StartNeedle(float time)
    {
        yield return new WaitForSeconds(0.9f);

            if (needlePos.x >= -0.34f)
            {
                needlePos.x -= (0.7f / time);
                transform.localPosition = needlePos;
                StartCoroutine(StartNeedle(time));
            } else
            {
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
