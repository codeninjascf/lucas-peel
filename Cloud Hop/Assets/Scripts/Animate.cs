using UnityEngine;

public class Animate : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    
    private bool _running;
    private bool _onGround;
    private bool _jumping;
    private bool _falling;
    private bool _jumpStart;
    
    private static readonly int Running = Animator.StringToHash("Running");
    private static readonly int OnGround = Animator.StringToHash("OnGround");
    private static readonly int JumpUp = Animator.StringToHash("JumpUp");
    private static readonly int Falling = Animator.StringToHash("Falling");
    private static readonly int JumpStart = Animator.StringToHash("JumpStart");

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        _onGround = true;
        _running = false;
        _jumping = false;
        _falling = false;
        _jumpStart = false;
    }

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float verticalVelocity = _rigidbody.velocity.y;

        switch (verticalVelocity)
        {
            case > 0.1f:
                _jumping = true;
                _falling = false;
                _onGround = false;
                break;
            case < -0.1f:
                _jumping = false;
                _falling = true;
                _onGround = false;
                break;
            default:
                _jumping = false;
                _falling = false;
                _onGround = true;
                break;
        }

        _jumpStart = Input.GetKeyDown(KeyCode.Space);

        if (horizontal != 0 && _onGround)
        {
            _running = true;
        }
        else
        {
            _running = false;
        }

        switch (horizontal)
        {
            case > 0:
            {
                Vector3 thisScale = transform.localScale;
                if (thisScale.x < 0)
                {
                    thisScale.x *= -1;
                }
                transform.localScale = thisScale;
                break;
            }
            case < 0:
            {
                Vector3 thisScale = transform.localScale;
                if (thisScale.x > 0)
                {
                    thisScale.x *= -1;
                }
                transform.localScale = thisScale;
                break;
            }
        }

        _animator.SetBool(Running, _running);
        _animator.SetBool(OnGround, _onGround);
        _animator.SetBool(JumpUp, _jumping);
        _animator.SetBool(Falling, _falling);
        _animator.SetBool(JumpStart, _jumpStart);
    }
}