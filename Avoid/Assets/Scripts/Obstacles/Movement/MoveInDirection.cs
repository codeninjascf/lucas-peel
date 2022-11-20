using UnityEngine;
using System.Collections;

namespace Avoidance
{
	/// <summary>
	/// Direction of movement.
	/// </summary>
	public enum Direction
	{
		Left = 1,
		Up = 100,
		Right = 200,
		Down = 300
	}

	/// <summary>
	/// Moves object in a specified direction.
	/// </summary>
	public class MoveInDirection : MonoBehaviour 
	{
		/// <summary>
		/// The direction to move object.
		/// </summary>
		public Direction direction = Direction.Left;

		/// <summary>
		/// Units of movement per second.
		/// </summary>
		public float distancePerSecond = 5f;

		private MovementDirection _movement;

		/// <summary>
		/// Setups the specified instance and begins movement next frame.
		/// </summary>
		/// <param name="moveDirection">Move direction.</param>
		/// <param name="moveDistancePerSecond">Move distance per second.</param>
		public void Setup(Direction moveDirection, float moveDistancePerSecond)
		{
			this.direction = moveDirection;
			this.distancePerSecond = moveDistancePerSecond;

			_movement = new MovementDirectionFactory ().Make (direction, distancePerSecond);
		}

		void OnDisable()
		{
			_movement = null;
		}

		void Update () 
		{
			if (_movement != null) 
			{
				_movement.UpdatePosition (transform);
			}
		}
	}
}
