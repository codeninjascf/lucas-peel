using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public float spawnDelay = 12f;
    public Vector2 xBounds;
    public Vector2 zBounds;
    public GameObject[] pickups;
    private bool _startedSpawning;
        IEnumerator SpawnPickup()
        {
            while (!GameManager.GameOver)
        {
            yield return new WaitForSeconds(spawnDelay);
            int pickupIndex = Random.Range(0, pickups.Length);
            float x = Random.Range(xBounds.x, xBounds.y);
            float z = Random.Range(zBounds.x, zBounds.y);
            Vector3 spawnPosition = new(x, 0, z);
            Instantiate(pickups[pickupIndex], spawnPosition, Quaternion.identity);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!_startedSpawning&&!GameManager.GameOver)
        {
            _startedSpawning = true;
            StartCoroutine(SpawnPickup());
        }
    }
}
