using UnityEngine;
using System.Collections;

namespace Avoidance
{
	/// <summary>
	/// <see cref="Avoidance.ObstacleDirectionSpawner"/> factory.
	/// </summary>
	public class ObstacleDirectionalSpawnerFactory 
	{
		/// <summary>
		/// Returns a <see cref="Avoidance.ObstacleDirectionSpawner"/> based on <see cref="Avoidance.ObstacleSpawnDirection"/>.
		/// </summary>
		public ObstacleDirectionSpawner Make(ObstacleSpawnDirection direction)
		{
			ObstacleDirectionSpawner directionalSpawner = null;
			
			switch(direction)
			{
				case ObstacleSpawnDirection.Horizontal:
					directionalSpawner = new ObstacleDirectionalSpawner (new ObstacleHorizontalDataImpl(Direction.Left, BoundsLocation.RightOffScreen),
						new ObstacleHorizontalDataImpl(Direction.Right, BoundsLocation.LeftOffScreen));
						break;
				case ObstacleSpawnDirection.Vertical:
					directionalSpawner = new ObstacleDirectionalSpawner (new ObstacleVerticalDataImpl(Direction.Up, BoundsLocation.BottomOffScreen),
						new ObstacleVerticalDataImpl(Direction.Down, BoundsLocation.TopOffScreen));
						break;
			}

			return directionalSpawner;
		}
	}
}