using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothingTime=.2f;
    public Vector3 cameraOffset;
    private Vector3 _velocity;

    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        Vector3 targetPos = new Vector3(target.position.x, target.position.y, transform.position.z) + cameraOffset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref _velocity, smoothingTime);

    }

}
