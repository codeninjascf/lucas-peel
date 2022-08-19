using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [Header("Scene Objects")]
    public GameObject player;
    public GameObject spawner;

    public GameObject[] challengeObjects;

    public static int Score
    {
        get => (int) _score;
        set => _score = value;
    }
    public static Vector2 ScreenBounds { get; private set; }
    public static bool GameOver { get; private set; }
    
    private static GameManager _instance;
    private static float _score;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
    }

    private void Start()
    {
        Score = 0;
        GameOver = false;
        ScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        player.SetActive(false);
        spawner.SetActive(false);
    }

    public static GameObject GetChallengeObject() =>
        _instance.challengeObjects[Random.Range(0, _instance.challengeObjects.Length)];

    public static void UpdateList(List<GameObject> activeObjects)
    {
        for (int i = activeObjects.Count - 1; i >= 0; i--)
        {
            GameObject activeObject = activeObjects[i];

            if (activeObject.GetComponent<ChallengeObject>().Size < -ScreenBounds.x)
            {
                activeObjects.Remove(activeObject);
                Destroy(activeObject);
            }
        }
    }

    public static void UpdateScore(Vector3 movement)
    {
        _score += Math.Abs(movement.x);
    }

    public static void StartGame()
    {
        _instance.player.SetActive(true);
        _instance.spawner.SetActive(true);
    }

    public static void EndGame()
    {
        GameOver = true;
        _instance.player.SetActive(false);
        _instance.spawner.SetActive(false);
    }
}
