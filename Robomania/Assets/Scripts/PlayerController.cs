using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float jumpForce = 10f;

    private bool _facingRight;
    private bool _isGrounded;
    
    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;
    private Camera _camera;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _camera = Camera.main;
    }

    private void Update()
    {
        float movement = Input.GetAxis("Horizontal");
        _rigidbody.velocity = new Vector2(movement * moveSpeed, _rigidbody.velocity.y);

        float clampedX = Mathf.Clamp(transform.position.x, -GameManager.ScreenBounds.x, GameManager.ScreenBounds.x);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
        
        _animator.SetBool("IsRunning", movement != 0);

        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _rigidbody.AddForce(jumpForce * Vector2.up, ForceMode2D.Impulse);
        }

        Vector3 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
        if (mousePos.x < transform.position.x && _facingRight) _facingRight = false;
        else if (mousePos.x > transform.position.x && !_facingRight) _facingRight = true;
        _spriteRenderer.flipX = !_facingRight;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            _isGrounded = true;
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            StartCoroutine(GameManager.PlayerEntity.Damage(10));
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            _isGrounded = false;
        }
    }
}
