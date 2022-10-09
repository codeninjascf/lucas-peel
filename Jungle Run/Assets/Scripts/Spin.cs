using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    private const float RotationSpeed = -5f;

    private void FixedUpdate()
    {
        transform.Rotate(0, 0, RotationSpeed);
    }
}
