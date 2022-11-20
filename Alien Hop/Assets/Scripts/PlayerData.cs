using System;
using System.Collections;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class PlayerData : MonoBehaviour {

    public static PlayerData Instance;

    private AudioSource _audioSource;
    private SpriteRenderer _spriteRenderer;
    
    public bool StartedMoving { get; set; }
    public AudioClip JumpSound { get; private set; }

    private managerVars _managerVars;

    private Sprite _defaultSprite;
    private Sprite _blinkingSprite;

    private void OnEnable()
    {
        _managerVars = Resources.Load("managerVarsContainer") as managerVars;
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start ()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _audioSource = GetComponent<AudioSource>();

        JumpSound = _managerVars.jumpSound;
        
        _defaultSprite = _managerVars.characters[GameManager.Instance.selectedSkin].gameCharacterSprite1;
        _blinkingSprite = _managerVars.characters[GameManager.Instance.selectedSkin].gameCharacterSprite2;

        StartCoroutine(Blinking());
        
        CameraControl.instance.PlayerSettings();
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PickUp"))
        {
            _audioSource.PlayOneShot(_managerVars.starSound);
            GameManager.Instance.points++; 
            other.gameObject.SetActive(false);
        }
    }

    private IEnumerator Blinking()
    {   
        _spriteRenderer.sprite = _blinkingSprite;
        yield return new WaitForSeconds(0.25f);
        
        _spriteRenderer.sprite = _defaultSprite;
        yield return new WaitForSeconds(1f);
        
        StartCoroutine(Blinking());
    }
}
