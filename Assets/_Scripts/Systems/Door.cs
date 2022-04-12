using UnityEngine;

namespace _Scripts.Systems
{
    public class Door : MonoBehaviour
    {
        [SerializeField]
        private string scene;
        private void OnTriggerEnter(Collider other)
        {
            ScreenFlow.Instance.LoadScene(scene);
        }
    }
}
