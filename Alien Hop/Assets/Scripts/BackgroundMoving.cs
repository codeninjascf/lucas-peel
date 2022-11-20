using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMoving : MonoBehaviour {

    public float bgspeed; //ref to moving speed 
    Vector2 offset = Vector2.zero; //tile set offset

    [SerializeField]
    private Renderer bgPlaneMat; //ref to background material
    [SerializeField]
    private bool moveWithPlayer = false, mountains = true;

    [HideInInspector]
    public managerVars vars;

    void OnEnable()
    {
        vars = Resources.Load("managerVarsContainer") as managerVars;
    }

    void Start()
    {
        if (mountains)//if this script is on mountain bg
            bgPlaneMat.material.mainTexture = vars.backgroundImg;//we assign mountain texture
        else
            bgPlaneMat.material.mainTexture = vars.cloudImg;
    }

    // Update is called once per frame
    void Update ()
    {
        if (GameManager.Instance.gameOver)//when game is over we save the time
            GameManager.Instance.timeDiff = Time.time;
        //if game is over or player is not moving and move with player is true
        if (GameManager.Instance.gameOver || PlayerData.Instance.StartedMoving == false
            && moveWithPlayer == true)//we return
            return;
        //else we set the offset
        offset = new Vector2((Time.time - GameManager.Instance.timeDiff) * bgspeed, 0);
        bgPlaneMat.material.mainTextureOffset = offset;//and change texture offset

	}
}
