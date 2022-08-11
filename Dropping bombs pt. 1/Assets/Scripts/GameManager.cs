using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Bomb Spawning")]
    public GameObject bombPrefab;
    public Vector2 delayRange = new (0.2f, 1.0f);

    private float _delay;
    private bool _spawningActive;

    private Vector2 _screenBounds;
    private float _objectWidth;
    private float _objectHeight;
    
    [Header("UI")]
    public GameObject titleScreen;
    public GameObject scoreText;

    private TextMeshProUGUI _scoreTMP;
    private Text _scoreT;
    private bool _textMeshPro;
    
    public bool GameOver { get; set; }
    public int Score { get; set; }
    public bool Playing => _spawningActive && !GameOver;

    private void Start()
    {
        _spawningActive = false;
        GameOver = false;
        Score = PlayerPrefs.GetInt("Score", 0);
        
        if (titleScreen != null)
            titleScreen.SetActive(true);

        _scoreT = scoreText.GetComponent<Text>();
        _scoreTMP = scoreText.GetComponent<TextMeshProUGUI>();
        if (_scoreT != null)
            _textMeshPro = false;
        else if (_scoreTMP != null)
            _textMeshPro = true;
        
        ResetDelay();
        StartCoroutine(EnemyGenerator());

        _screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        _objectWidth = bombPrefab.GetComponent<MeshRenderer>().bounds.size.x / 2;
        _objectHeight = bombPrefab.GetComponent<MeshRenderer>().bounds.size.y / 2;
    }

    private IEnumerator EnemyGenerator()
    {
        yield return new WaitForSeconds(_delay);
        if (_spawningActive && !GameOver)
        {
            float randomX = Random.Range(_screenBounds.x - _objectWidth, _screenBounds.x * -1 + _objectWidth);
            float spawnY = _screenBounds.y + _objectHeight + 5;
            
            Instantiate(bombPrefab, new Vector3(randomX, spawnY, 0), bombPrefab.transform.rotation);
            ResetDelay();
        }

        StartCoroutine(EnemyGenerator());
    }

    private void ResetDelay()
    {
        _delay = Random.Range(delayRange.x, delayRange.y);
    }

    private void Update()
    {
        if (_textMeshPro)
            _scoreTMP.text = $"Score: {Score}";
        else
            _scoreT.text = $"Score: {Score}";
    }

    public void StartGame()
    {
        if (Playing) return;
        _spawningActive = true;
        Score = 0;
        if (titleScreen != null)    
            titleScreen.SetActive(false);
    }
}
