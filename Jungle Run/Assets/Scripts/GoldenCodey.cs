using UnityEngine;

public class GoldenCodey : MonoBehaviour
{
    public int goldenCodeyActivationThreshold = 10;
    public Material goldenCodeyMaterial;
    public GameObject goldenSparkles;
    
    private Material _defaultMaterial;
    private SkinnedMeshRenderer _meshRenderer;
    private Lives _lives;
    
    private int _currentThreshold;
    private int _currentLives;
    private bool _setLives;
    private bool _givenLives;
    
    private void Start()
    {
        _currentThreshold = goldenCodeyActivationThreshold;
        _meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        _lives = GetComponent<Lives>();
        _defaultMaterial = _meshRenderer.material;
        goldenSparkles.SetActive(false);
    }

    private void Update()
    {
        if (!_setLives)
        {
            _currentLives = GameManager.Lives;
            _setLives = true;
        }
        
        if (GameManager.Lives < _currentLives)
        {
            _meshRenderer.material = _defaultMaterial;
            goldenSparkles.SetActive(false);
            _currentThreshold = GameManager.Score + goldenCodeyActivationThreshold;
            _currentLives = GameManager.Lives;
            _givenLives = false;
        }
        
        if (GameManager.Score >= _currentThreshold)
        {   
            _meshRenderer.material = goldenCodeyMaterial;
            goldenSparkles.SetActive(true);
            if (!_givenLives && _lives.GetLives() <= 3)
            {
                _givenLives = true;
                _lives.SetLives(_lives.GetLives() + 1);
                _currentLives = _lives.GetLives();
            }
        }
    }
}
