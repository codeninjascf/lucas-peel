using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{

    public static ObjectPooling Instance;

    public GameObject scoreEffect;
    public GameObject platform;

    public int count = 5;

    private readonly List<GameObject> _scoreEffects = new ();
    private readonly List<GameObject> _spawnPlatforms = new ();

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < count; i++)
        {
            GameObject obj = Instantiate(scoreEffect, gameObject.transform, true);
            obj.SetActive(false);
            _scoreEffects.Add(obj);
        }
        for (int i = 0; i < count; i++)
        {
            GameObject obj = Instantiate(platform, gameObject.transform, true);
            obj.SetActive(false);
            _spawnPlatforms.Add(obj);
        }
    }
        
    public GameObject GetScoreEffect()
    {
        foreach (GameObject effect in _scoreEffects.Where(effect => !effect.activeInHierarchy))
        {
            return effect;
        }
            
        GameObject obj = Instantiate(scoreEffect, gameObject.transform, true);
        obj.SetActive(false);
        _scoreEffects.Add(obj);
        return obj;
    }

    public GameObject GetPlatform()
    {
        foreach (GameObject spawnPlatform in _spawnPlatforms.Where(spawnPlatform => !spawnPlatform.activeInHierarchy)) 
        {
            return spawnPlatform;
        }
            
        GameObject obj = Instantiate(platform, gameObject.transform, true);
        obj.SetActive(false);
        _spawnPlatforms.Add(obj);
        return obj;
    }
}