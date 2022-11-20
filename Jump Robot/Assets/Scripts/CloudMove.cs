using UnityEngine;

public class CloudMove : MonoBehaviour {

    public float speed = 0.25f;
    private Vector3 _direction;

    private void Start ()
    {
        _direction = Vector3.right;
	}

    private void Update ()
    {
        _direction = transform.position.x switch
        {
            >= 13f => Vector2.left,
            <= 0f => Vector2.right,
            _ => _direction
        };

        transform.Translate(Time.deltaTime * speed * _direction);
    }
}
