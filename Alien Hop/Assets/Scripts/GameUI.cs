using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public static GameUI Instance;

    public GameObject mainMenu, gameOverMenu, instructionImg;
    public UIObjects UIO; //ref to the UIObject

#pragma warning disable 414
    private int _i; //this is to make game over coroutine run only once
#pragma warning restore 414

    public bool gameStarted { get; set; }

    public GameUI()
    {
        _i = 0;
    }

    void OnEnable()
    {
        Resources.Load<managerVars>("managerVarsContainer");
    }

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

	// Use this for initialization
	void Start ()
    {
        //basic setting when the game starts
        GameManager.Instance.gameOver = false;//game over is false
        GameManager.Instance.currentScore = 0;//current score is zero
        GameManager.Instance.currentPoints = 0;//current point is zero

        if (GameManager.Instance.gameRestart)
        {
            GameManager.Instance.gameRestart = false;
            PlayBtn();
        }
        //set the text values
        UIO.gameMenuUI.scoreText.text = "" + GameManager.Instance.currentScore;
        UIO.gameMenuUI.starText.text = "" + GameManager.Instance.points;
    } 

    void Update()
    {   //if game over is false and game started is true
        if (GameManager.Instance.gameOver == false && gameStarted == true)
        {   //set the text values
            UIO.gameMenuUI.scoreText.text = "" + GameManager.Instance.currentScore;
            UIO.gameMenuUI.starText.text = "" + GameManager.Instance.points;
        }
    }

    #region MainMenu
    public void PlayBtn()
    {
        UIObjects.instance.ButtonPress();
        UIO.gameMenuUI.scoreText.gameObject.SetActive(true);
        mainMenu.SetActive(false);
        gameStarted = true;
        instructionImg.SetActive(true);
    }

    #endregion

    #region GameOver Menu

    public void HomeBtn()
    {
        UIObjects.instance.ButtonPress();
        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }

    public void RestartBtn()
    {
        UIObjects.instance.ButtonPress();
        GameManager.Instance.gameRestart = true;
        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }
    
    #endregion

    public void GameIsOver()
    {
        StartCoroutine(GameOver());
    }

    private IEnumerator GameOver()
    {
        if (GameManager.Instance.currentScore > GameManager.Instance.bestScore)
        {
            GameManager.Instance.bestScore = GameManager.Instance.currentScore;
            GameManager.Instance.Save();
        }

        GameManager.Instance.lastScore = GameManager.Instance.currentScore;
        UIO.gameMenuUI.scoreText.gameObject.SetActive(false);
        UIO.gameOverMenuUI.gameOverScoreText.text = "" + GameManager.Instance.currentScore;
        UIO.gameOverMenuUI.gameOverBestScoreText.text = "BEST: " + GameManager.Instance.bestScore;

        yield return new WaitForSeconds(1f);
        gameOverMenu.SetActive(true);
    }
}
