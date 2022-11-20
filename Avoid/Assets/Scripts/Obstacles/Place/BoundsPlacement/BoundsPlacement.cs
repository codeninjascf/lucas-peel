using UnityEngine;
using System.Collections;

namespace Avoidance
{
	/// <summary>
	/// Interface for objects that will be responsible for placing an objct at a specified location.
	/// </summary>
	public interface BoundsPlacement  
	{
		Vector2 GetDesiredPosition (SpriteRenderer s);
	}

	/// <summary>
	/// Returns location of <see cref="Avoidance.BoundsLocation.Left"/>. 
	/// </summary>
	public class BoundsPlacementLeft : BoundsPlacement
	{
		public Vector2 GetDesiredPosition (SpriteRenderer s)
		{
			float halfWidth = s.Width () * 0.5f;

			float leftBounds = Camera.main.OrthographicBounds ().min.x;

			return new Vector2 (
				leftBounds + halfWidth,
				s.transform.position.y);
		}
	}

	/// <summary>
	/// Returns location of <see cref="Avoidance.BoundsLocation.LeftOffScreen"/>. 
	/// </summary>
	public class BoundsPlacementLeftOffScreen : BoundsPlacement
	{
		public Vector2 GetDesiredPosition (SpriteRenderer s)
		{
			float halfWidth = s.Width () * 0.5f;

			float leftBounds = Camera.main.OrthographicBounds ().min.x;

			return new Vector2 (
				leftBounds - halfWidth,
				s.transform.position.y);
		}
	}

	/// <summary>
	/// Returns location of <see cref="Avoidance.BoundsLocation.Right"/>. 
	/// </summary>
	public class BoundsPlacementRight : BoundsPlacement
	{
		public Vector2 GetDesiredPosition (SpriteRenderer s)
		{
			float halfWidth = s.Width () * 0.5f;

			float rightBounds = Camera.main.OrthographicBounds ().max.x;

			return new Vector2 (
				rightBounds - halfWidth,
				s.transform.position.y);
		}
	} 

	/// <summary>
	/// Returns location of <see cref="Avoidance.BoundsLocation.RightOffScreen"/>. 
	/// </summary>
	public class BoundsPlacementRightOffScreen : BoundsPlacement
	{
		public Vector2 GetDesiredPosition (SpriteRenderer s)
		{
			float halfWidth = s.Width () * 0.5f;

			float rightBounds = Camera.main.OrthographicBounds ().max.x;

			return new Vector2 (
				rightBounds + halfWidth,
				s.transform.position.y);
		}
	} 

	/// <summary>
	/// Returns location of <see cref="Avoidance.BoundsLocation.Top"/>. 
	/// </summary>
	public class BoundsPlacementTop : BoundsPlacement
	{
		public Vector2 GetDesiredPosition (SpriteRenderer s)
		{
			float halfHeight = s.Height () * 0.5f;

			float topBounds = Camera.main.OrthographicBounds ().max.y;

			return new Vector2 (
				s.transform.position.x,
				topBounds - halfHeight);
		}
	} 

	/// <summary>
	/// Returns location of <see cref="Avoidance.BoundsLocation.TopOffScreen"/>. 
	/// </summary>
	public class BoundsPlacementTopOffScreen : BoundsPlacement
	{
		public Vector2 GetDesiredPosition (SpriteRenderer s)
		{
			float halfHeight = s.Height () * 0.5f;

			float topBounds = Camera.main.OrthographicBounds ().max.y;

			return new Vector2 (
				s.transform.position.x,
				topBounds + halfHeight);
		}
	} 

	/// <summary>
	/// Returns location of <see cref="Avoidance.BoundsLocation.Bottom"/>. 
	/// </summary>
	public class BoundsPlacementBottom : BoundsPlacement
	{
		public Vector2 GetDesiredPosition (SpriteRenderer s)
		{
			float halfHeight = s.Height () * 0.5f;

			float lowerBounds = Camera.main.OrthographicBounds ().min.y;

			return new Vector2 (
				s.transform.position.x,
				lowerBounds + halfHeight);
		}
	} 

	/// <summary>
	/// Returns location of <see cref="Avoidance.BoundsLocation.BottomOffScreen"/>. 
	/// </summary>
	public class BoundsPlacementBottomOffScreen : BoundsPlacement
	{
		public Vector2 GetDesiredPosition (SpriteRenderer s)
		{
			float halfHeight = s.Height () * 0.5f;

			float lowerBounds = Camera.main.OrthographicBounds ().min.y;

			return new Vector2 (
				s.transform.position.x,
				lowerBounds - halfHeight);
		}
	} 
}