using System.Collections;
using UnityEngine;

namespace _Scripts
{
    public class MoveNeedle : MonoBehaviour
    {
        public float needleTime;
        Vector2 needlePos;

        private void Start()
        {
            needlePos = transform.localPosition;
            StartCoroutine(StartNeedle());
        }

        public IEnumerator StartNeedle()
        {
            while (true)
            {
                if (needlePos.x >= -0.34f)
                {
                    needlePos.x -= (0.7f / needleTime);
                    transform.localPosition = needlePos;
                } else
                {
                    yield return new WaitForSeconds(1);
                    Destroy(transform.parent.gameObject);
                }
                yield return new WaitForSeconds(1);
            }
        }
    }
}
