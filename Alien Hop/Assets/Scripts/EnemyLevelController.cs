using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLevelController : MonoBehaviour {

    private GameObject mainCameraObj , playerObj; //ref to main camera obj and player obj
    [SerializeField]
    private float distBtwCam; //to track distance between camera
    [SerializeField]
    private MovementController[] movingObj;//child object which moves
    [SerializeField]
    private GameObject left, right, down, up;//ref to child gameobject

    int i = 0;//to set the moving child object

	// Use this for initialization
	void Start ()
    {   //get ref to the main camera
        mainCameraObj = GameObject.FindGameObjectWithTag("MainCamera");	
        //ref to the player object
        playerObj = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update ()
    {   //if game over is true
        if (GameManager.Instance.gameOver)
        {   //we start a coroutine
            StartCoroutine(DeactivateLeftRight());
            return;//and return
        }
        //if the position of object from camera is less than distance limit
        if (transform.position.x - mainCameraObj.transform.position.x <= distBtwCam)
        {   //we spawn new level
            EnemyLevelSpawner.instance.SpawnLevel();
            if (movingObj != null)//if it thase any moving child object
            {   
                for (int i = 0; i < movingObj.Length; i++)
                {   //then we call the method from there script
                    movingObj[i].DeactiveTime();
                }
            }
            gameObject.SetActive(false);//then we deactivate the gameobject
        }
        //if player is null
        if (playerObj == null)
            return;//we return
        //when the distance between player and object is less than 10
        if (transform.position.x - playerObj.transform.position.x <= 10f)
        {   //we check if we have moving object and i == 0
            if (movingObj != null && i == 0)
            {
                i = 1; //change i = 1
                for (int i = 0; i < movingObj.Length; i++)
                {   //call basic setting method from the script of this moving object
                    movingObj[i].BasicSettings();
                }
            }
        }
    }
    //basic settings method called when the object is spawned by the spawner
    public void BasicSettings()
    {
        i = 0;//we reset the i
        int r = Random.Range(0, 5);//get random number

        if (r == 2)//if that random number is 2
        {
            int j = Random.Range(0, 2);//we then get another random number
            GameObject starObj = ObjectPooling.instance.GetStarPrefab();//get the star prefab
            //depending on random number
            if (j == 0) //we decide the position of the star prefab
                starObj.transform.position = down.transform.position;
            else
                starObj.transform.position = up.transform.position;

            starObj.SetActive(true);//and we activate it
        }

    }
    //when game is over we deactive the left and right child gameobject
    private IEnumerator DeactivateLeftRight()
    {
        yield return new WaitForSeconds(0.8f);//after 0.8 sec
        left.SetActive(false);
        right.SetActive(false);
    }

}
