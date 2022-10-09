using System.Collections.Generic;
using UnityEngine;


public class PlatformSpawner : MonoBehaviour
{
    public float generationDistance = 25f;
    public float distanceBetween = 2.5f;
    public GameObject startingPlatform;
    public Transform player;
    public GameObject[] platforms;

    private Vector3 _currentPos;
    
    private void Start()
    {
        _currentPos = new Vector3(startingPlatform.transform.position.x, startingPlatform.transform.position.y,
            startingPlatform.transform.position.z + startingPlatform.transform.localScale.z + distanceBetween);

        while (_currentPos.z < player.position.z + generationDistance)
        {
            GameObject platform = GeneratePlatform();
            platform.GetComponent<Animator>().enabled = false;
        }
    }

    private void Update()
    {
        if (_currentPos.z < player.position.z + generationDistance)
        {
            GeneratePlatform();
        }
    }
    
    private GameObject GeneratePlatform()
    {
        int selector = Random.Range(0, platforms.Length);
        GameObject newPlatform = Instantiate(platforms[selector], _currentPos, Quaternion.identity);
        _currentPos = new Vector3(_currentPos.x, _currentPos.y,
            _currentPos.z + newPlatform.transform.localScale.z + distanceBetween);
        return newPlatform;
    }
}
