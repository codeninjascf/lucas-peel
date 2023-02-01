using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Level level;
    public GridManager gridManager;
    private bool _gameEnded;
    // Start is called before the first frame update
    void Start()
    {
        _gameEnded = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (level.GameOver && gridManager.MoveComplete && !_gameEnded)
        {
            _gameEnded = true;
        }
    }
    void LateUpdate()
    {
        if(gridManager.MadeMove)
        {
            level.MovesRemaining--;
        }
    }
}
