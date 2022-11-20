using UnityEngine;
using System.Collections;

namespace Avoidance
{
	/// <summary>
	/// Movement direction interface. Provides UpdatePosition contract.
	/// </summary>
	public interface MovementDirection  
	{
		void UpdatePosition (Transform t);
	}

	/// <summary>
	/// Moves in the specified direction.
	/// </summary>
	public class MoveDirectionImpl : MovementDirection
	{
		private float _speed;
		private Vector3 _direction;

		/// <summary>
		/// Initializes a new instance of the <see cref="Avoidance.MoveDirectionImpl"/> class.
		/// </summary>
		/// <param name="direction">Direction of movement.</param>
		/// <param name="speed">Units per second.</param>
		public MoveDirectionImpl(Vector3 direction, float speed)
		{
			_direction = direction;
			_speed = speed;
		}

		/// <summary>
		/// Updates the position of the transform.
		/// </summary>
		/// <param name="t">Transforms position to update.</param>
		public void UpdatePosition(Transform t)
		{
			t.localPosition += _direction * _speed * Time.deltaTime;
		}
	}
}
