using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
        public float jumpForce = 5f;
        public float moveSpeed = 2f;

        public GameObject bubbleEffect;
        public GameObject deathEffect;

        private Rigidbody2D _rigidbody;
        private AudioSource _audioSource;
        private PlayerData _playerData;
        // Start is called before the first frame update
        
    void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _audioSource = GetComponent<AudioSource>();
            _playerData = GetComponent<PlayerData>();

            _rigidbody.isKinematic = true;
        }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.gameOver || !GameUI.Instance.gameStarted)
        {
            return;
        }
            if (Input.GetMouseButtonDown(0))
            {
            if (!_playerData.StartedMoving)
            {
                _playerData.StartedMoving = true;
                _rigidbody.isKinematic = false;
                _rigidbody.velocity = new Vector2(moveSpeed, 0);
            }
                _audioSource.PlayOneShot(_playerData.JumpSound);
                    GameManager.Instance.currentScore++;

            _rigidbody.velocity = new Vector2(moveSpeed, jumpForce);
        }
            }
        
        void FlipGravity()
        {
            bubbleEffect.SetActive(!bubbleEffect.activeSelf);
            jumpForce *= -1;
            _rigidbody.gravityScale *= -1;
            _rigidbody.velocity = new Vector2(moveSpeed, jumpForce);
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Water")) FlipGravity();
            else if (other.CompareTag("Enemy"))
                {
                Instantiate(deathEffect, transform.position, Quaternion.identity);
                GameManager.GameOver();
                gameObject.SetActive(false);
                }
        }

    void OnTriggerExit2D(Collider2D other)
    {
    if (other.CompareTag("Water")) FlipGravity();
    }
}