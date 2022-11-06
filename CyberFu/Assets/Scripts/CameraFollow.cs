using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    
    [Range(0f, 1f)]
    public float smoothingRate = 0.08f;

    private Vector3 _offset;

    private void Start()
    {
        _offset = transform.position;
    }

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + _offset, smoothingRate * Time.deltaTime * 100);
    }
}