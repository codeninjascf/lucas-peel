using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float jumpForce = 15f;
    private Rigidbody2D _rigidbody;
    private bool _canJump;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_rigidbody.velocity.y > -.01 && _rigidbody.velocity.y < .01)
        {
            _canJump = true;
        }
        else
        {
            _canJump = false;
        }
        if (_canJump && MobileInput.Tap)
        {
            _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
    void FixedUpdate()
    {
        float movement = moveSpeed * MobileInput.Tilt("Horizontal");
        transform.position += movement * Time.deltaTime * Vector3.right;
    }
}
