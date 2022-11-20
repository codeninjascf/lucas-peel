using UnityEngine;
using System;
using System.Collections;

namespace Avoidance
{	
	/// <summary>
	/// Performs bounds checks each frame. Raises events when an object enters bounds and when it leaves bounds.
	/// </summary>
	public class BoundsCheck : MonoBehaviour
	{
		/// <summary>
		/// Raised when object enters bounds.
		/// </summary>
		public Action onEnterBounds;

		/// <summary>
		/// Raised when object has entered bounds and then elaves bounds.
		/// </summary>
		public Action onLeftBounds;

		private static readonly float BOUNDS_OFFSET = 1f;

		private CheckAction enterBounds;
		private CheckAction leaveBounds;
		private bool enterBoundsRaised;
		private bool leaveBoundsRaised;

		void Start()
		{
			enterBounds = new CheckAction (new CheckDelegate[]{IsInBounds});
			leaveBounds = new CheckAction (new CheckDelegate[]{enterBounds.HasPassedCheck, IsOutOfBounds});
		}

		/// <summary>
		/// Determines whether this instance is within the camera bounds.
		/// </summary>
		/// <returns><c>true</c> if this instance is in bounds; otherwise, <c>false</c>.</returns>
		public bool IsInBounds()
		{
			var cameraBounds = Camera.main.OrthographicBounds ();

			return transform.position.x >= cameraBounds.min.x - BOUNDS_OFFSET &&
				transform.position.x <= cameraBounds.max.x + BOUNDS_OFFSET &&
				transform.position.y >= cameraBounds.min.y - BOUNDS_OFFSET &&
				transform.position.y <= cameraBounds.max.y + BOUNDS_OFFSET;
		}

		/// <summary>
		/// Determines whether this instance is within the camera bounds.
		/// </summary>
		/// <returns><c>true</c> if this instance is out of bounds; otherwise, <c>false</c>.</returns>
		public bool IsOutOfBounds()
		{
			return !IsInBounds ();
		}

		void OnEnable()
		{
			enterBoundsRaised = false;
			leaveBoundsRaised = false;

			if (enterBounds != null) 
			{
				enterBounds.Reset ();
			}

			if (leaveBounds != null) 
			{
				leaveBounds.Reset ();
			}
		}

		void Update()
		{
			if(AllEventsRaised ())
			{
				return;
			}
				

			if(ShouldRaiseEnteredBounds ())
			{
				RaiseEnteredBounds ();
			}
			else if (ShouldRaiseLeftBounds ())
			{
				RaiseLeftBounds ();
			}
		}

		private bool AllEventsRaised()
		{
			return enterBoundsRaised && leaveBoundsRaised;
		}
			
		private bool ShouldRaiseEnteredBounds()
		{
			return !enterBoundsRaised && enterBounds.HasPassedCheck ();
		}

		private void RaiseEnteredBounds()
		{
			if(onEnterBounds != null)
			{
				onEnterBounds ();
			}

			enterBoundsRaised = true;
		}

		private bool ShouldRaiseLeftBounds()
		{
			return !leaveBoundsRaised && leaveBounds.HasPassedCheck ();
		}

		private void RaiseLeftBounds()
		{
			if(onLeftBounds != null)
			{
				onLeftBounds ();
			}

			leaveBoundsRaised = true;
		}



	
	}
}
