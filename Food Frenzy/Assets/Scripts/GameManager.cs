using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Level level;
    public GridManager gridManager;
    public UIManager uiManager
    private bool _gameEnded;
    // Start is called before the first frame update
    void Start()
    {
        _gameEnded = false;
        if(level.type==LevelType.Moves)
        {
            uiManager.criteriaInfoText.text = "MovesRemaing";
        }
        else if(level.type==LevelType.Time)
        {
            uiManager.criteriaInfoText.text = "Time Remaining:";
        }
        uiManager.highScoreNumber.text = level.HighScore.ToString();
        uiManager.gameOverMenu.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if(level.type==LevelType.Moves)
        {
            uiManager.criteriaNumber.text = level.MovesRemaining.ToString();
        }
        else if(level.type==LevelType.Time)
        {
            double timeRemaining = Math.Max(level.TimeRemaining, 0);
            TimeSpan time = TimeSpan.FromSeconds(timeRemaining);
            uiManager.criteriaNumber.text = time.ToString(@"mm\:ss");
        }

        if (level.GameOver && gridManager.MoveComplete && !_gameEnded)
        {
            _gameEnded = true;
        }
        int starsAchieved = level.StarsAchieved(gridManager.Score);
        uiManager.UpdateScores(gridManager.Score);
        uiManager.UpdateStars(starsAchieved);
    }
    void LateUpdate()
    {
        if(gridManager.MadeMove)
        {
            level.MovesRemaining--;
        }
    }
}
