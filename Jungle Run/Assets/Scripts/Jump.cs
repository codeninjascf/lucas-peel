using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public float jumpForce = 4.5f;
    public float fallMultiplier = 2.5f;
    public float groundDistanceThreshold = 0.005f;
    public LayerMask whatIsGround;
    public GameObject particleTrail;
    private bool _isGrounded;
    private Rigidbody _rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        _isGrounded = Physics.Raycast(_rigidbody.transform.position, Vector3.down, groundDistanceThreshold, whatIsGround);
        if(_isGrounded&&Input.GetButton("Jump"))
        {
            _rigidbody.velocity = Vector3.up * jumpForce;
        }
        if(_rigidbody.velocity.y<0)
        {
            _rigidbody.velocity += (fallMultiplier - 1) * Time.deltaTime * Physics.gravity;
            particleTrail.SetActive(_isGrounded);
            if(_rigidbody.position.y<-0.5)
            {
                GameManager.GameOver = true;
            }
        }
    }
}
