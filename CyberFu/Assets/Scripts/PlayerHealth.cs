using UnityEngine;

public class PlayerHealth : Health
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HitCollider"))
        {
            Damage();
            
            if (health == 0) gameObject.SetActive(false);
        }
    }
}