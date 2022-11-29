using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private AudioSource sound;

    public TextMeshProUGUI highScoreText;
    public Button playBtn;
    public string gameScene;

    private void Start()
    {
        highScoreText.text = "High Score: " + GameManager.instance.highScore;
        sound = GetComponent<AudioSource>();
        playBtn.GetComponent<Button>().onClick.AddListener(PlayButton);    //play
    }

    private void PlayButton()
    {
        sound.Play();
        SceneManager.LoadScene(gameScene);
    }
}