using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public float speed;
    public float moveLimit = 2.4f;  

    private Vector3 _direction;       
    private Rigidbody2D _rigidbody;

    private void Start ()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        
        int startDirection = Random.Range(0, 2); 

        _direction = startDirection == 0 ? Vector3.left : Vector3.right;

        transform.localScale = new Vector3(_direction.x, 1);
    }
	
    private void Update ()
    {
        if (GameManager.Instance.gameOver) return;

        if (transform.position.x >= moveLimit)
        {
            _direction = new Vector3(-1, 0);
            transform.localScale = new Vector3(_direction.x, 1);
        }
        else if (transform.position.x <= -moveLimit)
        {
            _direction = new Vector3(1, 0);
            transform.localScale = new Vector3(_direction.x, 1);
        }

        _rigidbody.velocity = new Vector2(speed * _direction.x, _rigidbody.velocity.y);
    }
}
