using UnityEngine;
using System.Collections;

namespace Avoidance
{
	/// <summary>
	/// Scales a trail renderer to the same width as a sprites.
	/// </summary>
	public class ScaleTrailRendererToSpriteWidth : MonoBehaviour 
	{
		/// <summary>
		/// Continuously check for changes in sprite size and update trail renderer scale accordingly.
		/// </summary>
		public bool continouslyUpdate = true;

		/// <summary>
		/// The sprite renderer.
		/// </summary>
		public SpriteRenderer spriteRenderer;

		/// <summary>
		/// The trail renderer.
		/// </summary>
		public TrailRenderer trailRenderer;

		private float _cachedWidth;

		void Start () 
		{
			_cachedWidth = GetSpriteWidth ();

			ScaleTrailRendererWidth ();
		}

		void Update () 
		{
			if(continouslyUpdate)
			{
				var width = GetSpriteWidth ();

				if(_cachedWidth != width)
				{
					_cachedWidth = width;
					ScaleTrailRendererWidth ();
				}
			}
		
		}

		private void ScaleTrailRendererWidth()
		{
			trailRenderer.startWidth = _cachedWidth;
		}

		private float GetSpriteWidth()
		{
			return spriteRenderer.sprite.bounds.size.x * spriteRenderer.gameObject.transform.localScale.x;
		}
	}
}