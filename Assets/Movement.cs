
using System;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float rotationSpeed = 1;

    public float movementSpeed = 1;
    //public float jumpHigh = 1;
    private bool _onGround;
    public float high;
    private Rigidbody _rb;

    void Start()
    {
        _onGround = true;
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
    }

    private void Move()
    {
        var vertical = Input.GetAxis ("Vertical");
        if (vertical != 0f ) {
            transform.Translate (vertical * Vector3.forward * Time.deltaTime * movementSpeed);
            Debug.Log("vertical " + vertical);
        }

        var horizontal = Input.GetAxis ("Horizontal");
        if (horizontal != 0f) {
            transform.Rotate (Vector3.up, 50f * Time.deltaTime * horizontal * rotationSpeed);
            Debug.Log("horizontal movement");
        }
    }

    private void Jump()
    {
        if (_onGround)
        {
            if ( Input.GetButtonDown ("Jump") ) { 
                transform.Translate (Vector3.up * high);
                _onGround = false;
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        other.gameObject.CompareTag("Ground");
        _onGround = true;
    }
}
