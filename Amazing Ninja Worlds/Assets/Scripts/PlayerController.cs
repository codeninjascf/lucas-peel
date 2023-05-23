using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameManager gameManager;
    public float moveSpeed=5f;
    public float jumpForce=10f;
    public float groundDistanceThreshold=100f;
    public LayerMask whatIsGround;
    private bool _isGrounded;
    private Rigidbody2D _rigidbody;
    private bool _enabled;
    private Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody=GetComponent<Rigidbody2D>();
        _enabled = true;
    }

    private void FixedUpdate()
    {
        if (!_enabled) return;
        float movement=moveSpeed*Input.GetAxisRaw("Horizontal");
        _animator.SetBool("Moving", movement != 0);
        _rigidbody.position+=movement*Time.deltaTime*Vector2.right;

    }
    // Update is called once per frame
    void Update()
    {
        if (!_enabled) return;
        _isGrounded=Physics2D.Raycast(transform.position, Vector2.down, groundDistanceThreshold, whatIsGround);
        if(_isGrounded&&Input.GetButtonDown("Jump"))
        {
            _animator.SetBool("Jumping", true);
            _rigidbody.velocity=Vector2.up*jumpForce;
        }
        else
        {
            _animator.SetBool("Jumping", false);
        }
        _animator.SetBool("Falling", !_isGrounded);
    }
    public void Enable()
    {
        _enabled = true;
    }
    public void Disable()
    {
        _enabled = false;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Hazard"))
        {
            gameManager.KillPlayer();
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Checkpoint"))
        {
            gameManager.SetCheckpoint(other.transform);
        }
        else if(other.CompareTag("Collectible"))
        {
            gameManager.GotCollectible(other.transform);
            other.gameObject.SetActive(false);
        }
    }
    public void Disable()
    {
        _enabled = false;
        _animator.SetBool("Moving", false);
        _animator.SetBool("Jumping", false);
        _animator.SetBool("Falling", !_isGrounded);
    }
}
