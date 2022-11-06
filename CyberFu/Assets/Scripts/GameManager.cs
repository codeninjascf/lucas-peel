using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

[Serializable]
public class Wave
{
    public string name;
    public GameObject enemyPrefab;
    public int enemyCount;
    public float enemyDelay;
}

public class  GameManager : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject mainMenu;
    public GameObject gameOverlay;
    public GameObject gameOverMenu;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI healthText;
    public GameObject waveMessage;
    public TextMeshProUGUI waveNumber;
    public TextMeshProUGUI waveInfo;
    public TextMeshProUGUI finalScoreText;
    
    [Header("Game Settings")]
    public Transform player;
    public Transform spawnPoints;
    public float timeBetweenWaves;

    public Wave[] waves;

    private int _nextWave;
    private bool _waveActive;
    private int _waveCount;

    private Health _playerHealth;
    private List<GameObject> _enemies;
    
    private static bool _loaded;

    public static Transform Player { get; private set; }
    public static int Score { get; set; }
    public static bool GameOver { get; private set; }
    
    private void Start()
    {
        Player = player;
        Score = 0;
        
        _playerHealth = player.GetComponent<Health>();
        _enemies = new List<GameObject>();
        
        if (!_loaded)
        {
            mainMenu.SetActive(true);
            gameOverlay.SetActive(false);
            gameOverMenu.SetActive(false);
            
            _loaded = true;

            GameOver = true;
        }
        else
        {
            mainMenu.SetActive(false);
            gameOverlay.SetActive(true);
            gameOverMenu.SetActive(false);
            
            GameOver = false;
            
            StartCoroutine(SpawnWave());
        }
    }

    private void Update()
    {
        if (!_playerHealth) return;
        
        switch (GameOver)
        {
            case false when _playerHealth.CurrentHealth > 0:
                scoreText.text = $"Score: {Score}";
                healthText.text = $"Health: {_playerHealth.CurrentHealth}/{_playerHealth.maxHealth}";
                break;
            case false when _playerHealth.CurrentHealth <= 0:
                GameOver = true;
                
                gameOverlay.SetActive(false);
                gameOverMenu.SetActive(true);
                
                finalScoreText.text = $"Score: {Score}";
                break;
        }
    }

    private IEnumerator SpawnWave()
    {
        Wave wave = waves[_nextWave];
        _nextWave = Math.Min(_nextWave + 1, waves.Length - 1);
        _waveCount++;
        
        waveMessage.SetActive(true);
        waveNumber.text = $"Wave {_waveCount}";
        waveInfo.text = $"{wave.enemyCount} Enemies";
        
        Invoke("HideWaveMessage", 3f);

        for (int i = 0; i < wave.enemyCount; i++)
        {
            Transform spawnPoint = spawnPoints.GetChild(Random.Range(0, spawnPoints.childCount));
            _enemies.Add(Instantiate(wave.enemyPrefab, spawnPoint.position, spawnPoint.rotation));

            yield return new WaitForSeconds(wave.enemyDelay);
        }

        while (_enemies.Count > 0)
        {
            foreach (GameObject enemy in _enemies.ToArray().Reverse())
            {
                if (!enemy) _enemies.Remove(enemy);
            }
            yield return null;
        }

        yield return new WaitForSeconds(timeBetweenWaves);
        
        StartCoroutine(SpawnWave());
    }

    private void HideWaveMessage() => waveMessage.SetActive(false);

    public void StartGame()
    {
        mainMenu.SetActive(false);
        gameOverlay.SetActive(true);
            
        GameOver = false;
            
        StartCoroutine(SpawnWave());
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
