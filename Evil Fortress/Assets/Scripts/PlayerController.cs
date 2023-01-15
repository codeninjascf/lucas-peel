using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 6f;
    public float deathDelay = 4f;

    private bool _canMove;
    private bool _audioEnabled;
    
    private Vector3 _drag;
    
    private Animator _animator;
    private CharacterController _characterController;
    private AudioSource _audioSource;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();

        _audioEnabled = TryGetComponent(out _audioSource);

        _canMove = true;
    }

    private void Update()
    {
        if (!_canMove) return;

        Vector3 movement = new (Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (movement.magnitude > 1) movement = movement.normalized;

        _characterController.Move(moveSpeed * Time.deltaTime * movement);

        if (movement != Vector3.zero)
        {
            transform.forward = movement;

            if (_audioEnabled && !_audioSource.isPlaying)
            {
                _audioSource.Play();
            }
        }
        else if (_audioEnabled)
        {
            _audioSource.Stop();
        }
        
        _animator.SetBool("isRunning", movement != Vector3.zero);
    }

    public void Disable()
    {
        _canMove = false;
        _animator.SetBool("isRunning", false);
        
        if (_audioEnabled)
        {
            _audioSource.Stop();
        }
    }

    public void Kill()
    {
        Disable();
        _animator.SetTrigger("PlayerHit");
        Invoke(nameof(Restart), deathDelay);
    }

    private void Restart() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
}
