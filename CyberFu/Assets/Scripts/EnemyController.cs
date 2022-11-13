using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float chasingDistance = 1.5f;
    public float attackingDistance = 0.72f;
    public float attackTime = 2f;

    private float _currentAttackTime;

    private Transform _player;
    private Animator _animator;
    private Rigidbody _rigidbody;
    // Start is called before the first frame update
    private enum EnemyState
    {
        Waiting,
        Following,
        Attacking
    }
    private EnemyState _state;

    void Start()
    {
        _player = GameManager.Player;
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();

        _state = EnemyState.Following;

        // Update is called once per frame
    }
    void Update()
    {
        if (GameManager.GameOver)
        {
            _state = EnemyState.Waiting;
        }
        Vector3 direction = _player.position - transform.position;
        transform.rotation = Quaternion.LookRotation(direction);

        _animator.SetBool("Walk", _state == EnemyState.Following);
        _rigidbody.isKinematic = _state != EnemyState.Following;

        float distance = direction.magnitude;
        switch (_state)
        {
            case EnemyState.Attacking when distance >= attackingDistance:
                _state = EnemyState.Waiting;
                break;
            case EnemyState.Waiting or EnemyState.Following when distance < attackingDistance:
                _currentAttackTime = attackTime;
                _state = EnemyState.Attacking;
                break;
            case EnemyState.Following:
                _rigidbody.velocity = transform.forward * moveSpeed;
                break;
            case EnemyState.Attacking:
                _currentAttackTime += Time.deltaTime;
                if (_currentAttackTime > attackTime)
                {
                    int attack = Random.Range(1, 4);
                    string attackString = "Attack" + attack;
                    _animator.SetTrigger(attackString);

                    _currentAttackTime = 0;
                }
                break;
        }
    }
}