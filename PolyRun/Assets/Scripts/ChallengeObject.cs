using System.Linq;
using UnityEngine;

public class ChallengeObject : MonoBehaviour
{
    [Header("Object Settings")]
    [Tooltip("The amount of time the challenge should take to complete - The amount of time before the next challenge object spawns.")]
    public float challengeTime = 2f;
    
    public float Size => _rightMost.position.x + _rightMost.localScale.x / 2f;
    
    private Transform _rightMost;
    
    private void Start()
    {
        _rightMost = transform.Cast<Transform>().OrderBy(t => t.position.x + t.localScale.x / 2f).Last();
    }
}
