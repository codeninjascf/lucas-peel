using UnityEngine;
using System.Collections;

public class SpawnerController : MonoBehaviour {
    
    private static SpawnerController _instance;

    [SerializeField]
    private GameObject[] spawnPoints;  
    private AudioSource _sound;       
    public AudioClip[] swingClips;    
    public float timeReduce = 0.025f;            
    public float timeDecreaseMileStone = 5f;   
    private float _timeMileStoneCount;    

    public float minTime = 0.5f;              
    public float time = 1.5f;
    private int _lastI;                     

    private void Awake()
    {
        MakeInstance();
    }

    private void MakeInstance()
    {
        if (_instance == null)
            _instance = this;
    }

    private void Start ()
    {
        _sound = GetComponent<AudioSource>();
        _timeMileStoneCount = timeDecreaseMileStone;

        if (GameManager.instance.isGameOver == false)
        {
            SelectAnimal();
        }

        StartCoroutine(WaitForNextSpawn());
    }
	
    private void Update ()
    {
        IncreaseDiff();
    }

    private IEnumerator WaitForNextSpawn()
    {
        float timeVal;

        switch (GameManager.instance.currentScore)
        {
            case <= 10:
                timeVal = time;
                break;
            case > 10:
            {
                int i = Random.Range(0, 3);

                timeVal = i is >= 0 and < 2 ? time : 0.8f;

                break;
            }
        }

        yield return new WaitForSeconds(timeVal);
       
        if (GameManager.instance.isGameOver == false)
        {
            SelectAnimal();
        }

        StartCoroutine(WaitForNextSpawn());
    }

    private void SelectAnimal()
    {
        int i = Random.Range(0, spawnPoints.Length);
        while (i == _lastI)
        {
            i = Random.Range(0, spawnPoints.Length);
        }
        

        switch (i)
        {
            case 0:
            {
                GameObject newRed = ObjectPooling.instance.GetRed();
                newRed.transform.position = spawnPoints[i].transform.position;
                newRed.transform.rotation = transform.rotation;
                AnimalController code = newRed.GetComponent<AnimalController>();
                newRed.SetActive(true);
                code.ApplyForce = true;
                _sound.PlayOneShot(swingClips[0]);
                break;
            }
            case 1:
            {
                GameObject newBlue = ObjectPooling.instance.GetBlue();
                newBlue.transform.position = spawnPoints[i].transform.position;
                newBlue.transform.rotation = transform.rotation;
                AnimalController code = newBlue.GetComponent<AnimalController>();
                newBlue.SetActive(true);
                code.ApplyForce = true;
                _sound.PlayOneShot(swingClips[1]);
                break;
            }
            case 2:
            {
                GameObject newGreen = ObjectPooling.instance.GetGreen();
                newGreen.transform.position = spawnPoints[i].transform.position;
                newGreen.transform.rotation = transform.rotation;
                AnimalController code = newGreen.GetComponent<AnimalController>();
                newGreen.SetActive(true);
                code.ApplyForce = true;
                _sound.PlayOneShot(swingClips[2]);
                break;
            }
            case 3:
            {
                GameObject newYellow = ObjectPooling.instance.GetYellow();
                newYellow.transform.position = spawnPoints[i].transform.position;
                newYellow.transform.rotation = transform.rotation;
                AnimalController code = newYellow.GetComponent<AnimalController>();
                newYellow.SetActive(true);
                code.ApplyForce = true;
                _sound.PlayOneShot(swingClips[3]);
                break;
            }
        }

        _lastI = i;

    }

    private void IncreaseDiff()
    {
        if (!(GameManager.instance.currentScore > _timeMileStoneCount)) return;
        
        _timeMileStoneCount += timeDecreaseMileStone;
        timeDecreaseMileStone += 5f;
        time -= timeReduce;

        if (time < minTime)
        {
            time = minTime;
        }
    }
}
