using UnityEngine;

public class PlatformSettings : MonoBehaviour
{
    public Color green;
    public Color red;
    public SpriteRenderer[] sprites;

    private AudioSource _sound;
    private ManagerVariables _managerVariables;
    private void OnEnable()
    {
        _managerVariables = Resources.Load("ManagerVariablesContainer") as ManagerVariables;
    }

    private void Start()
    {
        _sound = GetComponent<AudioSource>();
    }

    public void SetGreen()
    {
        foreach (SpriteRenderer sprite in sprites)
        {
            sprite.color = green;
        }
    }
    
    public void SetRed()
    {
        foreach (SpriteRenderer sprite in sprites)
        {
            sprite.color = red;
        }
    }
    
    public void SetHighScore()
    {
        if (GameManager.Instance.currentScore <= GameManager.Instance.bestScore ||
            GameManager.Instance.scoreEffect != 0) return;
        
        GameManager.Instance.scoreEffect = 1;
            
        GameObject scoreEffect = ObjectPooling.Instance.GetScoreEffect();
        scoreEffect.transform.position = transform.position;
        scoreEffect.SetActive(true);
            
        _sound.PlayOneShot(_managerVariables.highScore);
            
        scoreEffect.GetComponent<DeactivateObject>().Deactivate();
    }
}
