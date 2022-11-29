using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.Serialization;

/// <summary>
/// This script helps in saving and loading data in device
/// </summary>
public class GameManager : MonoBehaviour {

    public static GameManager instance;

    private GameData data;

    //data which is not stored on device but referred while game is on
    public bool isGameOver;
    public int currentScore;

    //data to store on device
    public bool isGameStartedFirstTime;
    public bool isMusicOn;
    public int highScore;
    public int textureStyle;
    public bool showRate;
    public bool[] textureUnlocked;
    //ref to the background music
    //private AudioSource audio;

    void Awake()
    {
        MakeInstance();
        InitializeVariables();
    }


    void MakeInstance()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    //we initialize variables here
    void InitializeVariables()
    {
        Load();
        isGameStartedFirstTime = data == null || data.getIsGameStartedFirstTime();
        if (isGameStartedFirstTime)
        {
            //when game is started for 1st time on device we set the initial values
            isGameStartedFirstTime = false;
            highScore = 0;
            textureStyle = 0;
            textureUnlocked = new bool[4];
            textureUnlocked[0] = true;
            for (int i = 1; i < textureUnlocked.Length; i++)
            {
                textureUnlocked[i] = false;
            }
            isMusicOn = true;
            showRate = true;
            data = new GameData();

            //storing data
            data.setIsGameStartedFirstTime(isGameStartedFirstTime);
            data.setIsMusicOn(isMusicOn);
            data.setHiScore(highScore);
            data.setTexture(textureStyle);
            data.setTextureUnlocked(textureUnlocked);
            data.setShowRate(showRate);
            Save();
            Load();
        }
        else
        {
            //getting data
            isGameStartedFirstTime = data.getIsGameStartedFirstTime();
            isMusicOn = data.getIsMusicOn();
            highScore = data.getHiScore();
            textureStyle = data.getTexture();
            textureUnlocked = data.getTextureUnlocked();
            showRate = data.getShowRate();      
        }
    }

    void Update()
    {//here we control the background music
        //if (isGameOver == false && audio.isPlaying == false)
        //{
        //    audio.Play();
        //}
        //else if (isGameOver == true)
        //{
        //    audio.Stop();
        //}
    }

    //method to save data
    public void Save()
    {
        FileStream file = null;

        try
        {
            BinaryFormatter bf = new ();
            file = File.Create(Application.persistentDataPath + "/GameInfo.dat");
            if (data != null)
            {
                data.setIsGameStartedFirstTime(isGameStartedFirstTime);
                data.setHiScore(highScore);
                data.setTexture(textureStyle);
                data.setTextureUnlocked(textureUnlocked);
                data.setIsMusicOn(isMusicOn);
                data.setShowRate(showRate);
                bf.Serialize(file, data);
            }
        }
        catch
        {
            // ignored
        }
        finally
        {
            if (file != null)
            {
                file.Close();
            }
        }
    }

    //method to load data
    private void Load()
    {
        FileStream file = null;
        try
        {
            BinaryFormatter bf = new();
            file = File.Open(Application.persistentDataPath + "/GameInfo.dat", FileMode.Open); //here we get saved file
            data = (GameData)bf.Deserialize(file);
        }
        catch
        {
            // ignored
        }
        finally
        {
            if (file != null)
            {
                file.Close();
            }
        }
    }
}

[Serializable]
class GameData
{
    private bool isGameStartedFirstTime;
    private bool isMusicOn;
    private int hiScore, textureStyle;
    private bool[] textureUnlocked;
    private bool showRate;

    //is game started 1st time
    public void setIsGameStartedFirstTime(bool isGameStartedFirstTime)
    {
        this.isGameStartedFirstTime = isGameStartedFirstTime;
    }

    public bool getIsGameStartedFirstTime()
    {
        return isGameStartedFirstTime;
    }

    //rate
    public void setShowRate(bool showRate)
    {
        this.showRate = showRate;
    }

    public bool getShowRate()
    {
        return showRate;
    }

    //music
    public void setIsMusicOn(bool isMusicOn)
    {
        this.isMusicOn = isMusicOn;
    }

    public bool getIsMusicOn()
    {
        return isMusicOn;
    }

    //hi score 
    public void setHiScore(int hiScore)
    {
        this.hiScore = hiScore;
    }

    public int getHiScore()
    {
        return hiScore;
    }

    //textureStyle 
    public void setTexture(int textureStyle)
    {
        this.textureStyle = textureStyle;
    }

    public int getTexture()
    {
        return textureStyle;
    }

    //texture unlocked
    public void setTextureUnlocked(bool[] textureUnlocked)
    {
        this.textureUnlocked = textureUnlocked;
    }

    public bool[] getTextureUnlocked()
    {
        return textureUnlocked;
    }
}