using UnityEngine;
using System;
using System.Collections;

namespace Avoidance
{
	/// <summary>
	/// Spawns projectiles with a delay.
	/// </summary>
	public class ProjectileSpawner : Startable 
	{
		/// <summary>
		/// The projectile spawner prefab.
		/// </summary>
		public GameObject projectileSpawnerPrefab;

		/// <summary>
		/// The minimum and maximum time between spawns.
		/// </summary>
		public Vector2 minMaxTimeBetweenSpawns = new Vector2(2f, 10f);

		/// <summary>
		/// The minimum and maximum initial delay before spawn begins.
		/// </summary>
        public Vector2 minMaxTimeInitialDelay = Vector2.zero;

		[Range(0f, 1f)]
		/// <summary>
		/// The chance to spawn once delay is over. 0 = never, 1 = always.
		/// </summary>
		public float chanceToSpawn = 0.5f;

		/// <summary>
		/// Starts spawning projectiles.
		/// </summary>
		public override void OnStart ()
		{
			StartCoroutine (SpawnShapes ());
		}

		private IEnumerator SpawnShapes()
		{
            if(minMaxTimeInitialDelay != Vector2.zero)
            {
                yield return new WaitForSeconds(UnityEngine.Random.Range(minMaxTimeBetweenSpawns.x, minMaxTimeBetweenSpawns.y));
            }

			while(true)
			{
				float nextSpawnTime = GetNextSpawnTime ();

				yield return new WaitForSeconds (nextSpawnTime);

				if(UnityEngine.Random.value > chanceToSpawn)
				{
					continue;
				}

				try
				{
					SpawnPoint ();
				} 
				catch(UnityException)
				{
					break;
				}
			}
		}

		private float GetNextSpawnTime()
		{
			return UnityEngine.Random.Range (minMaxTimeBetweenSpawns.x, minMaxTimeBetweenSpawns.y);
		}

		private void SpawnPoint()
		{
			if(ObjectPool.instance == null)
			{
				throw new UnityException ("Object pool not found");
			}

			var projObj = ObjectPool.instance.GetObjectForType (projectileSpawnerPrefab.name, false);

			var renderer = projObj.GetComponentInChildren<SpriteRenderer> ();

			projObj.SetActive (true);

			Vector2 spawnLocation = PointWithinBounds.GetRandomWithinBounds (
				renderer.sprite.bounds.size.x * renderer.gameObject.transform.localScale.x,
				renderer.sprite.bounds.size.y * renderer.gameObject.transform.localScale.y);


			projObj.transform.position = spawnLocation;
		}
	}
}