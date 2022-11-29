using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InGameGui : MonoBehaviour {

    private AudioSource sound;
    public GameObject gameOn , gameOver;
    public Text score, best;
    public TextMeshProUGUI inGameScore;
    public Color[] medalCols;
    public Image medal;
    public Button homeBtn, retryBtn;
    public string mainMenu;

    private void Start ()
    {
        sound = GetComponent<AudioSource>();
        GameManager.instance.currentScore = 0;
        inGameScore.text = GameManager.instance.currentScore.ToString();
        homeBtn.GetComponent<Button>().onClick.AddListener(HomeButton);
        retryBtn.GetComponent<Button>().onClick.AddListener(RetryButton); 
    }

    private void Update ()
    {
        inGameScore.text = GameManager.instance.currentScore.ToString();

        if (GameManager.instance.currentScore >= GameManager.instance.highScore)
        {
            GameManager.instance.highScore = GameManager.instance.currentScore;
            GameManager.instance.Save();
        }

        if (GameManager.instance.isGameOver)
        {
            score.text = "Score: " + GameManager.instance.currentScore;
            best.text = "Best: " + GameManager.instance.highScore;
            MedalColour();
            gameOn.SetActive(false);
            gameOver.SetActive(true);
        }

    }

    private void HomeButton()
    {
        sound.Play();
        GameManager.instance.isGameOver = false;
        SceneManager.LoadScene(mainMenu);
    }

    private void RetryButton()
    {
        sound.Play();
        GameManager.instance.isGameOver = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void MedalColour()
    {
        if (GameManager.instance.currentScore >= 10)
        {
            medal.color = medalCols[GameManager.instance.currentScore / 10 - 1];
        }
    }
}
