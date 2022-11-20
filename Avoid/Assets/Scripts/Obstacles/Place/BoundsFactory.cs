using UnityEngine;
using System.Collections;

namespace Avoidance
{
	/// <summary>
	/// <see cref="Avoidance.BoundsPlacement"/> factory.
	/// </summary>
	public class BoundsFactory  
	{
		/// <summary>
		/// Creates and returns a <see cref="Avoidance.BoundsPlacement"/> based on <see cref="Avoidance.BoundsLocation"/>.
		/// </summary>
		public BoundsPlacement Make(BoundsLocation location)
		{
			BoundsPlacement boundsPlacement = null;

			switch(location)
			{
				case BoundsLocation.Top:
					boundsPlacement = new BoundsPlacementTop ();
						break;
				case BoundsLocation.TopOffScreen:
					boundsPlacement = new BoundsPlacementTopOffScreen ();
					break;
				case BoundsLocation.Right:
					boundsPlacement = new BoundsPlacementRight ();
						break;
				case BoundsLocation.RightOffScreen:
					boundsPlacement = new BoundsPlacementRightOffScreen ();
					break;
				case BoundsLocation.Left:
					boundsPlacement = new BoundsPlacementLeft ();
					break;
				case BoundsLocation.LeftOffScreen:
					boundsPlacement = new BoundsPlacementLeftOffScreen ();
					break;
				case BoundsLocation.Bottom:
					boundsPlacement = new BoundsPlacementBottom ();
					break;
				case BoundsLocation.BottomOffScreen:
					boundsPlacement = new BoundsPlacementBottomOffScreen ();
					break;
				default:
					Debug.LogError ("Bounds placement not set");
					break;
			}

			return boundsPlacement;
		}
	}
}