using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameManager gameManager;
    public float deathHeight = -2;
    private bool _following;
    private float _cameraHeight;
    public Transform target;
    public float smoothingTime=.2f;
    public Vector3 cameraOffset;
    private Vector3 _velocity;
    public bool heightLimitActive;
    public float heightLimit = 16;
    // Start is called before the first frame update
    void Start()
    {
        _following = true;
        _cameraHeight = transform.position.y - Camera.main.ViewportToWorldPoint(Vector3.zero).y;
        ResetView();
    }
    // Update is called once per frame
    void Update()
    {
        _following = target.position.y > deathHeight && (!heightLimitActive || target.position.y < heightLimit);
        Vector3 targetPos = new Vector3(target.position.x, target.position.y, transform.position.z) + cameraOffset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref _velocity, smoothingTime);
        if (target.gameObject.activeSelf && target.position.y <= deathHeight - _cameraHeight)
        {
            gameManager.KillPlayer();
        }
    }
    void FixedUpdate()
    {
        if (!_following) return;
    }
    public void ResetView()
    {
        if(target.gameObject.activeSelf&target.position.y<=heightLimit+_cameraHeight&&heightLimitActive)
        {
            gameManager.KillPlayer();
        }
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z) + cameraOffset;
        _velocity = Vector3.zero;
    }
}
