using UnityEngine;

public class EnemyHealth : Health
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerHitCollider"))
        {
            Damage();
            
            if (health == 0)
            {
                GameManager.Score++;
                Destroy(gameObject);
            }
        }
    }
}