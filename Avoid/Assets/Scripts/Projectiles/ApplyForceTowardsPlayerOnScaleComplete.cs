using UnityEngine;
using System.Collections;

namespace Avoidance
{
	/// <summary>
	/// Applies force towards player when scale is complete.
	/// </summary>
	[RequireComponent(typeof(Rigidbody2D))]
	public class ApplyForceTowardsPlayerOnScaleComplete : MonoBehaviour 
	{
		/// <summary>
		/// The force to apply.
		/// </summary>
		public float force = 200;

		/// <summary>
		/// The scale to wait on.
		/// </summary>
		public Scale scale;

		private Rigidbody2D _rigidBody2D;

		void Awake()
		{
			_rigidBody2D = GetComponent<Rigidbody2D> ();
		}

		void OnEnable()
		{
			scale.onScaleComplete += ApplyForceTowardsPlayer;
		}

		void OnDisable()
		{
			scale.onScaleComplete -= ApplyForceTowardsPlayer;
		}

		private void ApplyForceTowardsPlayer()
		{
			Vector2 playerPosition = GameObject.FindGameObjectWithTag ("Player").transform.position;

			var heading = (playerPosition - (Vector2)transform.position);
			var dist = heading.magnitude;
			var dir = heading / dist;

			_rigidBody2D.AddForce (dir * force);
		}
	}
}
