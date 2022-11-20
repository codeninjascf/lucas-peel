using UnityEngine;

public class EnemyLevelSpawner : MonoBehaviour {

    public static EnemyLevelSpawner instance;

    [SerializeField]
    private float gapBtwLevel = 10.15f;//game between 2 levels
    [SerializeField]
    private GameObject startLevel;//ref to start level

    private float nextSpawnPos;//ref to next x spawn position

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

	// Use this for initialization
	void Start ()
    {   //we set the next spawn position
        nextSpawnPos = startLevel.transform.position.x + gapBtwLevel;
        for (int i = 0; i < 2; i++)
        {   //at start we spawn 2 new levels
            SpawnLevel();
        }	
	}
	//method which select the level and spawn it
    public void SpawnLevel()
    {
        int r = Random.Range(0, 10);//we have 10 levels
        GameObject enemyLevel = null;
        if (r == 0)
            enemyLevel = ObjectPooling.instance.GetPlatfrom1();
        else if (r == 1)
            enemyLevel = ObjectPooling.instance.GetPlatfrom2();
        else if (r == 2)
            enemyLevel = ObjectPooling.instance.GetPlatfrom3();
        else if (r == 3)
            enemyLevel = ObjectPooling.instance.GetPlatfrom4();
        else if (r == 4)
            enemyLevel = ObjectPooling.instance.GetPlatfrom5();
        else if (r == 5)
            enemyLevel = ObjectPooling.instance.GetPlatfrom6();
        else if (r == 6)
            enemyLevel = ObjectPooling.instance.GetPlatfrom7();
        else if (r == 7)
            enemyLevel = ObjectPooling.instance.GetPlatfrom8();
        else if (r == 8)
            enemyLevel = ObjectPooling.instance.GetPlatfrom9();
        else if (r == 9)
            enemyLevel = ObjectPooling.instance.GetPlatfrom10();
        //we set its transform
        enemyLevel.transform.position = new Vector3(nextSpawnPos, transform.position.y, 0);
        //get the script attached to it
        EnemyLevelController script = enemyLevel.GetComponent<EnemyLevelController>();
        enemyLevel.SetActive(true);//activate it
        script.BasicSettings();//call its basic setting method
        //we set the next spawn position
        nextSpawnPos = gapBtwLevel + enemyLevel.transform.position.x;

    }
    

}
