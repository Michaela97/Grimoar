using System;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float movementSpeed;
        [SerializeField] private float jumpHigh;
        [SerializeField] private CharacterController controller;
        [SerializeField] private Animator animator;

        private float gravity = -9.81f;
        private bool _isGrounded;
        private Vector3 _velocity;

        private AudioManager _audioManager;

        void Start()
        {
            _isGrounded = true;
            animator = GetComponent<Animator>();
            controller = GetComponent<CharacterController>();
            _audioManager = FindObjectOfType<AudioManager>();
        }

        void Update()
        {
            Move();
            Jump();
        }

        private void Move()
        {
            var z = Input.GetAxis("Vertical");
            var x = Input.GetAxis("Horizontal");

            var move = transform.right * x + transform.forward * z;

            controller.Move(move * movementSpeed * Time.deltaTime);

            _velocity.y += gravity * Time.deltaTime;
            controller.Move(_velocity * Time.deltaTime);

            SetRunningAnimation(move.magnitude);
        }

        private void SetRunningAnimation(float magnitude)
        {
            animator.SetFloat(PlayerAnimHash.isRunningHash, magnitude);

            if (magnitude > 0)
            {
                _audioManager.Play("PlayerFootSteps");
            }
            else
            {
                _audioManager.Stop("PlayerFootSteps");
            }
        }

        private void Jump()
        {
            if (_isGrounded && Input.GetButtonDown("Jump"))
            {
                var jump = transform.up *  jumpHigh;
                controller.Move(jump * Time.deltaTime);
                _isGrounded = false;
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                _isGrounded = true;
            }
        }
    }
}