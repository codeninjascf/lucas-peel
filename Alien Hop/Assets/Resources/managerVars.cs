using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ShopItemData{
    public Sprite gameCharacterSprite1, gameCharacterSprite2;
}


public class managerVars : ScriptableObject {

    [SerializeField]
	public List<ShopItemData> characters = new List<ShopItemData>();

    [SerializeField]
    public Sprite soundOnButton, soundOffButton, leaderboardButton, playButton,
        homeButton, rateButton, backButton, starImg, titleImage, noAdsImage, shopCloseImage,
        shareImage, gameOverImg, retryBtnImg;

    [SerializeField]
    public Texture backgroundImg, cloudImg;

    [SerializeField]
    public Color32 gameOverScoreTextColor, gameOverBestScoreTextColor, inGameScoreTextColor,
        gameMenuStarTextColor, shopMenuStarTextColor, giftRewardTextColor;

    [SerializeField]
	public Font mainFont, secondFont;

    [SerializeField]
    public AudioClip buttonSound, starSound, jumpSound, backgroundMusic , deathSound;
}
