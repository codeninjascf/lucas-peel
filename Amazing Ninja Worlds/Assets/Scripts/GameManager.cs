using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public float respawnDelay = 1.5f;
    public PlayerController player;
    public Vector3 spawnPosition;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void KillPlayer()
    {
        player.Disable();
    }
}
