using UnityEngine;


public class ManagerVariables : ScriptableObject {

    [SerializeField]
    public Sprite soundOnIcon, soundOffIcon;
    [SerializeField]
    public Sprite bronzeMedal, silverMedal, goldMedal;
    [SerializeField]
    public AudioClip buttonSound, jumpSound, gameOverSound, highScore;
}
