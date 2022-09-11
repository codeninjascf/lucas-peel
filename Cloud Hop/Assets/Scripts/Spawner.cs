using System;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class Spawner : MonoBehaviour
{
    public float spawnDistance = 6f;
    public float initialSpawnHeight = 5f;
    
    public GameObject[] platforms;
    
    private float _platformHeight;
    private Transform _camera;

    public static Vector2 ScreenBounds { get; private set; }

    private float _height;

    private void Start()
    {
        _height = initialSpawnHeight - spawnDistance;
        
        _platformHeight = platforms[0].GetComponent<SpriteRenderer>().bounds.size.y / 2;
        _camera = Camera.main.transform;
        Vector3 initialPos = _camera.position;
        _camera.position = Vector3.zero;
        ScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        _camera.position = initialPos;
    }

    private void Update()
    {
        if (_height + spawnDistance - _platformHeight < _camera.position.y + ScreenBounds.y)
        {
            int platform = Random.Range(0, platforms.Length);
            float xPos = Random.Range(-ScreenBounds.x / 2, ScreenBounds.x / 2);

            Vector3 position = new(xPos, _height + spawnDistance, platforms[platform].transform.position.z);
            _height += spawnDistance;

            Instantiate(platforms[platform], position, Quaternion.identity);
        } 
    }
}
