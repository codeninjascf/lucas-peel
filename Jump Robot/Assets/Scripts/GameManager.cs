using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    private GameData _data;

    [HideInInspector]
    public bool gameOver, gameStarted, gameRestarted;
    [HideInInspector]
    public int currentScore, scoreEffect;

    [HideInInspector]
    public bool isMusicOn, isGameStartedFirstTime;
    [HideInInspector]
    public int bestScore, lastScore;
    [HideInInspector]
    public bool[] achievements;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        
        InitialiseGameVariables();
    }

    private void InitialiseGameVariables()
    {
        Load();
            
        isGameStartedFirstTime = _data == null || _data.GetIsGameStartedFirstTime();

        if (isGameStartedFirstTime)
        {
            isGameStartedFirstTime = false;
            isMusicOn = true;
            bestScore = lastScore = 0;

            achievements = new bool[5];
            for (int i = 0; i < achievements.Length; i++)
            {
                achievements[i] = false;
            }

            _data = new GameData();

            _data.SetIsGameStartedFirstTime(isGameStartedFirstTime);
            _data.SetMusicOn(isMusicOn);
            _data.SetBestScore(bestScore);
            _data.SetLastScore(lastScore);
            _data.SetAchievementsUnlocked(achievements);
                
            Save();
            Load();
        }
        else
        {
            if (_data == null) return;
                
            isGameStartedFirstTime = _data.GetIsGameStartedFirstTime();
            isMusicOn = _data.GetMusicOn();
            bestScore = _data.GetBestScore();
            lastScore = _data.GetLastScore();
            achievements = _data.GetAchievementsUnlocked();
        }
    }


    public void Save()
    {
        FileStream file = null;
        try
        {
            BinaryFormatter bf = new();

            file = File.Create(Application.persistentDataPath + "/GameData.dat");

            if (_data == null) return;

            _data.SetIsGameStartedFirstTime(isGameStartedFirstTime);
            _data.SetMusicOn(isMusicOn);
            _data.SetBestScore(bestScore);
            _data.SetLastScore(lastScore);
            _data.SetAchievementsUnlocked(achievements);
            bf.Serialize(file, _data);
            
            file.Close();
        }
        catch
        {
            file?.Close();
        }
    }
    public void Load()
    {
        FileStream file = null;
        try
        {
            BinaryFormatter bf = new ();
            file = File.Open(Application.persistentDataPath + "/GameData.dat", FileMode.Open);
            _data = (GameData)bf.Deserialize(file);
        }
        catch
        {
            ResetGameManager();
        }

        file?.Close();
    }
        
    public void ResetGameManager()
    {
        isGameStartedFirstTime = false;
        isMusicOn = true;

        bestScore = lastScore = 0;

        achievements = new bool[5];
        for (int i = 0; i < achievements.Length; i++)
        {
            achievements[i] = false;
        }

        _data = new GameData();

        _data.SetIsGameStartedFirstTime(isGameStartedFirstTime);
        _data.SetMusicOn(isMusicOn);
        _data.SetBestScore(bestScore);
        _data.SetLastScore(lastScore);
        _data.SetAchievementsUnlocked(achievements);
            
        Save();
        Load();

        Debug.Log("GameManager Reset");
    }


}

[Serializable]
internal class GameData
{
    private bool _isGameStartedFirstTime;
    private bool _isMusicOn;
    private int _bestScore, _lastScore;
    private bool[] _achievements;

    public void SetIsGameStartedFirstTime(bool isGameStartedFirstTime)
    {
        _isGameStartedFirstTime = isGameStartedFirstTime;
    }

    public bool GetIsGameStartedFirstTime()
    {
        return _isGameStartedFirstTime;
    }

    public void SetMusicOn(bool isMusicOn)
    {
        _isMusicOn = isMusicOn;
    }

    public bool GetMusicOn()
    {
        return _isMusicOn;
    }
        
    public void SetBestScore(int bestScore)
    {
        _bestScore = bestScore;
    }

    public int GetBestScore()
    {
        return _bestScore;
    }

    public void SetLastScore(int lastScore)
    {
        _lastScore = lastScore;
    }

    public int GetLastScore()
    {
        return _lastScore;
    }

    public void SetAchievementsUnlocked(bool[] achievements)
    {
        _achievements = achievements;
    }

    public bool[] GetAchievementsUnlocked()
    {
        return _achievements;
    }
}