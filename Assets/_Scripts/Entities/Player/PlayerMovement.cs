using UnityEngine;

namespace _Scripts.Entities.Player
{
    /// <summary>
    /// Handle the player movment
    /// </summary>
    public class PlayerMovement : MonoBehaviour
    {
        private CharacterController _chController;

        private float _playerSpeed = 10f;
        private Vector3 _playerVelocity;

        private const float gravityValue = -12;

        // Start is called before the first frame update
        private void Start()
        {
            TryGetComponent(out _chController);
        }

        /// <summary>
        /// Move character controller based on axis "horizontal" and "vertical"
        /// </summary>
        /// <param name="horizontal"></param>
        /// <param name="vertical"></param>
        public void ProcessInput(float horizontal, float vertical)
        {
            var move = new Vector3(horizontal, 0, vertical);
            //move.y += gravityValue * Time.deltaTime;
            _chController.Move(move * Time.deltaTime * _playerSpeed);

            if (move != Vector3.zero)
            {
                //gameObject.transform.forward = move;
                Quaternion toRotation = Quaternion.LookRotation(move, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 720 * Time.deltaTime);
            }

        }
    }
}
