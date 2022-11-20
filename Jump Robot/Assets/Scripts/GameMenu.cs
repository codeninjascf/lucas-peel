using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour 
{
    public Button soundBtn, shareBtn, playBtn;
    public GameObject gamePanel;
    public Text bestScore, scoreText, bestText; 
    public UIObjects uio;
    private AudioSource _sfxSound; 

    private ManagerVariables _managerVariables;

    private void OnEnable()
    {
        _managerVariables = Resources.Load("ManagerVariablesContainer") as ManagerVariables;
    }

    private void Start ()
    {
        _sfxSound = GetComponent<AudioSource>();
        FindMedalColour();

        soundBtn.GetComponent<Button>().onClick.AddListener(SoundBtn);    
        shareBtn.GetComponent<Button>().onClick.AddListener(ShareBtn);   
        playBtn.GetComponent<Button>().onClick.AddListener(PlayBtn);

        AudioListener.volume = GameManager.Instance.isMusicOn ? 1 : 0;
        uio.soundIcon.sprite = GameManager.Instance.isMusicOn
            ? _managerVariables.soundOnIcon
            : _managerVariables.soundOffIcon;
    }
	
    private void Update ()
    {  
        if (GameManager.Instance.gameStarted)
        {   
            scoreText.text = "" + GameManager.Instance.currentScore;
            if (GameManager.Instance.currentScore > GameManager.Instance.bestScore)
            {  
                GameManager.Instance.bestScore = GameManager.Instance.currentScore;
                GameManager.Instance.Save();
            }
        }
        else
        {
            if (GameManager.Instance.gameRestarted)
            {  
                bestText.text = "Score";
                bestScore.text = "" + GameManager.Instance.currentScore;
                FindMedalColour();
            }
            else
            {
                bestText.text = "Best";
                bestScore.text = "" + GameManager.Instance.bestScore;
            }
        }

        if (GameManager.Instance.gameOver) StartCoroutine(GameOver());
    }

    private static IEnumerator GameOver()
    {   
        yield return new WaitForSeconds(0.5f);
            
        GameManager.Instance.gameRestarted = true; 
        GameManager.Instance.gameStarted = false;   
        GameManager.Instance.gameOver = false;   
        GameManager.Instance.scoreEffect = 0;     

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
        
    private void SoundBtn()
    {   
        _sfxSound.PlayOneShot(_managerVariables.buttonSound);
        GameManager.Instance.isMusicOn = !GameManager.Instance.isMusicOn;
        AudioListener.volume = GameManager.Instance.isMusicOn ? 1 : 0;
        uio.soundIcon.sprite = GameManager.Instance.isMusicOn
            ? _managerVariables.soundOnIcon
            : _managerVariables.soundOffIcon;
        GameManager.Instance.Save();
    }
        
    private void ShareBtn()
    {
        _sfxSound.PlayOneShot(_managerVariables.buttonSound);
        ShareScreenShot.Instance.ButtonShare();
    }

    private void PlayBtn()
    {
        _sfxSound.PlayOneShot(_managerVariables.buttonSound);
        GameManager.Instance.currentScore = 0;
        gamePanel.SetActive(false);
        scoreText.text = "" + GameManager.Instance.currentScore;
        scoreText.gameObject.SetActive(true);
        GameManager.Instance.gameStarted = true;
    }

    private void FindMedalColour()
    {
        switch (GameManager.Instance.currentScore)
        {
            case < 10:
                uio.medal.gameObject.SetActive(false);
                break;
            case >= 10 and < 25:
                uio.medal.gameObject.SetActive(true);
                uio.medal.sprite = _managerVariables.bronzeMedal;
                break;
            case >= 25 and < 40:
                uio.medal.gameObject.SetActive(true);
                uio.medal.sprite = _managerVariables.silverMedal;
                break;
            case >= 40:
                uio.medal.gameObject.SetActive(true);
                uio.medal.sprite = _managerVariables.goldMedal;
                break;
        }
    }

}