using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanishingPlatform : MonoBehaviour
{
    private float vanishTime = 5f;

    private SpriteRenderer _spriteRenderer;
    private bool _vanishing;
    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    IEnumerator Vanish()
    {
        if (!_vanishing)
        {
            _vanishing = true;
            while (_spriteRenderer.color.a > 0)
            {
                _spriteRenderer.color = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g,
                    _spriteRenderer.color.b, _spriteRenderer.color.a - 1 / 255f);

                yield return new WaitForSeconds(vanishTime / 255f);
            }
            transform.DetachChildren();
            gameObject.SetActive(false);
        }
        void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                StartCoroutine(Vanish());
            }
        }
    }
}
// Update is called once per frame
