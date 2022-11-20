using UnityEngine;
using System.Collections;

namespace Avoidance
{
	/// <summary>
	/// Encapsulates data attached to an obstacles. Provides centralised access.
	/// </summary>
	public class ObstacleData : MonoBehaviour 
	{
		[Header("Obstacle Parent")]
		/// <summary>
		/// Reference to PlaceAtCameraBounds. Attached to obstacle Gameobject.
		/// </summary>
		public PlaceAtCameraBounds placementMain;

		/// <summary>
		/// Reference to MoveInDirection. Attached to obstacle Gameobject.
		/// </summary>
		public MoveInDirection movement;

		[Header("Left Obstacle")]
		/// <summary>
		/// Reference to ObstacleIndividualData. Attached to obstacle Gameobject.
		/// </summary>
		public ObstacleIndividualData leftObstacleData;

		[Header("Right Obstacle")]
		/// <summary>
		/// Reference to ObstacleIndividualData. Attached to obstacle Gameobject.
		/// </summary>
		public ObstacleIndividualData rightObstacleData;

		/// <summary>
		/// Child obstacle side.
		/// </summary>
		public enum ObstacleSide
		{
			Left = 0,
			Right = 1
		}

		private ObstacleIndividualData[] _individualObstacleData;

		void Awake()
		{
			_individualObstacleData = new ObstacleIndividualData [2];
			_individualObstacleData [0] = leftObstacleData;
			_individualObstacleData [1] = rightObstacleData;
		}

		/// <summary>
		/// Setups the parent object.
		/// </summary>
		/// <param name="location">Location.</param>
		/// <param name="lockedAxis">Locked axis.</param>
		/// <param name="moveDirection">Move direction.</param>
		/// <param name="moveDistancePerSecond">Move distance per second.</param>
		public void SetupParent(BoundsLocation location, Axis lockedAxis,
			Direction moveDirection, float moveDistancePerSecond)
		{
			placementMain.Setup (location, lockedAxis);
			movement.Setup (moveDirection, moveDistancePerSecond);
		}

		/// <summary>
		/// Setups child obstacle.
		/// </summary>
		/// <param name="side">Side.</param>
		/// <param name="multiplyBy">Multiply by.</param>
		/// <param name="lockRatioTo">Lock ratio to.</param>
		/// <param name="location">Location.</param>
		/// <param name="lockedAxis">Locked axis.</param>
		/// <param name="basedOnHeightOnly">If set to <c>true</c> based on height only.</param>
		public void SetupObstacle(ObstacleSide side, Vector2 multiplyBy,
			Side lockRatioTo, BoundsLocation location, Axis lockedAxis, bool basedOnHeightOnly)
		{
			var data = GetDataFromSide (side);
			UpdateObstacleSideData (data, multiplyBy, lockRatioTo, 
				location, lockedAxis, basedOnHeightOnly);
		}

		private ObstacleIndividualData GetDataFromSide(ObstacleSide side)
		{
			return _individualObstacleData [(int)side];
		}

		private void UpdateObstacleSideData(ObstacleIndividualData data, Vector2 multiplyBy,
											Side lockRatioTo,
											BoundsLocation location, 
			Axis lockedAxis, bool basedOnHeightOnly)
		{
			data.scale.Setup (multiplyBy, lockRatioTo, basedOnHeightOnly);
			data.placement.Setup (location, lockedAxis);
		}

	}

	/// <summary>
	/// Data associated with an individual obstacle.
	/// </summary>
	[System.Serializable]
	public struct ObstacleIndividualData
	{
		public ScaleToScreenSize scale;
		public PlaceAtCameraBounds placement;
	}
		
}
