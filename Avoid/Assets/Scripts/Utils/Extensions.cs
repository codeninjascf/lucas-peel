using UnityEngine;
using System.Collections;

namespace Avoidance
{
	/// <summary>
	/// Extension methods.
	/// </summary>
	public static class Extensions  
	{
		/// <summary>
		/// Gets the orthographics bounds for the camera.
		/// </summary>
		/// <returns>The bounds.</returns>
		/// <param name="camera">Camera.</param>
		public static Bounds OrthographicBounds(this Camera camera)
		{
			float screenAspect = (float)Screen.width / (float)Screen.height;
			float cameraHeight = camera.orthographicSize * 2;
			Bounds bounds = new Bounds(
				camera.transform.position,
				new Vector3(cameraHeight * screenAspect, cameraHeight, 0));
			return bounds;
		}

		/// <summary>
		/// Returns height of sprite attached to Spriterenderer.
		/// </summary>
		/// <param name="s">Spriterenderer.</param>
		public static float Height(this SpriteRenderer s)
		{
			return s.sprite.bounds.size.y * s.transform.localScale.y;
		}

		/// <summary>
		/// Returns width of sprite attached to Spriterenderer.
		/// </summary>
		/// <param name="s">Spriterenderer.</param>
		public static float Width(this SpriteRenderer s)
		{
			return s.sprite.bounds.size.x * s.transform.localScale.x;
		}
	}
}
