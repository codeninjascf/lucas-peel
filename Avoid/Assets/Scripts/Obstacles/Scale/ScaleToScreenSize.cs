using UnityEngine;
using System.Collections;

namespace Avoidance
{
	/// <summary>
	/// Side to scale.
	/// </summary>
	public enum Side
	{
		None,
		Width,
		Height
	}

	/// <summary>
	/// Scales an object to a percentage of the current screensize.
	/// </summary>
	[RequireComponent(typeof(SpriteRenderer))]
	public class ScaleToScreenSize : MonoBehaviour 
	{
		/// <summary>
		/// The percentage of the screen size to scale object.
		/// </summary>
		public Vector2 multiplyBy = Vector2.one;

		/// <summary>
		/// Locks the scale so sides are equal.
		/// </summary>
		public Side lockRatioTo = Side.None;

		/// <summary>
		/// Performs scale on Start.
		/// </summary>
		public bool scaleOnStart = false;

		private SpriteRenderer _spriteRenderer;

		void Awake () 
		{
			if (_spriteRenderer == null) 
			{
				_spriteRenderer = GetComponent<SpriteRenderer> ();
			}

			if(scaleOnStart)
			{
				Setup (multiplyBy, lockRatioTo, false);
			}
		}

		/// <summary>
		/// Simulates the specified scale. Returns scale.
		/// </summary>
		/// <param name="basedOnHeightOnly">If set to <c>true</c> scales based on height of screen only.</param>
		public Vector2 Simulate(bool basedOnHeightOnly)
		{
			if (_spriteRenderer == null) 
			{
				_spriteRenderer = GetComponent<SpriteRenderer> ();
			}

			float width = _spriteRenderer.sprite.bounds.size.x;
			float height = _spriteRenderer.sprite.bounds.size.y;

			float worldScreenHeight = Camera.main.orthographicSize * 2.0f;
			float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

			Vector2 scale = Vector2.zero;

			if (!basedOnHeightOnly) 
			{
				scale = new Vector2 (
					(worldScreenWidth / width) * multiplyBy.x, 
					(worldScreenHeight / height) * multiplyBy.y);
			}
			else
			{
				scale = new Vector2 (
					(worldScreenHeight / width) * multiplyBy.x, 
					(worldScreenHeight / height) * multiplyBy.y);
			}

			if(lockRatioTo == Side.Width)
			{
				scale = new Vector2 (scale.x, scale.x);
			}
			else if(lockRatioTo == Side.Height)
			{
				scale = new Vector2 (scale.y, scale.y);
			}

			return scale;
		}
			
		/// <summary>
		/// Performs scale.
		/// </summary>
		/// <param name="multiplyBy">Scale multiplier.</param>
		/// <param name="lockRatioTo">Lock ratio to a specified side.</param>
		/// <param name="basedOnHeightOnly">If set to <c>true</c> based on height of screen only.</param>
		public void Setup(Vector2 multiplyBy, Side lockRatioTo, bool basedOnHeightOnly)
		{
			this.multiplyBy = multiplyBy;
			this.lockRatioTo = lockRatioTo;

			transform.localScale = Simulate (basedOnHeightOnly);
		}
			

			
	}
}
