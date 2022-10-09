using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 6f;
    public float changeLaneSpeed = 6f;

    private int _lane;

    private void Update()
    {
        transform.Translate(moveSpeed * Time.deltaTime * Vector3.forward);
        
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            _lane = Math.Min(_lane + 1, 1);
        }
        
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _lane = Math.Max(_lane - 1, -1);
        }

        transform.position = Vector3.MoveTowards(transform.position,
            new Vector3(_lane, transform.position.y, transform.position.z), changeLaneSpeed * Time.deltaTime);
    }
}
