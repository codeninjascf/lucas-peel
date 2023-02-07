using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    public UIManager uiManager;
    public Image starsImage;
    public string targetLevel;

    private int _stars;

    private void Update()
    {
        _stars = PlayerPrefs.GetInt(targetLevel + "_Stars", 0);
        starsImage.sprite = uiManager.starSprites[_stars];
    }

    public void OnClick()
    {
        SceneManager.LoadScene(targetLevel);
    }
}
