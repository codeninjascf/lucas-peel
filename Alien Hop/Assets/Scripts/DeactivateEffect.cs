using UnityEngine;
using System.Collections;

/// <summary>
/// Script which deactivate the effect after some time
/// </summary>
public class DeactivateEffect : MonoBehaviour {

    private AudioSource audioS;

    [HideInInspector]
    public managerVars vars;

    void OnEnable()
    {
        vars = Resources.Load("managerVarsContainer") as managerVars;
    }

    // Use this for initialization
    void Start ()
    {
        audioS = GetComponent<AudioSource>();//get audiosource component attached to object
        audioS.clip = vars.deathSound;	//set its clip to death sound
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (gameObject.activeInHierarchy == true)//when the effect get active in scene
            StartCoroutine(Deactivate());	//coroutine is started
	}

    private IEnumerator Deactivate()
    {
        yield return new WaitForSeconds(1.5f);//after 1.5 sec
        gameObject.SetActive(false);//it gets deactivated
    }

}
