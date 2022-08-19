using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Start is called before the first frame update
    public float objectSpeed=8f;
    private List<GameObject> _activeObjects;
    void Start()
    {
        _activeObjects=new List<GameObject>();
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement=objectSpeed*Time.deltaTime*Vector3.left;

        foreach (GameObject activeObject in _activeObjects)
        {
            activeObject.transform.position+=movement;
        }
        GameManager.UpdateScore(movement);
    }

    IEnumerator Spawn()
    {
        GameManager.UpdateList(_activeObjects); 
        GameObject challengeObject=Instantiate(GameManager.GetChallengeObject());
        challengeObject.transform.position=new Vector3(GameManager.ScreenBounds.x,0);
        _activeObjects.Add(challengeObject);
        ChallengeObject script=challengeObject.GetComponent<ChallengeObject>();
        yield return new WaitForSeconds(script.challengeTime);
        StartCoroutine(Spawn());
    }
}