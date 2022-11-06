using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Object = UnityEngine.Object;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

[System.Serializable]
public class Entity
{
    public GameObject gameObject;
    public GameObject healthBar;
    public int maxHealth;
    public Material damageFlash;
    public GameObject damageParticles;

    private int _health;
    private bool _invulnerable;
    private float _damageParticlesDuration;

    private SpriteRenderer _spriteRenderer;
    private Slider _healthSlider;
    private Material _defaultMaterial;
    
    public bool Dead { get; private set; }
    public int Template { get; set; }

    public Entity(Entity template, int templateNumber, Vector3 position, Transform enemiesList)
    {
        gameObject = Object.Instantiate(template.gameObject, position, Quaternion.identity);
        healthBar = Object.Instantiate(template.healthBar, enemiesList);
        maxHealth = template.maxHealth;
        damageFlash = template.damageFlash;
        damageParticles = template.damageParticles;
        Template = templateNumber;
        Initialise();
    }

    public void Initialise()
    {
        Dead = false;
        if (!gameObject.TryGetComponent(out _spriteRenderer))
            _spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();
        _defaultMaterial = _spriteRenderer.material;
        _damageParticlesDuration = damageParticles.GetComponent<ParticleSystem>().main.duration;
        _healthSlider = healthBar.GetComponentInChildren<Slider>();
        _healthSlider.maxValue = maxHealth;
        SetHealth(maxHealth);
    }

    public IEnumerator Damage(int amount = 1, float resetDelay = .05f)
    {
        if (_invulnerable || Dead) yield break;
        
        _invulnerable = true;
        
        _spriteRenderer.material = damageFlash;
        Object explosion = Object.Instantiate(damageParticles, gameObject.transform.position, Quaternion.identity);
        Object.Destroy(explosion, _damageParticlesDuration);
        
        SetHealth(_health - amount);

        yield return new WaitForSeconds(resetDelay);
        
        _spriteRenderer.material = _defaultMaterial;
        _invulnerable = false;
    }

    public void SetHealth(int health)
    {
        if (Dead) return;
        _health = health;
        if (_health < 0) Kill();
        else _healthSlider.value = _health;
    }

    public void Kill()
    {
        Object.Destroy(healthBar);
        gameObject.SetActive(false);
        Dead = true;
    }
}

public class GameManager : MonoBehaviour
{
    [Header("User Interface")] 
    public GameObject mainMenu;
    public GameObject gameOverlay;
    public GameObject gameOverMenu;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverScoreText;

    [Header("Game Setup")]
    public Entity player;
    public int spawnThreshold;
    public float difficultyIncreaseDelay = 20f;
    public Transform enemiesListPosition;
    public Entity[] crusherTemplates;

    private float _spawningBounds;  
    private int _spawnDirection;
    private int _score;

    private static bool _loaded;
    private static List<Entity> _crushers;
    
    public static bool GameOver { get; private set; }
    
    public static Entity GetEntity(GameObject obj) => _crushers.FirstOrDefault(e => e.gameObject == obj);
    public static Entity PlayerEntity { get; private set; }
    public static GameObject Player { get; private set; }
    public static Vector2 ScreenBounds { get; private set; }
    
    private void Start()
    {

        if (!_loaded)
        {
            mainMenu.SetActive(true);
            gameOverlay.SetActive(false);
            gameOverMenu.SetActive(false);
            
            player.gameObject.SetActive(false);
            
            _loaded = true;
            
            GameOver = true;
        }
        else
        {
            mainMenu.SetActive(false);
            gameOverlay.SetActive(true);
            gameOverMenu.SetActive(false);
            
            player.gameObject.SetActive(true);
            
            GameOver = false;
            
            StartCoroutine(IncreaseSpawnThreshold());
        }
        
        player.Initialise();
        PlayerEntity = player;
        Player = player.gameObject;

        _score = 0;

        _crushers = new List<Entity>();
        
        ScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        _spawningBounds = ScreenBounds.x + crusherTemplates
            .Select(crusher => crusher.gameObject.GetComponentInChildren<SpriteRenderer>().bounds.size.x / 2)
            .Prepend(0).Max();
        _spawnDirection = Random.Range(0, 2) == 0 ? -1 : 1;
    }

    private void Update()
    {
        switch (GameOver)
        {
            case false when !player.Dead:
            {
                foreach (Entity crusher in _crushers.Where(crusher => crusher.Dead).Reverse())
                {
                    _crushers.Remove(crusher);
                    
                    SpawnCrusher(crusher.Template + 1, crusher.gameObject.transform.position);

                    Destroy(crusher.gameObject, 1f);

                    _score++;
                    scoreText.text = $"Score: {_score}";
                }

                if (_crushers.Count <= spawnThreshold)
                {
                    SpawnCrusher();
                }

                break;
            }
            case false when player.Dead:
            {
                GameOver = true;
                
                Destroy(player.gameObject);
                
                gameOverlay.SetActive(false);
                gameOverMenu.SetActive(true);
                
                gameOverScoreText.text = $"Score: {_score}";

                break;
            }
        }
    }

    private void SpawnCrusher(int template = -1, Vector3 position = default)
    {
        if (template >= crusherTemplates.Length) return;
        
        if (template < 0)
        {
            _spawnDirection *= -1;

            template = Random.Range(0, 2);
            
            Vector3 spawnPosition = new(_spawningBounds * _spawnDirection, Random.Range(-2, 2f));

            _crushers.Add(new Entity(crusherTemplates[template], template, spawnPosition, enemiesListPosition));
        }
        else
        {
            _crushers.Add(new Entity(crusherTemplates[template], template, position, enemiesListPosition));
        }
    }

    private IEnumerator IncreaseSpawnThreshold()
    {
        yield return new WaitForSeconds(difficultyIncreaseDelay);
        spawnThreshold++;
        StartCoroutine(IncreaseSpawnThreshold());
    }

    public void StartGame()
    {
        mainMenu.SetActive(false);
        gameOverlay.SetActive(true);
        
        player.gameObject.SetActive(true);
        
        GameOver = false;
        
        StartCoroutine(IncreaseSpawnThreshold());
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(0);
    }
}
