using UnityEngine;
using System.Collections;

namespace Avoidance
{
	/// <summary>
	/// Responsible for spawning a single projectile when scale complete.
	/// </summary>
	[RequireComponent(typeof(Scale))]
	public class SpawnProjectileOnScaleComplete : MonoBehaviour 
	{
		/// <summary>
		/// The projectile prefab to spawn.
		/// </summary>
		public GameObject projectilePrefab;

		/// <summary>
		/// The scale down speed.
		/// </summary>
		public float scaleDownSpeed;

		/// <summary>
		/// The object to pool when scale down complete.
		/// </summary>
		public GameObject objectToPool;

		/// <summary>
		/// The delay before spawning projectile.
		/// </summary>
		public float delay = 0.5f;

		/// <summary>
		/// The clip to play on projectile spawn.
		/// </summary>
        public AudioClip clipOnProjectileSpawn;

		private Scale _scale;
		private Scale _projectileScale;
		private Scaler _scaleDown;

		void Awake()
		{
			_scale = GetComponent<Scale> ();
		}

		void OnEnable () 
		{
			_scale.onScaleComplete += SpawnObject;
			_scaleDown = null;
		}

		void OnDisable()
		{
			_scale.onScaleComplete -= SpawnObject;

			RemoveProjectileScaleListener ();
		}

		void Update()
		{
			if(_scaleDown != null)
			{
				if (!_scaleDown.IsComplete ()) 
				{
					_scaleDown.Scale ();
				} 
				else 
				{
					ObjectPool.instance.PoolObject (objectToPool);
				}
			}
		}

		private void SpawnObject()
		{
			StartCoroutine (SpawnObjectAfterDelay ());
		}

		private IEnumerator SpawnObjectAfterDelay()
		{
			yield return new WaitForSeconds (delay);
			var spawnedObject = ObjectPool.instance.GetObjectForType (projectilePrefab.name, false);
			spawnedObject.SetActive (true);
			spawnedObject.transform.position = transform.position;

			RemoveProjectileScaleListener ();

			_projectileScale = spawnedObject.GetComponentInChildren<Scale> ();

			_projectileScale.onScaleComplete += ScaleDown;
		}

		private void RemoveProjectileScaleListener()
		{
			if(_projectileScale != null)
			{
				_projectileScale.onScaleComplete -= ScaleDown;
			}
		}

		private void ScaleDown()
		{
            if(clipOnProjectileSpawn != null)
            {
                MusicAudioPlayer.instance.PlayOneShot(clipOnProjectileSpawn);
            }

			var data = new ScaleData (transform, transform.localScale, scaleDownSpeed);
			_scaleDown = new ScalerFactory ().Make (Scale.ScaleType.Down, data);
		}

	}
}
