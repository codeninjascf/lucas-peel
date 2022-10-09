using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI scoreText;
    public GameObject gameOverMenu;
    public TextMeshProUGUI finalScoreText;
    
    private bool _enabledGameOver;

    public static int Score { get; set; }
    public static bool GameOver { get; set; }

    private void Start()
    {
        Score = 0;
        GameOver = false;
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
            finalScoreText.text = $"Final Score: {Score}";
        }

        scoreText.text = $"SCORE: {Score}";
    }
}
