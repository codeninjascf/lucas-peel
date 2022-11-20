using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow Instance;

    private Vector2 _velocity;
    private GameObject _player; 
    private float _distance;
    private bool _playerEnabled;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void InitialisePlayerSettings()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _distance = _player.transform.position.y - transform.position.y;
        _playerEnabled = true;
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.gameOver || !_playerEnabled) return;
        
        float posY = Mathf.SmoothDamp(transform.position.y, _player.transform.position.y - _distance, ref _velocity.y, 0.05f);
        transform.position = new Vector3(transform.position.x, posY, transform.position.z);
    }
}