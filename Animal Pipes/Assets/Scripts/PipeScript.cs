using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PipeScript : MonoBehaviour {

    [SerializeField]
    private string animalTag;      //ref to specific tag object to allow collide with specific pipe
    private bool moving = false;   //this for the lerp which happen when the pipe moves on tapping

    private AudioSource sound;    //audio source variable
    public AudioClip[] clips; //0 for points , 1 of end
    // Use this for initialization
    void Start ()
    {
        //audio source component assigned to the variable
        sound = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        //we detect tap when screen is touched and game over is false
        if (Input.GetMouseButtonDown(0) && !GameManager.instance.isGameOver)
        {
            DetectTap();
        }
        //when the pipe moves beyond limits its transfers
        //well it means when the pipe moves to extreme right its transformed to left
        if (transform.position.x > 5.25f)
        {
            transform.position = new Vector3(-5.25f, transform.position.y);
        }

        if (transform.position.x < -5.25f)
        {
            transform.position = new Vector3(5.25f, transform.position.y);
        }
    }

    //this detects the object colliding the pipe
    void OnTriggerEnter2D(Collider2D other)
    {
        //if the tag of colliding object is equal to allowed object
        if (other.CompareTag(animalTag))
        {
            //we increase score , play pipe animation
            sound.PlayOneShot(clips[0]);
            other.gameObject.SetActive(false);
            transform.GetChild(0).GetComponent<PipeAnim>().playAnim = true;
            GameManager.instance.currentScore++;
        }
        else
        {
            //gameover
            sound.PlayOneShot(clips[1]);
            GameManager.instance.isGameOver = true;
            //some camera shake
            CameraShake.instance.ShakeCamera(0.02f, 0.2f);
        }
    }

    //methode which detect tap
    void DetectTap()
    {
        //we get the position of tap
        Vector2 pos = Input.mousePosition;

        //left
        if (pos.x > 0 && pos.x < Screen.width / 2)
        {
            MoveLeft();
        }
        //right
        else if (pos.x > Screen.width / 2 && pos.x < Screen.width)
        {
            MoveRight();
        }
    }

    //here we lerp pipe from lastpos to new pos in right direction
    void MoveRight()
    {
        Vector3 lastPos = transform.position;
        Vector3 newPos = new Vector3(lastPos.x + 1.5f, lastPos.y);

        StartCoroutine(MoveFromTo(lastPos, newPos, 0.2f));
    }

    void MoveLeft()
    {
        Vector3 lastPos = transform.position;
        Vector3 newPos = new Vector3(lastPos.x - 1.5f, lastPos.y);

        StartCoroutine(MoveFromTo(lastPos, newPos, 0.2f));
    }

    //lerp methode
    IEnumerator MoveFromTo(Vector3 pointA, Vector3 pointB, float time)
    {
        if (!moving)
        {                     // Do nothing if already moving
            moving = true;                 // Set flag to true
            float t = 0f;
            while (t < 1.0f)
            {
                t += Time.deltaTime / time; // Sweeps from 0 to 1 in time seconds
                transform.position = Vector3.Lerp(pointA, pointB, t); // Set position proportional to t
                yield return 0;         // Leave the routine and return here in the next frame
            }
            moving = false;             // Finished moving
        }
    }
}
