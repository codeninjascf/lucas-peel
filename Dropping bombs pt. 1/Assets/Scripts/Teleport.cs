using UnityEngine;

public class Teleport : MonoBehaviour
{
    
    private Vector2 _screenBounds;
    private float _objectHeight;
    private static GameManager _gameManager;

    private void Awake()
    {
        if (_gameManager == null)
            _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    
    // OnEnable is called when the object becomes enabled and active
    private void OnEnable()
    {
        _screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        _objectHeight = transform.GetComponent<MeshRenderer>().bounds.size.y / 2;
    }

    // Update is called once per frame
    private void Update()
    {
        if (transform.position.y < _screenBounds.y * -1 - _objectHeight || _gameManager.GameOver)
        {
            _gameManager.Score += _gameManager.GameOver ? 0 : 1;
            Destroy(gameObject);
        }
    }
}
