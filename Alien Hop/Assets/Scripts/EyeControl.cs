using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeControl : MonoBehaviour {

    private GameObject target; //ref to target, here its player

    [SerializeField]
    private GameObject pupil; //ref to pupil child gameobject
    [SerializeField]
    private float eyeRadius; //ref to the radius of eye

	// Use this for initialization
	void Start ()
    {   //get the player
        target = GameObject.FindGameObjectWithTag("Player");	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (target != null)//if player is not null
            LookAtPlayer();	//we call this method
	}

    //method which make the pupil look at player
    void LookAtPlayer()
    {   //we get the distance between player and this gameobject
        Vector3 distanceToTarget = target.transform.position - transform.position;
        //clamp that distance max to the eye radius
        distanceToTarget = Vector3.ClampMagnitude(distanceToTarget, eyeRadius);
        //define new vector
        Vector3 pupilPosition = transform.position + distanceToTarget;
        pupil.transform.position = pupilPosition;//set the pupil position
    }

}
