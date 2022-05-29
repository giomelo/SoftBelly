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
            // var move = new Vector3(horizontal, 0, vertical);
            // move.y += gravityValue * Time.deltaTime;
            // _chController.Move(move * Time.deltaTime * _playerSpeed);
            // //gameObject.transform.forward = move;
        }
        
        void Move()
        {
            
            _chController.Move( Time.deltaTime * _playerSpeed * Input.GetAxis("Vertical") * transform.forward);

            transform.Rotate(0,Input.GetAxis("Horizontal") * 180 * Time.deltaTime,0);
  
        }
        private void Update()
        {
            Move();
        }
    }
}
