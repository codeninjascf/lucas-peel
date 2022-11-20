using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Avoidance
{
	/// <summary>
	/// Maintains list of disabled gameobjects. Reduces calls to Instantiate and Destroy.
	/// </summary>
	public class ObjectPool : Singleton<ObjectPool>
	{
		/// <summary>
		/// Items to add to object pool.
		/// </summary>
		public ObjectPoolItem[] ObjectPoolItems;

		/// <summary>
		/// The pooled objects currently available.
		/// </summary>
		public List<GameObject>[] pooledObjects;


		/// <summary>
		/// The container object that we will keep unused pooled objects so we dont clog up the editor with objects.
		/// </summary>
		protected GameObject containerObject;


		private bool _init;

		// Use this for initialization
		void Start ()
		{
			if (!_init) {
				Initialise ();
			}					
		}

		private void Initialise ()
		{
			containerObject = gameObject;

			pooledObjects = new List<GameObject>[ObjectPoolItems.Length];

			int i = 0;

			foreach (var item in ObjectPoolItems) {
				pooledObjects [i] = new List<GameObject> (); 

				for (int n = 0; n < item.BufferAmount; n++) {
					GameObject newObj = Instantiate (item.ObjectPrefab) as GameObject;
					newObj.name = item.ObjectPrefab.name;
					newObj.SetActive (false);
					PoolObject (newObj);
				}

				i++;
			}

			_init = true;
		}

		/// <summary>
		/// Gets a new object for the name type provided.  If no object type exists or if onlypooled is true and there is no objects of that type in the pool
		/// then null will be returned.
		/// </summary>
		/// <returns>
		/// The object for type.
		/// </returns>
		/// <param name='objectType'>
		/// Object type.
		/// </param>
		/// <param name='onlyPooled'>
		/// If true, it will only return an object if there is one currently pooled.
		/// </param>
		public GameObject GetObjectForType (string objectType, bool onlyPooled)
		{
			if (!_init) {
				Initialise ();
			}	

			for (int i = 0; i < ObjectPoolItems.Length; i++) {
				var prefab = ObjectPoolItems [i].ObjectPrefab;
				if (prefab.name == objectType) {

					if (pooledObjects [i].Count > 0) {
						GameObject pooledObject = pooledObjects [i] [0];
												
						if (pooledObject) {
							pooledObjects [i].RemoveAt (0);
							pooledObject.transform.SetParent (null, false);
						} 

						return pooledObject;

					} else if (!onlyPooled) {

						GameObject newObj = Instantiate (prefab) as GameObject;
						newObj.name = prefab.name;
						return newObj;
					}

					break;

				}
			}

			// No object of the specified type or none were left in the pool with onlyPooled set to true
			Debug.Log("No object of type " + objectType + " found");
			return null;
		}

		/// <summary>
		/// Pools the object specified.  Will not be pooled if there is no prefab of that type.
		/// </summary>
		/// <param name='obj'>
		/// Object to be pooled.
		/// </param>
		public void PoolObject (GameObject obj)
		{
			
			for (int i = 0; i < ObjectPoolItems.Length; i++) {
				if (ObjectPoolItems [i].ObjectPrefab.name == obj.name) {
					obj.SetActive (false);
					obj.transform.SetParent (containerObject.transform, true);
					pooledObjects [i].Add (obj);
					return;
				}
			}

			Debug.LogWarning ("Object not pooled: " + obj.name);
		}
			

	}

	[System.Serializable]
	public class ObjectPoolItem
	{
		public GameObject ObjectPrefab;
		public int BufferAmount = 1;
	}

}
