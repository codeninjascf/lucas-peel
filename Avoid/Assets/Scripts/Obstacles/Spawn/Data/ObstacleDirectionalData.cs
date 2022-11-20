using UnityEngine;
using System.Collections;

namespace Avoidance
{
	/// <summary>
	/// Encapsulates ObstacleDataContainer creation.
	/// </summary>
	public interface ObstacleDirectionalData  
	{
		ObstacleDataContainer Build();
	}

	/// <summary>
	/// Encapsulates data to be used to create an obstacle.
	/// </summary>
	public struct ObstacleDataContainer
	{
		//Parent
		public Direction moveDirection; 
		public float moveDistancePerSecond;
		public BoundsLocation parentBoundsLocation; 
		public Axis parentLockedAxis;

		public ObstacleDataContainerSide leftSide;

		public ObstacleDataContainerSide rightSide;
	}

	/// <summary>
	/// Encapsulates data to be used to create an obstacle side.
	/// </summary>
	public struct ObstacleDataContainerSide
	{
		public Vector2 scale; //ScaleToScreenSize
		public Side lockedScaleSide;
		public BoundsLocation boundsLocation;
		public Axis lockedAxis;
		public bool basedOnHeightOnly;
	}

	/// <summary>
	/// Obstacle directional data implementation.
	/// </summary>
	public abstract class ObstacleDirectionalDataImpl : ObstacleDirectionalData
	{
		protected static readonly float OBSTACLE_HEIGHT = 0.02f;
		protected static readonly float MINIMUM_SCALE = 0.05f;

		protected Direction moveDirection;
		protected BoundsLocation boundsLocation;

		/// <summary>
		/// Initializes a new instance of the <see cref="Avoidance.ObstacleDirectionalDataImpl"/> class.
		/// </summary>
		/// <param name="moveDirection">Move direction.</param>
		/// <param name="boundsLocation">Bounds location.</param>
		public ObstacleDirectionalDataImpl(Direction moveDirection, BoundsLocation boundsLocation)
		{
			this.moveDirection = moveDirection;
			this.boundsLocation = boundsLocation;
		}

		public abstract ObstacleDataContainer Build();

		protected abstract void SetScale(ref ObstacleDataContainerSide leftSide, ref ObstacleDataContainerSide rightSide);
		protected abstract ObstacleDataContainer GetDefaultContainer();
	}

	/// <summary>
	/// Builds horizontal obstacle data.
	/// </summary>
	public class ObstacleHorizontalDataImpl : ObstacleDirectionalDataImpl
	{
		protected static readonly float MINIMUM_OBSTACLE_GAP = 0.35f;
		protected static readonly Vector2 MIN_MAX_MOVEMENT_SPEED = new Vector2(1f, 1.5f);

		/// <summary>
		/// Initializes a new instance of the <see cref="Avoidance.ObstacleHorizontalDataImpl"/> class.
		/// </summary>
		/// <param name="moveDirection">Move direction.</param>
		/// <param name="boundsLocation">Bounds location.</param>
		public ObstacleHorizontalDataImpl(Direction moveDirection, 
			BoundsLocation boundsLocation):base(moveDirection, boundsLocation)
		{
		}

		/// <summary>
		/// Build ObstacleDataContainer for a horizontal obstacle.
		/// </summary>
		public override ObstacleDataContainer Build()
		{
			ObstacleDataContainer container = GetDefaultContainer ();

			container.moveDistancePerSecond = Random.Range (MIN_MAX_MOVEMENT_SPEED.x, MIN_MAX_MOVEMENT_SPEED.y);

			SetScale (ref container.leftSide, ref container.rightSide);

			return container;
		}

