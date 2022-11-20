using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[System.Serializable]
public class MainMenuUI
{
    public Image titleImage, playImage, leaderboardBtnImage, noAdsImage, soundImage,
        settingBtnImg, rateBtnImg, restorePurchaseBtnImg, shopBtnImg, backBtnImg;
}

[System.Serializable]
public class ShopMenuUI
{
    public Image starImage, shopCloseImage;
    public Text starText;
}

[System.Serializable]
public class GameMenuUI
{
    public Image starImage;
    public Text starText, scoreText;
}

[System.Serializable]
public class GameOverMenuUI
{
    public Text gameOverScoreText, gameOverBestScoreText;
}

public class UIObjects : MonoBehaviour
{

    public static UIObjects instance;
    private AudioSource audioS;
    private AudioClip buttonClick;

    [HideInInspector]
    public managerVars vars;

    void OnEnable()
    {
        vars = Resources.Load<managerVars>("managerVarsContainer");
    }

    void Awake()
    {
        if (instance == null)
            instance = this;
        audioS = GetComponent<AudioSource>();       
    }

    void Start()
    {
        buttonClick = vars.buttonSound;
    }

    public MainMenuUI mainMenuUI;
    public ShopMenuUI shopMenuUI;
    public GameMenuUI gameMenuUI;
    public GameOverMenuUI gameOverMenuUI;
	public Text[] mainFont, secondFont;

    public void ButtonPress()
    {
        if (buttonClick == null) return;
        audioS.PlayOneShot(buttonClick);
    }

}
