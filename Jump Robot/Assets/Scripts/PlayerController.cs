using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 2;
    public float moveLimit = 2.4f;
    public float minJump = 5;
    public float maxJump = 6;
    public float groundRadius = 0.01f;

    public Transform groundDetect;
    public LayerMask whatIsGround;

    private LayerMask _currentLayer;              
    private bool _isGrounded;
    private float _currentJumpForce;
    private Rigidbody2D _rigidbody;
    private Vector2 _direction = new(1, 0);
    private bool _canJump = true;
    private AudioSource _sound;
    
    private ManagerVariables _managerVariables;

    private void OnEnable()
    {
        _managerVariables = Resources.Load("ManagerVariablesContainer") as ManagerVariables;
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>(); 
        _sound = GetComponent<AudioSource>();  
        _currentLayer = gameObject.layer;     
        CameraFollow.Instance.InitialisePlayerSettings(); 
    }

    private void Update()
    {
        if (GameManager.Instance.gameOver)
        {
            _rigidbody.gravityScale = 0;
            return;
        }

        gameObject.layer = CreateRay() && _rigidbody.velocity.y <= 0 ? _currentLayer : 8;
        
        if (_rigidbody.velocity.y <= 0)
        {
            _isGrounded = Physics2D.OverlapCircle(groundDetect.transform.position, groundRadius, whatIsGround);
            if (_isGrounded)
            {
                _canJump = true;
                _currentJumpForce = 0;
            }
        }

        Movement();
    }

    private bool CreateRay()
    {
        RaycastHit2D hit = Physics2D.Raycast(groundDetect.position, Vector2.down, groundRadius, whatIsGround);
        Debug.DrawRay(groundDetect.position, Vector2.down * 0.1f, Color.red); //to show on the game scene

        return hit.collider != null && hit.collider.CompareTag("Platform");
    }

    void Movement()
    {
        // Horizontal Movement
        if (transform.position.x >= moveLimit)
        {
            _direction = new Vector2(-1, 0);
            transform.localScale = new Vector2(_direction.x, 1);
        }
        else if (transform.position.x <= -moveLimit)
        {
            _direction = new Vector2(1, 0);
            transform.localScale = new Vector2(_direction.x,1);
        }
        
        _rigidbody.velocity = new Vector2(speed * _direction.x, _rigidbody.velocity.y);

        // Jumping
        if (Input.GetMouseButtonDown(0) && _canJump && GameManager.Instance.gameStarted)
        {
            _sound.PlayOneShot(_managerVariables.jumpSound);
            _currentJumpForce = maxJump;
        }
        if (Input.GetMouseButton(0) && _canJump && GameManager.Instance.gameStarted)
        {
            if (_currentJumpForce > minJump)
            {
                _currentJumpForce = Math.Max(_currentJumpForce - Time.deltaTime * 1.8f, minJump);
            }
            else _canJump = false;
       
            _isGrounded = false;
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
            _rigidbody.AddForce(new Vector2(0, _currentJumpForce), ForceMode2D.Impulse);
        }
        if (Input.GetMouseButtonUp(0) && _canJump && GameManager.Instance.gameStarted)
        {
            _canJump = false;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            _sound.PlayOneShot(_managerVariables.gameOverSound);
            GameManager.Instance.gameOver = true;
        }
    }
}
