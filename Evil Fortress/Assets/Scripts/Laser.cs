using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public float range = 50f;
    
    private Vector3 _endPoint;
    private LineRenderer _lineRenderer;

    private bool _playerDead;
    private bool _audioEnabled;

    private AudioSource _audioSource;
    
    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();

        _audioEnabled = TryGetComponent(out _audioSource);
    }

    private void Update()
    {
        _endPoint = transform.forward * range;
        
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, range))
        {
            _endPoint = transform.InverseTransformPoint(hit.point);

            if (hit.collider.CompareTag("Player") && !_playerDead)
            {
                hit.collider.GetComponent<PlayerController>().Kill();

                _playerDead = true;

                if (_audioEnabled)
                {
                    _audioSource.Play();
                }
            }
        }

        _lineRenderer.SetPosition(1, _endPoint);
    }
}
