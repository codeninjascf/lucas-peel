using UnityEngine;

public class RocketAnimation : MonoBehaviour
{
    private Animator _animator;
    private static readonly int HAxis = Animator.StringToHash("HAxis");

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        _animator.SetFloat(HAxis, horizontal);
    }
}
