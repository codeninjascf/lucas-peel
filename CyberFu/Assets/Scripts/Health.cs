using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 10;
    public GameObject deathParticles;
    
    public int CurrentHealth => health;

    protected int health;
    
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        health = maxHealth;
    }
    
    public void Add(int amount) => health = Math.Min(health + amount, maxHealth);

    protected void Damage()
    {
        health--;
        _animator.SetTrigger("Hit");

        if (health <= 0)
        {
            health = 0;
            Destroy(Instantiate(deathParticles, transform.position, Quaternion.identity), 5f);
        }
    }
}
