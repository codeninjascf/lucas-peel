using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    public int bulletDamage = 5;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            StartCoroutine(GameManager.GetEntity(gameObject).Damage(bulletDamage));
            Destroy(other.gameObject);
        }
    }
}
