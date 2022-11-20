using UnityEngine;
using System.Collections;

namespace Avoidance
{
	/// <summary>
	/// Pools object when it exits camera bounds.
	/// </summary>
	[RequireComponent(typeof(BoundsCheck))]
	public class PoolObjectOutOfBounds : MonoBehaviour 
	{
		private BoundsCheck _boundsCheck;

		void Awake()
		{
			_boundsCheck = GetComponent<BoundsCheck> ();
		}

		void OnEnable()
		{
			_boundsCheck.onLeftBounds += PoolGameObject;
		}

		void OnDisable()
		{
			_boundsCheck.onLeftBounds -= PoolGameObject;
		}
			
		private void PoolGameObject()
		{
			ObjectPool.instance.PoolObject (gameObject);
		}

	}
}
