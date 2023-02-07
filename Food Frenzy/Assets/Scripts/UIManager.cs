using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Sprite[] starSprites;
    
    [Header("Overlay")]
    public TextMeshProUGUI criteriaInfoText;
    public TextMeshProUGUI criteriaNumber;
    public TextMeshProUGUI highScoreNumber;
    public TextMeshProUGUI currentScoreNumber;
    public Image currentStarsImage;
    
    [Header("Game Over Menu")]
    public GameObject gameOverMenu;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI finalScoreText;
    public Image finalStarsImage;
    public GameObject newHighScoreText;

    [Header("Button Navigation")] 
    public string menuSceneName = "Menu";
    public string nextLevelName = "Level2";
    
    public void UpdateScores(int score)
    {
        currentScoreNumber.text = score.ToString();
        finalScoreText.text = "Final Score: " + score;
    }
    
    public void UpdateStars(int starsAchieved)
    {
        currentStarsImage.sprite = starSprites[starsAchieved];
        finalStarsImage.sprite = starSprites[starsAchieved];
    }

    public void MenuButton()
    {
        SceneManager.LoadScene(menuSceneName);
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void NextButton()
    {
        SceneManager.LoadScene(nextLevelName);
    }
}
