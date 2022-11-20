using UnityEngine;
using System.Collections;

namespace Avoidance
{
	/// <summary>
	/// Applies force with direction when scale completes.
	/// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public class ApplyForceOnScaleComplete : MonoBehaviour
    {
		/// <summary>
		/// The force to apply.
		/// </summary>
        public float force = 200;

		/// <summary>
		/// The scale object to wait on.
		/// </summary>
        public Scale scale;

		/// <summary>
		/// The direction of force to apply.
		/// </summary>
        public Vector2 direction;

		private Rigidbody2D _rigidBody2D;

        void Awake()
        {
            _rigidBody2D = GetComponent<Rigidbody2D>();
        }

        void OnEnable()
        {
            scale.onScaleComplete += ApplyForce;
        }

        void OnDisable()
        {
            scale.onScaleComplete -= ApplyForce;
        }

        private void ApplyForce()
        {
            _rigidBody2D.AddForce(direction * force);
        }

    }
}
