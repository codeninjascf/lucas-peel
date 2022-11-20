using UnityEngine;
using System.Collections;

namespace Avoidance
{
	/// <summary>
	/// Spawns enemies off screen after a delay.
	/// </summary>
	public class EnemySpawner : Startable 
	{
		/// <summary>
		/// The enemy prefab to spawn.
		/// </summary>
		public GameObject enemyPrefab;

		/// <summary>
		/// A delay between x and y is randomly selected.
		/// </summary>
		public Vector2 minMaxSpawnDelay;

		private ActionOnDisabled _enemyAction;

		public override void OnStart()
		{
			float delay = Random.Range (minMaxSpawnDelay.x, minMaxSpawnDelay.y);
			StartCoroutine (SpawnAfterDelay (delay));
		}

		void OnDisable()
		{
			RemoveListener ();
		}

		private void OnEnemyRemovedFromGame()
		{
			if(ObjectPool.instance == null)
			{
				return;
			}
			
			float delay = Random.Range (minMaxSpawnDelay.x, minMaxSpawnDelay.y);
			StartCoroutine (SpawnAfterDelay (delay));
		}

		private IEnumerator SpawnAfterDelay(float delay)
		{
			yield return new WaitForSeconds (delay);
			Spawn ();
		}

		private void Spawn()
		{
			var enemyObj = ObjectPool.instance.GetObjectForType (enemyPrefab.name, false);

			var renderer = enemyObj.GetComponentInChildren<SpriteRenderer> ();

			enemyObj.SetActive (true);

			Vector2 spawnLocation = PointWithinBounds.GetRandomWithoutBounds (
				renderer.sprite.bounds.size.x * renderer.gameObject.transform.localScale.x,
				renderer.sprite.bounds.size.y * renderer.gameObject.transform.localScale.y);


			enemyObj.transform.position = spawnLocation;

			RemoveListener ();

			_enemyAction = enemyObj.GetComponent<ActionOnDisabled> ();

			AddListener ();
		}

		private void AddListener()
		{
			if(_enemyAction != null)
			{
				_enemyAction.onDisabled += OnEnemyRemovedFromGame;
			}
		}

		private void RemoveListener()
		{
			if(_enemyAction != null)
			{
				_enemyAction.onDisabled -= OnEnemyRemovedFromGame;
			}
		}
	}
}
