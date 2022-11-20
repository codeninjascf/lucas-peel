using UnityEngine;
using System.Collections;

namespace Avoidance
{
	/// <summary>
	/// Provides access to random point within and without camera bounds.
	/// </summary>
	public static class PointWithinBounds
	{
		private static readonly float SIDE_BUFFER_PERCENTAGE = 0.1f;

		/// <summary>
		/// Gets a random point within camera bounds.
		/// </summary>
		/// <returns>The random point within bounds.</returns>
		/// <param name="spriteWidth">Sprite width.</param>
		/// <param name="spriteHeight">Sprite height.</param>
		public static Vector2 GetRandomWithinBounds(float spriteWidth, float spriteHeight)
		{
			float xBuffer = GetXBuffer (spriteWidth);
			float yBuffer = GetYBuffer (spriteHeight);


			return new Vector2 (
				Random.Range (GetMinX (spriteWidth) + xBuffer, GetMaxX (spriteWidth) - xBuffer),
				Random.Range (GetMinY (spriteHeight) + yBuffer, GetMaxY (spriteHeight) - yBuffer)) ;

		}

		/// <summary>
		/// Gets a random point without camera bounds.
		/// </summary>
		/// <returns>The random point within bounds.</returns>
		/// <param name="spriteWidth">Sprite width.</param>
		/// <param name="spriteHeight">Sprite height.</param>
		public static Vector2 GetRandomWithoutBounds(float spriteWidth, float spriteHeight)
		{
			float xBuffer = GetXBuffer (spriteWidth);
			float yBuffer = GetYBuffer (spriteHeight);

			if(Random.value > 0.5f)
			{
				return GetRandomVerticalWithoutBounds (spriteWidth, spriteHeight, xBuffer, yBuffer);
			}
			else
			{
				return GetRandomHorizontalWithoutBounds (spriteWidth, spriteHeight, xBuffer, yBuffer);
			}
		}

		private static Vector2 GetRandomVerticalWithoutBounds(float spriteWidth, float spriteHeight,
																		float xBuffer, float yBuffer)
		{
			float x = Random.Range (GetMinX (spriteWidth) - spriteWidth - xBuffer, GetMaxX (spriteWidth) + spriteWidth + xBuffer);

			float y = Random.value > 0.5f ? (GetMinY (spriteHeight) - spriteHeight - yBuffer) : (GetMaxY (spriteHeight) + spriteHeight + yBuffer);

			return new Vector2 (x, y);
		}

		private static Vector2 GetRandomHorizontalWithoutBounds(float spriteWidth, float spriteHeight,
			float xBuffer, float yBuffer)
		{
			float x = Random.value > 0.5f ? (GetMinX (spriteWidth) - spriteWidth - yBuffer) : (GetMaxX (spriteWidth) + spriteWidth + yBuffer);
			float y = Random.Range (GetMinY (spriteHeight) - spriteHeight - xBuffer, GetMaxY (spriteHeight) + spriteHeight + xBuffer);

			return new Vector2 (x, y);
		}

		private static float GetXBuffer(float spriteWidth)
		{
			return (GetMaxX (spriteWidth) - GetMinX (spriteWidth)) * SIDE_BUFFER_PERCENTAGE;
		}

		private static float GetYBuffer(float spriteHeight)
		{
			return (GetMaxY (spriteHeight) - GetMinY (spriteHeight)) * SIDE_BUFFER_PERCENTAGE;
		}

		private static float GetMinX(float spriteWidth)
		{
			return Camera.main.OrthographicBounds ().min.x + spriteWidth;
		}

		private static float GetMaxX(float spriteWidth)
		{
			return GetScreenWidth () - spriteWidth;
		}

		private static float GetMinY(float spriteHeight)
		{
			return Camera.main.OrthographicBounds ().min.y + spriteHeight;
		}

		private static float GetMaxY(float spriteHeight)
		{
			return GetScreenHeight () - spriteHeight;
		}

		private static float GetScreenHeight()
		{
			return Camera.main.orthographicSize;
		}

		private static float GetScreenWidth()
		{
			return Camera.main.orthographicSize / Screen.height * Screen.width;
		}
	}
}