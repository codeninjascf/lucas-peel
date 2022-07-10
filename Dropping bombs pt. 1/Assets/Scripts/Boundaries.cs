using UnityEngine;

public class Boundaries : MonoBehaviour
{
    
    private Camera _camera;
    
    private void Start()
    {
        _camera = Camera.main;
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 pos = _camera.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp01(pos.x);
        pos.y = Mathf.Clamp01(pos.y);
        transform.position = _camera.ViewportToWorldPoint(pos);
    }
    
}
