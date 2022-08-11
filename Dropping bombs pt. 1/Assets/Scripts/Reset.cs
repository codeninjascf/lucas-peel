using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Reset : MonoBehaviour
{
    public Animator clouds;
    public GameObject explosion;
    private GameManager _gameManager;
    private Transform _transform;
    private MeshRenderer _meshRenderer;
    private Collider _collider;

    private void Awake()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _collider = GetComponent<Collider>();
        _transform = transform;
    }

    // Called when the object collides with something
    private void OnCollisionEnter(Collision collision)
    {
        StartCoroutine(ResetScene());
    }
    
    private IEnumerator ResetScene()
    {
        _gameManager.GameOver = true;
        _meshRenderer.enabled = false;
        _collider.enabled = false;
        clouds.enabled = false;
        transform.GetChild(0).gameObject.SetActive(false);
        Instantiate(explosion, _transform.position, _transform.rotation);
        PlayerPrefs.SetInt("Score", _gameManager.Score);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(0);
    }
}
