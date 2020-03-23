
using System;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float movementSpeed = 1;
    public float jumpHigh = 50;
    
    public CharacterController controller;
    private float gravity = -9.81f;
    
    private bool isGrounded;
    private Vector3 velocity;

    private Animator animator;
    
    

    void Start()
    {
        isGrounded = true;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump(); //TODO
    }

    private void Move()
    {
        var z = Input.GetAxis ("Vertical");
        var x = Input.GetAxis ("Horizontal");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * movementSpeed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void Jump()
    {
        if (isGrounded)
        {
            if ( Input.GetButtonDown ("Jump") ) { 
                // transform.Translate (Vector3.up * high);
                Vector3 jump = transform.up;
                controller.Move(jump * jumpHigh * Time.deltaTime);
                Debug.Log("jump");
                isGrounded = false;
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Collision with ground detected");
            isGrounded = true;
        }

    }
    
}
