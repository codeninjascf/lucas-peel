using UnityEngine;

namespace Avoidance
{
	/// <summary>
	/// Pools gameobject on contact with player.
	/// </summary>
	public class PoolGameobjectInteractable : MonoBehaviour, Interactable
	{
		/// <summary>
		/// The object to pool.
		/// </summary>
		public GameObject objectToPool;

		public void Interact(GameObject interacted)
		{
			ObjectPool.instance.PoolObject (objectToPool);
		}
	}
}
