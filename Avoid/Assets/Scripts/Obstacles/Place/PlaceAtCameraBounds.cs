using UnityEngine;
using System.Collections;

namespace Avoidance
{
	/// <summary>
	/// Camera bounds locations.
	/// </summary>
	public enum BoundsLocation
	{
		Top = 1,
		TopOffScreen = 2,
		Left = 200,
		LeftOffScreen = 201,
		Right = 300,
		RightOffScreen = 301,
		Bottom = 400,
		BottomOffScreen = 401
	}

	/// <summary>
	/// Possible 2D axis.
	/// </summary>
	public enum Axis
	{
		None = 0,
		X = 100,
		Y = 200,
		XY = 300
	}
			
	/// <summary>
	/// Places object at a specified camera bounds.
	/// </summary>
	public class PlaceAtCameraBounds : MonoBehaviour 
	{
		/// <summary>
		/// The sprite renderer of the objct to be placed.
		/// </summary>
		public SpriteRenderer spriteRenderer;

		/// <summary>
		/// The location to place the object.
		/// </summary>
		public BoundsLocation boundsLocation = BoundsLocation.Left;

		/// <summary>
		/// If an axis is locked it will not be moved from its current position.
		/// </summary>
		public Axis lockAxis = Axis.None;

		void Awake()
		{
			if (spriteRenderer == null) 
			{
				spriteRenderer = GetComponent<SpriteRenderer> ();
			}
		}
			
		/// <summary>
		/// Setup the specified instance. Places object at location.
		/// </summary>
		/// <param name="location">Location.</param>
		/// <param name="lockedAxis">Locked axis.</param>
		public void Setup(BoundsLocation location, Axis lockedAxis)
		{
			if(spriteRenderer == null)
			{
				Debug.LogError ("No spriterenderer found: stopping placement");
				return;
			}

			this.boundsLocation = location;
			this.lockAxis = lockedAxis;

			transform.localPosition = Vector3.zero;

			if(BothAxisLocked ())
			{
				Debug.Log ("Both Axis locked: stopping placement");
				return;
			}
				
			Vector2 desiredPosition = GetDesiredPosition ();

			if(lockAxis == Axis.X)
			{
				desiredPosition.x = transform.position.x;
			}
			else if(lockAxis == Axis.Y)
			{
				desiredPosition.y = transform.position.y;
			}

			transform.position = desiredPosition;
		}

		private bool BothAxisLocked()
		{
			return lockAxis == Axis.XY;
		}

		private Vector2 GetDesiredPosition()
		{
			BoundsPlacement placement = new BoundsFactory ().Make (boundsLocation);
			return placement.GetDesiredPosition (spriteRenderer);
		}
	}
}