using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalController : MonoBehaviour
{
    public float minForce = 12;
    public float maxForce = 14;
    private Rigidbody2D _rigidbody;
    private Collider2D _collider;
    public bool ApplyForce { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if(GameManager.instance.isGameOver)
        {
            _rigidbody.isKinematic = true;
            return;
        }
        if(ApplyForce)
        {
            float force = Random.Range(minForce, maxForce);
            _rigidbody.AddForce(Vector2.up * force, ForceMode2D.Impulse);
            ApplyForce = false;
        }
        _collider.enabled = _rigidbody.velocity.y < 0;
        if(transform.position.y<-9f)
        {
            gameObject.SetActive(false);
        }
    }
}
