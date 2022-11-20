using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    private GameData data;

    #region Variables not saved on device
    [HideInInspector]
    public bool gameOver = false, gameRestart = false;
    [HideInInspector]
    public int currentScore, currentPoints, gamesPlayed , lastScore;
    [HideInInspector]
    public float timeDiff;
    #endregion

    #region Variables saved on device
    //variables which are saved on the device
    public bool isGameStartedFirstTime;
    [HideInInspector]
    public bool canShowAds;
    [HideInInspector]
    public bool isMusicOn;
    [HideInInspector]
    public bool rateBtnClicked;
    public int bestScore;
    public bool[] skinUnlocked;
    public int selectedSkin;
    public int points; //to buy new skins
    [HideInInspector]
    public ulong giftTime; //to save the time at which the gift button was clicked
    #endregion
    public managerVars vars;

    void OnEnable()
    {
        vars = Resources.Load<managerVars>("managerVarsContainer");
    }

    void Awake()
    {
        MakeSingleton();
        InitializeGameVariables();
    }

    void MakeSingleton()
    {
        //this state that if the gameobject to which this script is attached , if it is present in scene then destroy the new one , and if its not present
        //then create new 
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void InitializeGameVariables()
    {
        Load();
        if (data != null)
        {
            isGameStartedFirstTime = data.getIsGameStartedFirstTime();
        }
        else
        {
            isGameStartedFirstTime = true;
        }

        if (isGameStartedFirstTime)
        {
            isGameStartedFirstTime = false;
            isMusicOn = true;
            canShowAds = true;
            bestScore = 0;
            points = 10;

            skinUnlocked = new bool[vars.characters.Count];
            skinUnlocked[0] = true;
            for (int i = 1; i < skinUnlocked.Length; i++)
            {
                skinUnlocked[i] = false;
            }
            selectedSkin = 0;

            rateBtnClicked = false;
                

            data = new GameData();

            data.setIsGameStartedFirstTime(isGameStartedFirstTime);
            data.setMusicOn(isMusicOn);
            data.setCanShowAds(canShowAds);
            data.setRateClick(rateBtnClicked);
            data.setBestScore(bestScore);
            data.setSkinUnlocked(skinUnlocked); 
            data.setPoints(points);
            data.setSelectedSkin(selectedSkin);   

            Save();

            Load();
        }
        else
        {
            isGameStartedFirstTime = data.getIsGameStartedFirstTime();
            isMusicOn = data.getMusicOn();
            canShowAds = data.getCanShowAds();
            giftTime = data.getGiftTime();
            rateBtnClicked = data.getRateClick();
            bestScore = data.getBestScore();
            points = data.getPoints();
            selectedSkin = data.getSelectedSkin();
            skinUnlocked = data.getSkinUnlocked();
        }
    }

    public static void GameOver()
    {
        Instance.gameOver = true;
        Instance.Save();
        GameUI.Instance.GameIsOver();
    }


    //                              .........this function take care of all saving data like score , current player , current weapon , etc
    public void Save()
    {
        FileStream file = null;
        //whicle working with input and output we use try and catch
        try
        {
            BinaryFormatter bf = new BinaryFormatter();

            file = File.Create(Application.persistentDataPath + "/GameData.dat");

            if (data != null)
            {
                data.setIsGameStartedFirstTime(isGameStartedFirstTime);
                data.setMusicOn(isMusicOn);
                data.setCanShowAds(canShowAds);
                data.setGiftTime(giftTime);
                data.setRateClick(rateBtnClicked);
                data.setBestScore(bestScore);
                data.setSkinUnlocked(skinUnlocked);
                data.setPoints(points);
                data.setSelectedSkin(selectedSkin);
                bf.Serialize(file, data);
            }
        }
        finally
        {
            if (file != null)
            {
                file.Close();
            }
        }


    }
    //                            .............here we get data from save
    public void Load()
    {
        FileStream file = null;
        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            file = File.Open(Application.persistentDataPath + "/GameData.dat", FileMode.Open);
            data = (GameData) bf.Deserialize(file);

        }
        catch
        {
            ResetGameManager();
        }
        finally
        {
            if (file != null)
            {
                file.Close();
            }
        }
    }

    //for resetting the gameManager

    public void ResetGameManager()
    {
        isGameStartedFirstTime = false;
        isMusicOn = true;
        canShowAds = true;

        bestScore  = 0;
        points = 10;

        skinUnlocked = new bool[vars.characters.Count];

        skinUnlocked[0] = true;

        for (int i = 1; i < skinUnlocked.Length; i++)
        {
            skinUnlocked[i] = false;
        }

        rateBtnClicked = false;


        data = new GameData();

        data.setIsGameStartedFirstTime(isGameStartedFirstTime);
        data.setMusicOn(isMusicOn);
        data.setCanShowAds(canShowAds);
        data.setRateClick(rateBtnClicked);
        data.setBestScore(bestScore);
        data.setSkinUnlocked(skinUnlocked);
        data.setPoints(points);
        data.setSelectedSkin(selectedSkin);
        Save();
        Load();

        Debug.Log("GameManager Reset");
    }


}

[Serializable]
class GameData
{
    private bool isGameStartedFirstTime;
    private bool isMusicOn;
    private bool canShowAds;
    private bool rateBtnClicked;
    private int bestScore;
    private bool[] skinUnlocked;
    private int selectedSkin;
    private int points; //to buy new skins
    private ulong giftTime; //to save the time at which the gift button was clicked

    public void setCanShowAds(bool canShowAds)
    {
        this.canShowAds = canShowAds;
    }

    public bool getCanShowAds()
    {
        return this.canShowAds;
    }

    public void setIsGameStartedFirstTime(bool isGameStartedFirstTime)
    {
        this.isGameStartedFirstTime = isGameStartedFirstTime;

    }

    public bool getIsGameStartedFirstTime()
    {
        return this.isGameStartedFirstTime;

    }
    //                                                                    ...............music
    public void setMusicOn(bool isMusicOn)
    {
        this.isMusicOn = isMusicOn;

    }

    public bool getMusicOn()
    {
        return this.isMusicOn;

    }
    //                                                                      .......music
        
    //....................................................for rate btn
    public void setRateClick(bool rateBtnClicked)
    {
        this.rateBtnClicked = rateBtnClicked;

    }

    public bool getRateClick()
    {
        return this.rateBtnClicked;

    }

    //best score
    public void setBestScore(int bestScore)
    {
        this.bestScore = bestScore;
    }

    public int getBestScore()
    {
        return this.bestScore;
    }

    //points
    public void setPoints(int points)
    {
        this.points = points;
    }

    public int getPoints()
    {
        return this.points;
    }

    //skin unlocked
    public void setSkinUnlocked(bool[] skinUnlocked)
    {
        this.skinUnlocked = skinUnlocked;
    }

    public bool[] getSkinUnlocked()
    {
        return this.skinUnlocked;
    }

    //selectedSkin
    public void setSelectedSkin(int selectedSkin)
    {
        this.selectedSkin = selectedSkin;
    }

    public int getSelectedSkin()
    {
        return this.selectedSkin;
    }

    //gift time
    public void setGiftTime(ulong giftTime)
    {
        this.giftTime = giftTime;
    }

    public ulong getGiftTime()
    {
        return this.giftTime;
    }

}
