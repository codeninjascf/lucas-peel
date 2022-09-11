using UnityEngine;
using UnityEngine.SceneManagement;

public class Reset : MonoBehaviour
{
    private float _playerWidth;
    private float _playerHeight;
    private Transform _camera;

    private void Start()
    {
        _playerWidth = 1;
        _playerHeight = 1.8f;
        _camera = Camera.main.transform;
    }

    private void Update()
    {
        if (transform.position.x - _playerWidth > _camera.position.x + Spawner.ScreenBounds.x ||
            transform.position.x + _playerWidth < _camera.position.x - Spawner.ScreenBounds.x ||
            transform.position.y + _playerHeight < _camera.position.y - Spawner.ScreenBounds.y)
        {
            SceneManager.LoadScene(0);
        }
    }
}
