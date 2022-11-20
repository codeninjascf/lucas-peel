using UnityEngine;
using System.Collections;

namespace Avoidance
{
	/// <summary>
	/// <see cref="Avoidance.MovementDirection"/> factory.
	/// </summary>
	public class MovementDirectionFactory  
	{
		/// <summary>
		/// Returns a <see cref="Avoidance.MovementDirection"/> based on <see cref="Avoidance.Direction"/>.
		/// </summary>
		public MovementDirection Make(Direction direction, float speed)
		{
			MovementDirection moveDirection = null;
			
			switch(direction)
			{
			case Direction.Down:
				moveDirection = new MoveDirectionImpl (Vector3.down, speed);
				break;
			case Direction.Up:
				moveDirection = new MoveDirectionImpl (Vector3.up, speed);
				break;
			case Direction.Right:
				moveDirection = new MoveDirectionImpl (Vector3.right, speed);
				break;
			case Direction.Left:
				moveDirection = new MoveDirectionImpl (Vector3.left, speed);
				break;
			}

			return moveDirection;
		}
	}
}