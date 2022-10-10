using UnityEngine;

namespace _Scripts.Entities.Player
{
    /// <summary>
    /// Handle the player movment
    /// </summary>
    public class PlayerMovement : MonoBehaviour
    {
        private CharacterController _chController;
        public Animator animator;

        private float _playerSpeed = 10f;
        private Vector3 _playerVelocity;
        [SerializeField]
        private float gravity = 2f;

        private const float gravityValue = -12;

        // Start is called before the first frame update
        private void Start()
        {
            TryGetComponent(out _chController);
            animator = GetComponent<Animator>();
        }

        /// <summary>
        /// Move character controller based on axis "horizontal" and "vertical"
        /// </summary>
        /// <param name="horizontal"></param>
        /// <param name="vertical"></param>
        public void ProcessInput(float horizontal, float vertical)
        {
            var move = new Vector3(horizontal, gravity, vertical);
            //move.y += gravityValue * Time.deltaTime;
            _chController.Move(move * Time.deltaTime * _playerSpeed);
            Vector3 angle = new Vector3(move.x, 0, move.z);                     
            if (move == Vector3.zero) return;
            //gameObject.transform.forward = move;
            Quaternion toRotation = Quaternion.LookRotation(angle, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 720 * Time.deltaTime);
        }

        void FixedUpdate()
        {
            if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) )
            {
                animator.SetBool("Walk", true);
                animator.SetBool("Idle", false);
            }else if(Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
            {
                animator.SetBool("Walk", false);
                animator.SetBool("Idle", true);
            }
        }
    }
}
