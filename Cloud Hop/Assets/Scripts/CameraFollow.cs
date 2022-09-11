using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target Object")]
    public Transform target;

    [Range(0f, 1f)]
    public float smoothingCoefficient = 0.1f;

    private Vector3 _targetPosition;
    private float _maxHeight;
    
    private void Start()
    {
        _maxHeight = transform.position.y;
        _targetPosition = target.position;
    }

    private void FixedUpdate()
    {
        if (target.position.y > _maxHeight)
        {
            _maxHeight = target.position.y;
        }

        _targetPosition = new Vector3(transform.position.x, _maxHeight, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, _targetPosition, smoothingCoefficient);
    }
}