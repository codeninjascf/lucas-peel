using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    private int _score;
    private List<GameObject> _collidedPlatforms;

    private bool _started;
    
    private void Start()
    {
        scoreText.text = "Score: " + PlayerPrefs.GetInt("score", 0);
        _collidedPlatforms = new List<GameObject>();
    }

    private void Update()
    {
        if (!_started && Input.anyKeyDown)
        {
            _started = true;
            scoreText.text = "Score: " + _score;
            PlayerPrefs.SetInt("score", _score);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Platform") && !_collidedPlatforms.Contains(other.gameObject))
        {
            _collidedPlatforms.Add(other.gameObject);
            _score++;
            PlayerPrefs.SetInt("score", _score);
            scoreText.text = "Score: " + _score;
        }
    }
}
