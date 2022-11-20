using UnityEngine;

public class MovementController : MonoBehaviour {

    [SerializeField]
    [Header("Combination of Rotate - MoveUpDown , Rotate - MoveLeft , MoveUpDown - Move Left are allowed")]
    private bool rotate;//enemy rotate movement
    [SerializeField]
    private bool moveUpDown, moveLeft, swing;//enemy axial movement
    [SerializeField]
    [Header("Speed of rotation")]
    private float rotSpeed;             //speed whith which the object must rotate
    [SerializeField]
    [Header("Speed of moving left")]
    private float speed;
    [SerializeField]
    [Header("Max movement from its y position")]
    private float yLimit;
    [SerializeField]
    [Header("Max angle can be moved")]
    private int angLimit;

    private float angle;               //keep track of angle
    private Vector3 defaultPos; //the position at the start
    private Quaternion defaultRot; //the rotation at the start
    private bool swingRight = false; //track swing direction
    private int direction = 1;//to give the direction
    private float currentAngle = 0;//track the current angle

    private bool startMoving = false;//decide when to start moving

    // Use this for initialization
    void Start ()
    {
        defaultPos = transform.localPosition;//save the deafult position
        defaultRot = transform.localRotation;//save the deafult rotation
    }
	
	// Update is called once per frame
	void Update ()
    {   //if start moving is false
        if (!startMoving)
            return;
        //when only rotation is true
        if (rotate && !moveUpDown && !moveLeft && !swing)
        {
            Rotate();
        }
        //when  rotation && moveUpDown is true
        else if (rotate && moveUpDown && !moveLeft && !swing)
        {
            Rotate();
            MoveUpDown();
        }
        //when  rotation && moveLeft is true
        else if (rotate && !moveUpDown && moveLeft && !swing)
        {
            Rotate();
            MoveOnlyLeft();
        }
        //when  moveUpDown is true
        else if (!rotate && moveUpDown && !moveLeft && !swing)
        {
            MoveUpDown();
        } //when only moveLeft is true
        else if (!rotate && !moveUpDown && moveLeft && !swing)
        {
            MoveOnlyLeft();
        }
        //when  moveUpDown && moveLeft is true
        else if (!rotate && moveUpDown && moveLeft && !swing)
        {
            MoveUpDown();
            MoveOnlyLeft();
        }
        //when only swing is true
        else if (!rotate && !moveUpDown && !moveLeft && swing)
        {
            Swing();
        }

    }
    //basci settings called when its spawned
    public void BasicSettings()
    {
        direction = 1; //reset the direction
        transform.localPosition = defaultPos;
        transform.localRotation = defaultRot;
        startMoving = true; //moving is started
    }

    public void DeactiveTime()
    {
        startMoving = false;
    }

    void Rotate()
    {
        // we get the z angle as we are going to rotate object on z axis
        angle = transform.rotation.eulerAngles.z;
        //then keep adding the speed with time to the angle
        angle += rotSpeed * Time.deltaTime;
        //then we change the z angle on object with new angle 
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
    //method which make object to move left with time and speed
    void MoveOnlyLeft()
    {
        transform.position += new Vector3(Time.deltaTime * -1 * speed, 0, 0);
    }
    //method which make object to move up down
    void MoveUpDown()
    {   //when y value is more than default y + limit y
        if (transform.position.y >= yLimit + defaultPos.y)
        {
            direction = -1;//we change the direction
        }
        else if (transform.position.y <= -yLimit + defaultPos.y)
        {
            direction = 1;
        }

        transform.position += new Vector3(0, Time.deltaTime * direction * speed, 0);
    }

    void Swing()
    {
        // we get the z angle as we are going to rotate object on z axis
        angle = transform.rotation.eulerAngles.z;
        //then keep adding the speed with time to the angle
        if (currentAngle >= angLimit)
            swingRight = true;
        else if (currentAngle <= -angLimit)
            swingRight = false;

        if (swingRight)
        {
            angle -= rotSpeed * Time.deltaTime;
            currentAngle -= rotSpeed * Time.deltaTime;
        }
        if (!swingRight)
        {
            angle += rotSpeed * Time.deltaTime;
            currentAngle += rotSpeed * Time.deltaTime;
        }

        //then we change the z angle on object with new angle 
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

}
