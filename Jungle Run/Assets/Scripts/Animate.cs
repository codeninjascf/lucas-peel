using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animate : MonoBehaviour
{
    private Animator _animator;
    private Jump _jump;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _jump = GetComponent<Jump>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
