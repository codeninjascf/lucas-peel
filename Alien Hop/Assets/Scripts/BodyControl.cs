using UnityEngine;

/// <summary>
/// This script is for the emey which inflates its body
/// </summary>
public class BodyControl : MonoBehaviour {

    [SerializeField]
    private float inflatingSpeed; //set the speed
    [SerializeField]
    private Vector2 maxSize; //set the max size

    private bool enlarging = true; //track when its enlarging
    private Vector2 minSize; //set its minimum size

	// Use this for initialization
	void Start ()
    {
        minSize = transform.localScale;	//store min size value as its default value
	}
	
	// Update is called once per frame
	void Update ()
    {
        Inflating();
    }

    //Method which cause inflating effect
    void Inflating()
    {   //if the x and y scale is more than max size
        if (transform.localScale.x >= maxSize.x && transform.localScale.y >= maxSize.y)
        {
            enlarging = false;//enlarging is false
        }//if the x and y scale is less than min size
        else if (transform.localScale.x <= minSize.x && transform.localScale.y <= minSize.y)
        {
            enlarging = true;//enlarging is true
        }
        //if enlarging is true
        if (enlarging)
        {   //we increase the scale with time
            transform.localScale += new Vector3(Time.deltaTime * 1 * inflatingSpeed,
                Time.deltaTime * 1 * inflatingSpeed, 0);
        }
        else if (!enlarging)
        {   //else we decrease the scale with time
            transform.localScale += new Vector3(Time.deltaTime * -1 * inflatingSpeed,
                Time.deltaTime * -1 * inflatingSpeed, 0);
        }
    }

}
