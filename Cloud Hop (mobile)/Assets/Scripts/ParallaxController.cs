using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    private Transform _camera;
    private float _currentHeight;
    
    private void Start()
    {
        _camera = Camera.main.transform;
        _currentHeight = _camera.position.y;
    }

    void Update()
    {
        if (_camera.position.y > _currentHeight)
        {
            float dy = _camera.position.y - _currentHeight;
            foreach (ParallaxLayer layer in GetComponentsInChildren<ParallaxLayer>())
            {
                layer.transform.position += layer.parallaxMultiplier * dy * Vector3.up;
            }
            _currentHeight = _camera.position.y;
        }
    }
}
