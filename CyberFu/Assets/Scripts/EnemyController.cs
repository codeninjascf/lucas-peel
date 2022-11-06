using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed=2f;
    public float chasingDistance=1.5f;
    public float attackingDistance=0.72f;
    public float attackTime=2f;

    private float _currentAttackTime;
    private Transform _player;
    private Animator _animator;
    private Rigidbody _rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
