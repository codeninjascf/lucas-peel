using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    public float distanceFromCamera = 5f;
    
    private Transform _camera;

    private void Start()
    {
        _camera = Camera.main.transform;
    }

    private void Update()
    {
        if (_camera.position.z > transform.position.z + transform.localScale.z + distanceFromCamera)
        {
            Destroy(gameObject);
        }
    }
}
