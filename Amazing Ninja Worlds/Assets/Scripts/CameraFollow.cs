using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothingTime=.2f;
    public Vector3 cameraOffset;
    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    Vector3 targetPos = new Vector3(TargetJoint2D.position.x, target.position.x, target.position.y, transform.position.z) + cameraOffset;
    transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref _velocity, smoothingTime);
    void Update()
    {

    }
}
