using UnityEngine;

public class PlatformHandler : MonoBehaviour
{
    public float minSpeed = 1f;
    public float maxSpeed = 3f;

    public float movementAmount = 1.5f;

    public enum Direction
    {
        Horizontal,
        Vertical,
        Diagonal
    }

    public Direction direction;

    private GameObject _player;
    
    private Vector3 _target;
    private Vector3 _point1;
    private Vector3 _point2;

    private float _speed;

    private float _platformHeight;
    private Transform _camera;

    private void Start()
    {
        _player = GameObject.FindWithTag("Player");
        
        switch (direction)
        {
            case Direction.Horizontal:
                _point1 = new Vector3(transform.position.x + movementAmount, transform.position.y, transform.position.z);
                _point2 = new Vector3(transform.position.x - movementAmount, transform.position.y, transform.position.z);
                break;
            case Direction.Vertical:
                _point1 = new Vector3(transform.position.x, transform.position.y + movementAmount, transform.position.z);
                _point2 = new Vector3(transform.position.x, transform.position.y - movementAmount, transform.position.z);
                break;
            case Direction.Diagonal:
                int diagonalDirection = Random.Range(1, 3) == 1 ? -1 : 1;
                _point1 = new Vector3(transform.position.x + movementAmount, transform.position.y + movementAmount * diagonalDirection, transform.position.z);
                _point2 = new Vector3(transform.position.x - movementAmount, transform.position.y - movementAmount * diagonalDirection, transform.position.z);
                break;
        }

        _target = Random.Range(1, 3) == 1 ? _point1 : _point2;
        _speed = Random.Range(minSpeed, maxSpeed);

        _platformHeight = GetComponent<SpriteRenderer>().bounds.size.y / 2;
        _camera = Camera.main.transform;
    }

    private void Update()
    {
        if (transform.position.y + _platformHeight <  _camera.position.y - Spawner.ScreenBounds.y)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        if (transform.position == _target)
        {
            _target = transform.position == _point1 ? _point2 : _point1;
        }

        transform.position = Vector3.MoveTowards(transform.position, _target, _speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject == _player)
        {
            _player.transform.parent = transform;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject == _player)
        {
            _player.transform.parent = null;
        }
    }
}
