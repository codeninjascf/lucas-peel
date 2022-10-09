using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    private Vector3 _offset;
    
    private void Start()
    {
        _offset = transform.position;
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(_offset.x, _offset.y, target.position.z + _offset.z);
    }
}
