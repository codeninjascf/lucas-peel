using System;
using System.Linq;
using UnityEngine;

public class MobileInput : MonoBehaviour
{
    public static bool Tap => Input.touches.Any(touch => touch.phase == TouchPhase.Began);

    public static float Tilt(string axis, float threshold = 0.05f)
    {
        return axis switch
        {
            "Horizontal" => Math.Abs(Input.acceleration.x) > threshold ? Input.acceleration.x : 0,
            "Vertical" => Math.Abs(Input.acceleration.y) > threshold ? Input.acceleration.y : 0,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}
