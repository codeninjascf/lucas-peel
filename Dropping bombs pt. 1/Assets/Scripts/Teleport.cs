using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    
    private Vector2 _screenBounds;
    private float _objectWidth;
    private float _objectHeight;
    private Rigidbody _rb;

    // OnEnable is called when the object becomes enabled and active
    private void OnEnable()
    {
        _screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        _objectWidth = transform.GetComponent<MeshRenderer>().bounds.size.x / 2;
        _objectHeight = transform.GetComponent<MeshRenderer>().bounds.size.y / 2;
        _rb = GetComponent<Rigidbody>();

        Respawn();
    }

    // Update is called once per frame
    private void Update()
    {
        if (transform.position.y < _screenBounds.y * -1 - _objectHeight-10)
        {
            Respawn();
        }
    }

    private void Respawn()
    {
        float randomX = Random.Range(_screenBounds.x + _objectWidth, _screenBounds.x * -1 - _objectWidth);
        float randomY = Random.Range(_screenBounds.y + _objectHeight + 10, _screenBounds.y + _objectHeight + 20);

        transform.position = new Vector3(randomX, randomY);
        _rb.velocity = Vector3.zero;
    }
    
}
