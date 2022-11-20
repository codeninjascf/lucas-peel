using UnityEngine;
using System.Collections;

namespace Avoidance
{
	/// <summary>
	/// Pools object after time.
	/// </summary>
	public class PoolAfterTime : MonoBehaviour 
	{
		/// <summary>
		/// The time in seconds until object is pooled.
		/// </summary>
		public float timeInSeconds;

		private float _currentTime;

		void OnEnable () 
		{
			_currentTime = timeInSeconds;
		}

		void Update () 
		{
			_currentTime -= Time.deltaTime;

			if(_currentTime <= 0f)
			{
				ObjectPool.instance.PoolObject (gameObject);
			}
		}
	}
}
