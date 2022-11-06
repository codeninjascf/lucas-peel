using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 2f;
    public float runSpeed = 3f;
    public float turnSmoothing = 0.1f;
    public Vector2 xBoundaries;
    public Vector2 zBoundaries;

    [Header("Attacks")] 
    public float maxComboDelay = 0.5f;

    private float _speed;
    
    private int _combo;
    private float _lastButtonTime;
    
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (GameManager.GameOver) return;
        
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        if (input.magnitude > 0)
        {
            float targetRotation = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, targetRotation, 0), turnSmoothing * Time.deltaTime * 100);

            _speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;
            
            transform.position += _speed * Time.deltaTime * new Vector3(input.x, 0, input.y);
            
            transform.position = new Vector3(
                    Mathf.Clamp(transform.position.x, xBoundaries.x, xBoundaries.y),
                    transform.position.y,
                    Mathf.Clamp(transform.position.z, zBoundaries.x, zBoundaries.y)
                );
        }
        
        _animator.SetFloat("Speed", input.magnitude * _speed);

        if (Time.time - _lastButtonTime > maxComboDelay)
        {
            _combo = 0;
        }
        
        if (Input.GetButtonDown("Fire1"))
        {
            _lastButtonTime = Time.time;
            _combo++;

            if (_combo == 1)
            {
                _animator.SetBool("Attack1", true);

            }

            _combo = Math.Min(_combo, 3);
        }
    }

    public void AttackReturn1()
    {
        if (_combo >= 2)
        {
            _animator.SetBool("Attack2", true);
        }
        else
        {
            _animator.SetBool("Attack1", false);
        }
    }

    public void AttackReturn2()
    {
        if (_combo >= 3)
        {
            _animator.SetBool("Attack3", true);
        }
        else
        {
            _animator.SetBool("Attack1", false);
            _animator.SetBool("Attack2", false);
        }
    }

    public void AttackReturn3()
    {
        _animator.SetBool("Attack1", false);
        _animator.SetBool("Attack2", false);
        _animator.SetBool("Attack3", false);
        _combo = 0;
    }
}
