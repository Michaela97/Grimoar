
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

    public Animator animator;
    private int isRunningHash = Animator.StringToHash("Speed");
    private int attackHash = Animator.StringToHash("Attack");
    private int deadHash = Animator.StringToHash("Dead");
    
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
        Attack();
    }

    private void Move()
    {
        var z = Input.GetAxis ("Vertical");
        var x = Input.GetAxis ("Horizontal");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * movementSpeed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        SetRunningAnimation(z, x);
    }

    private void SetRunningAnimation(float z, float x)
    {
        var value = z + x; 
        
        if (value < 0f)
        {
            value = -1 * value + 1f;
        }
        
        
        Debug.Log(value);
        animator.SetFloat(isRunningHash, value);
    }

    private void Attack()
    {
        var input = Input.GetMouseButton(0);
        if (input)
        {
            animator.SetBool(attackHash, true);
        }
        else
        {
            animator.SetBool(attackHash, false);
        }
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

        else if (other.gameObject.name == "Enemy")
        {
            Debug.Log("Collision with Enemy detected");
            isGrounded = true;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Enemy")
        {
            Debug.Log("Player triggered by enemy");
        }
    }
}
