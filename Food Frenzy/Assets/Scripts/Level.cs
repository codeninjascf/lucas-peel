using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
public enum LevelType
{
    Time,
    Moves
}
public class Level : MonoBehaviour
{
    public LevelType type;

    public int levelCondition;
    public int firstStar;
    public int secondStar;
    public int thirdStar;

    private string _levelName;

    public int MovesRemaining { get; set; }
    public double TimeRemaining { get; set; }
    public int HighScore { get; set; }

    public bool GameOver => type == LevelType.Moves && MovesRemaining <= 0 || type == LevelType.Time && TimeRemaining <= 0;
    // Start is called before the first frame update
    
    
     public int StarsAchieved(int score) => Convert.ToInt32(score >= firstStar) + 
        Convert.ToInt32(score >= secondStar) + Convert.ToInt32(score >= thirdStar);

     public void UpdateHighScore(int score) => PlayerPrefs.SetInt(_levelName + 
         "_HighScore", Math.Max(score, HighScore));

     public void UpdateStarsAchieved(int score) => PlayerPrefs.SetInt(_levelName + 
         "_Stars", StarsAchieved(score));


     
    void Awake()
    {
        _levelName = SceneManager.GetActiveScene().name;

        MovesRemaining = levelCondition;
        TimeRemaining = levelCondition;

        HighScore = PlayerPrefs.GetInt(_levelName + "_HighScore", 0);
    }

    void Update()
    {
        TimeRemaining -= Time.deltaTime;
    }

    
}