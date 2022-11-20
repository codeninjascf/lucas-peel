using UnityEngine;
using System.Collections;

public class DeactivateObject : MonoBehaviour {
    private IEnumerator DeactivateEffect()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }
    
    public void Deactivate()
    {
        StartCoroutine(DeactivateEffect());
    }
}
