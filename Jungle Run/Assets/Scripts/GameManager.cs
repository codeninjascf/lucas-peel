using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI scoreText;
    public GameObject gameOverMenu;
    public TextMeshProUGUI finalScoreText;
    
    [Header("Lives")]
    public Lives lives;
    public GameObject healthBar;
    public Sprite goldenHeartSprite;
    public GameObject deathEffect;
    public List<GameObject> hearts;

    private bool _enabledGameOver;
    private bool _golden;

    public static int Score { get; set; }
    public static int Lives { get; private set; }
    public static bool GameOver { get; set; }

    private Sprite _heartSprite;

    private void Start()
    {
        Score = 0;
        GameOver = false;
        Lives = 3;
        _heartSprite = hearts[0].GetComponent<Image>().sprite;
    }

    private void Update()
    {
        if (GameOver && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }

        if (GameOver && !_enabledGameOver)
        {
            _enabledGameOver = true;
            gameOverMenu.SetActive(true);
            scoreText.gameObject.SetActive(false);
            healthBar.SetActive(false);
            lives.GetComponent<PlayerMovement>().enabled = false;
            lives.GetComponent<Jump>().enabled = false;
            if (lives.gameObject.GetComponent<Rigidbody>().position.y > -0.5f)
            {
                Instantiate(deathEffect, lives.gameObject.transform.position, Quaternion.identity);
                lives.gameObject.SetActive(false);
            }
            finalScoreText.text = $"Final Score: {Score}";
        }

        if (lives.GetLives() < Lives && Lives > 0)
        {
            Lives = lives.GetLives();
            if (_golden)
            {
                for (int i = 0; i < 3; i++)
                {
                    hearts[i].GetComponent<Image>().sprite = _heartSprite;
                }
                _golden = false;
            }
            else
            {
                hearts[Lives].SetActive(false);
            }
        }

        if (lives.GetLives() > Lives && Lives <= 3)
        {
            _golden = true;
            for (int i = 0; i < 3; i++)
            {
                hearts[i].GetComponent<Image>().sprite = goldenHeartSprite;
            }
            Lives = lives.GetLives();
        }

        scoreText.text = $"SCORE: {Score}";
    }

    public void AddLife()
    {
        if (Lives < 3)
        {
            Lives++;
        }
    }
}
