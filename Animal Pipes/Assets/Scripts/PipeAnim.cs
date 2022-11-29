using UnityEngine;
using System.Collections;

public class PipeAnim : MonoBehaviour {

    [HideInInspector]
    public bool playAnim;     //ref to when to play animation

    private Animator anim;    //ref to animator component on the object

    // Use this for initialization
    void Start ()
    {
        //get the component and store it in anim variable
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        //if playanim is true we play the animation
        if (playAnim)
        {
            StartCoroutine(PlayAnim());
        }
            
    }

    IEnumerator PlayAnim()
    {
        anim.Play("PipeAnim");
        yield return new WaitForSeconds(0.33f);
        anim.Play("PipeIdle");
        playAnim = false;
    }


}
