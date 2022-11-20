using UnityEngine;
using System.Collections;

namespace Avoidance
{
	/// <summary>
	/// Moves object to touch position.
	/// </summary>
	public class MoveToTouchPosition : Startable 
	{
		/// <summary>
		/// The move units per second.
		/// </summary>
		public float moveUnitsPerSecond = 5f;

		private bool _shouldMove = false;

		/// <summary>
		/// Enables movement.
		/// </summary>
		public override void OnStart ()
		{
			_shouldMove = true;
		}

		void Update () 
		{
			if (_shouldMove) 
			{
				Vector3 target = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				target.z = transform.position.z;

				transform.position = Vector3.MoveTowards (transform.position, target, moveUnitsPerSecond * Time.deltaTime);
			}
		}
	}
}
