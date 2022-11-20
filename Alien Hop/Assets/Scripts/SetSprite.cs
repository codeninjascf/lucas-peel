using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSprite : MonoBehaviour {

    private SpriteRenderer mySprite;

    [HideInInspector]
    public managerVars vars;

    void OnEnable()
    {
        vars = Resources.Load("managerVarsContainer") as managerVars;
    }

    // Use this for initialization
    void Start ()
    {
        mySprite = GetComponent<SpriteRenderer>();
        mySprite.sprite = vars.starImg;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
