using UnityEngine;

public class BulletTimeout : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 2f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
           Destroy(gameObject);
        }
    }
}
