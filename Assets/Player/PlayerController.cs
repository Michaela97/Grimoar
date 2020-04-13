using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        public float movementSpeed = 1;
        public float jumpHigh = 50;

        public CharacterController controller;
        private float gravity = -9.81f;

        private bool isGrounded;
        private Vector3 velocity;

        public Animator animator;
        private int isRunningHash = Animator.StringToHash("Speed");
        private AudioManager _audioManager;
        
        private int i = 0;
        

        void Start()
        {
            isGrounded = true;
            animator = GetComponent<Animator>();
            controller = GetComponent<CharacterController>();
            _audioManager = FindObjectOfType<AudioManager>();
        }

        // Update is called once per frame
        void Update()
        {
            // if (!PlayerCombat.PlayerIsDead) 
            // {
                Move();
                Jump(); //TODO
          // }
            
        }

        private void Move()
        {
            var z = Input.GetAxis("Vertical");
            var x = Input.GetAxis("Horizontal");

            Vector3 move = transform.right * x + transform.forward * z;

            controller.Move(move * movementSpeed * Time.deltaTime);
            
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);

            SetRunningAnimation(move.magnitude);
        }

        private void SetRunningAnimation(float magnitude)
        {
            animator.SetFloat(isRunningHash, magnitude);
    
            if (magnitude>0)
            {
                _audioManager.Play("PlayerFootSteps");
                i++;
            }
            else
            {
                _audioManager.Stop("PlayerFootSteps");
            }
        }

        private void Jump()
        {
            if (isGrounded)
            {
                if (Input.GetButtonDown("Jump"))
                {
                    // transform.Translate (Vector3.up * high);
                    Vector3 jump = transform.up;
                    controller.Move(jump * jumpHigh * Time.deltaTime);
                    Debug.Log("jump");
                    isGrounded = false;
                }
            }
        }
        
    }
}