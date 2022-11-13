using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePickup : MonoBehaviour
{
    // Start is called before the first frame update
    public int scoreAmount = 1;
    public float rotateSpeed = 50f;
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Score += scoreAmount;
            Destroy(gameObject);
        }
    }
}