		protected override void SetScale(ref ObstacleDataContainerSide leftSide, ref ObstacleDataContainerSide rightSide)
		{
			float firstScale = Random.Range (MINIMUM_SCALE, 1f - MINIMUM_OBSTACLE_GAP - MINIMUM_SCALE);
			float secondScale = (1f - firstScale) - MINIMUM_OBSTACLE_GAP;

			if(Random.value >= 0.5f)
			{
				leftSide.scale = new Vector2(OBSTACLE_HEIGHT, firstScale);
				rightSide.scale = new Vector2(OBSTACLE_HEIGHT, secondScale);
			}
			else
			{
				leftSide.scale = new Vector2(OBSTACLE_HEIGHT, secondScale);
				rightSide.scale = new Vector2(OBSTACLE_HEIGHT, firstScale);
			}

		}

		protected override ObstacleDataContainer GetDefaultContainer ()
		{
			var container = new ObstacleDataContainer ();
			container.moveDirection = moveDirection;
			container.parentBoundsLocation = boundsLocation;
			container.parentLockedAxis = Axis.Y;

			container.leftSide.lockedScaleSide = Side.None;
			container.leftSide.boundsLocation = BoundsLocation.Top;
			container.leftSide.lockedAxis = Axis.None;
			container.leftSide.basedOnHeightOnly = true;

			container.rightSide.lockedScaleSide = Side.None;
			container.rightSide.boundsLocation = BoundsLocation.Bottom;
			container.rightSide.lockedAxis = Axis.None;
			container.rightSide.basedOnHeightOnly = true;

			return container;
		}
	}

	/// <summary>
	/// Builds vertical obstacle data.
	/// </summary>
	public class ObstacleVerticalDataImpl : ObstacleDirectionalDataImpl
	{
		protected static readonly float MINIMUM_OBSTACLE_GAP = 0.45f;
		protected static readonly Vector2 MIN_MAX_MOVEMENT_SPEED = new Vector2(1f, 2f);

		/// <summary>
		/// Initializes a new instance of the <see cref="Avoidance.ObstacleVerticalDataImpl"/> class.
		/// </summary>
		/// <param name="moveDirection">Move direction.</param>
		/// <param name="boundsLocation">Bounds location.</param>
		public ObstacleVerticalDataImpl(Direction moveDirection, 
			BoundsLocation boundsLocation):base(moveDirection, boundsLocation)
		{
		}

		/// <summary>
		/// Build ObstacleDataContainer for a vertical obstacle.
		/// </summary>
		public override ObstacleDataContainer Build()
		{
			ObstacleDataContainer container = GetDefaultContainer ();
			container.moveDistancePerSecond = Random.Range (MIN_MAX_MOVEMENT_SPEED.x, MIN_MAX_MOVEMENT_SPEED.y);
			SetScale (ref container.leftSide, ref container.rightSide);

			return container;
		}

		protected override void SetScale(ref ObstacleDataContainerSide leftSide, ref ObstacleDataContainerSide rightSide)
		{
			float firstScale = Random.Range (MINIMUM_SCALE, 1f - MINIMUM_OBSTACLE_GAP - MINIMUM_SCALE);
			float secondScale = (1f - firstScale) - MINIMUM_OBSTACLE_GAP;

			if(Random.value >= 0.5f)
			{
				leftSide.scale = new Vector2(firstScale, OBSTACLE_HEIGHT);
				rightSide.scale = new Vector2(secondScale, OBSTACLE_HEIGHT);
			}
			else
			{
				leftSide.scale = new Vector2(secondScale, OBSTACLE_HEIGHT);
				rightSide.scale = new Vector2(firstScale, OBSTACLE_HEIGHT);
			}

		}

		protected override ObstacleDataContainer GetDefaultContainer ()
		{
			var container = new ObstacleDataContainer ();

			container.moveDirection = moveDirection;
			container.parentBoundsLocation = boundsLocation;
			container.parentLockedAxis = Axis.X;

			container.leftSide.lockedScaleSide = Side.None;
			container.leftSide.boundsLocation = BoundsLocation.Left;
			container.leftSide.lockedAxis = Axis.None;


			container.rightSide.lockedScaleSide = Side.None;
			container.rightSide.boundsLocation = BoundsLocation.Right;
			container.rightSide.lockedAxis = Axis.None;

			return container;
		}
	}
		

}
