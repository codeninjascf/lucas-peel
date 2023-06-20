using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public float respawnDelay = 1.5f;
    public PlayerController player;
    public CameraFollow cam;
    public Transform[] checkpoints;
    private int _currentCheckpoint;
    public Transform[] collectibles;
    private bool[] _collectiblesCollected;
    public GameObject deathParticles;
    void Start()
    {
        _currentCheckpoint = 0;
        _collectiblesCollected = new bool[3];
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void KillPlayer()
    {
        player.Disable();
        player.gameObject.SetActive(false);
        GameObject particles = Instantiate(deathParticles, new
            Vector3(player.transform.position.x, player.transform.position.y),
            Quaternion.identity);
        Destroy(particles, 1f);
        StartCoroutine(ResetPlayer());
    }
    IEnumerator ResetPlayer()
    {
        yield return new WaitForSeconds(respawnDelay);
        Vector3 spawnPosition = checkpoints[_currentCheckpoint].position;
        player.Enable();
        player.gameObject.SetActive(true);
        player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        player.transform.position = spawnPosition;
        cam.ResetView();
    }
    public void SetCheckpoint(Transform checkpoint)
    {
        int checkpointNumber = Array.IndexOf(checkpoints, checkpoint);
        if(checkpointNumber>_currentCheckpoint)
        {
            _currentCheckpoint = checkpointNumber;
        }
    }
    public void GotCollectible(Transform collectible)
    {
        int collectibleNumber = Array.IndexOf(collectibles, collectible);
        _collectiblesCollected[collectibleNumber] = true;
    }
}
