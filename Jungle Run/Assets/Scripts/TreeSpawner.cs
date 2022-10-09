using UnityEngine;
using static UnityEngine.Quaternion;

public class TreeSpawner : MonoBehaviour
{

    public float generationDistance = 200f;
    public float distanceBetween = 1f;
    public float variance = 0.1f;
    public Vector2 xBoundary = new(6, 50);
    public GameObject ground;
    public Transform player;
    public GameObject[] trees;

    private float _currentZ;
    private float _currentGroundZ;
    private float _groundSize;

    private void Start()
    {
        _currentGroundZ = ground.transform.position.z;
        _groundSize = ground.transform.localScale.z * 10;
        while (_currentZ < player.position.z + generationDistance)
        {
            GenerateTrees();
        }
    }

    private void Update()
    {
        if (_currentZ < player.position.z + generationDistance)
        {
            GenerateTrees();
        }
    }

    private void GenerateTrees()
    {
        int selector = Random.Range(0, trees.Length);
        float randomX = Random.Range(xBoundary.x, xBoundary.y) * (Random.Range(0, 2) == 1 ? 1 : -1);
        Vector3 newPos = new(randomX, 0, _currentZ);
        GameObject newTree = Instantiate(trees[selector], newPos, identity);
        newTree.transform.rotation = new Quaternion(0, Random.Range(0, 360), 0, 0);
        float range = Random.Range(-variance, variance);
        newTree.transform.localScale = new Vector3(
            newTree.transform.localScale.x + variance,
            newTree.transform.localScale.y + variance,
            newTree.transform.localScale.z + variance
        );
        _currentZ += distanceBetween;
        if (_currentZ > _currentGroundZ + _groundSize / 2)
        {
            newPos = new Vector3(ground.transform.position.x, ground.transform.position.y, _currentGroundZ + _groundSize);
            Instantiate(ground, newPos, identity);
            _currentGroundZ += _groundSize;
        }
    }
}
