using UnityEngine;
using System.Collections;

namespace Avoidance
{
	/// <summary>
	/// Spawns points within bounds of camera.
	/// </summary>
	public class PointsSpawner : Startable 
	{
		/// <summary>
		/// The points prefab.
		/// </summary>
		public GameObject pointsPrefab;

		/// <summary>
		/// The delay between each spawn.
		/// </summary>
		public float delayBetweenSpawn = 0.3f;

		private ActionOnDisabled _pointsAction;

		/// <summary>
		/// Begins spawning.
		/// </summary>
		public override void OnStart ()
		{
			Spawn ();
		}

		/// <summary>
		/// Spawns a point object. Called when current point is collected or removed from game.
		/// </summary>
		public void PointsCollected()
		{
			if(IsGameOver ())
			{
				return;
			}

			StartCoroutine (SpawnAfterDelay());
		}

		void OnDisable()
		{
			RemoveListener ();
		}

		private bool IsGameOver()
		{
			return GameStateController.instance == null || GameStateController.instance.isGameOver;
		}

		private IEnumerator SpawnAfterDelay()
		{
			yield return new WaitForSeconds (delayBetweenSpawn);

			if(IsGameOver ())
			{
				yield break;
			}

			Spawn ();
		}

		private void Spawn()
		{
			
			var pointObj = ObjectPool.instance.GetObjectForType (pointsPrefab.name, false);

			var renderer = pointObj.GetComponentInChildren<SpriteRenderer> ();

			pointObj.SetActive (true);

			Vector2 spawnLocation = PointWithinBounds.GetRandomWithinBounds (
				renderer.sprite.bounds.size.x * renderer.gameObject.transform.localScale.x,
				renderer.sprite.bounds.size.y * renderer.gameObject.transform.localScale.y);


			pointObj.transform.position = spawnLocation;

			AddEventOnPointCollected (pointObj);
		}

		private void AddEventOnPointCollected(GameObject pointObj)
		{
			RemoveListener ();
			_pointsAction = pointObj.GetComponent<ActionOnDisabled> (); 
			AddListener ();
		}

		private void AddListener()
		{
			if(_pointsAction != null)
			{
				_pointsAction.onDisabled += PointsCollected;
			}
		}

		private void RemoveListener()
		{
			if(_pointsAction != null)
			{
				_pointsAction.onDisabled -= PointsCollected;
			}
		}

	}


}
