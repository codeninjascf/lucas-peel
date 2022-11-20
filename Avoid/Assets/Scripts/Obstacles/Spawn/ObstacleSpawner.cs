using UnityEngine;
using System.Collections;

namespace Avoidance
{
	/// <summary>
	/// Direction to spawn obstacles.
	/// </summary>
	public enum ObstacleSpawnDirection
	{
		Horizontal = 0,
		Vertical = 100
	}

	/// <summary>
	/// Spawns obstacles in a specified direction (horizontal or vertical).
	/// </summary>
	public class ObstacleSpawner : Startable 
	{
		/// <summary>
		/// The obstacle prefab.
		/// </summary>
		public GameObject obstaclePrefab;

		/// <summary>
		/// The direction to spawn obstacles.
		/// </summary>
		public ObstacleSpawnDirection spawnDirection = ObstacleSpawnDirection.Horizontal;

		private ObstacleDirectionSpawner _directionalSpawner;
		private ActionOnDisabled _actionWhenDisabled;

		/// <summary>
		/// Initialise instance and begins spawn.
		/// </summary>
		public override void OnStart()
		{
            _directionalSpawner = new ObstacleDirectionalSpawnerFactory().Make(spawnDirection);
            SpawnShape ();
		}

		void OnDisable()
		{
			RemoveListener ();
		}

		/// <summary>
		/// Spawns and enables obstacle. Adds listener to spawn another object when this obstacle is disabled (when the obstacle leaves bounds).
		/// </summary>
		public void SpawnShape()
		{
			if(IsGameOver ())
			{
				return;
			}
			
			GameObject obstacle;
			
			try
			{
				obstacle = _directionalSpawner.SpawnNew (obstaclePrefab.name);
			}
			catch(UnityException)
			{
				return;
			}
		
			obstacle.SetActive (true);

			RemoveListener ();

			_actionWhenDisabled = obstacle.GetComponent<ActionOnDisabled> ();
		
			AddListener ();
		}

		private bool IsGameOver()
		{
			return GameStateController.instance == null || GameStateController.instance.isGameOver;
		}

		private void RemoveListener()
		{
			if(_actionWhenDisabled != null)
			{
				_actionWhenDisabled.onDisabled -= SpawnShape;
			}
		}

		private void AddListener()
		{
			if(_actionWhenDisabled != null)
			{
				_actionWhenDisabled.onDisabled += SpawnShape;
			}	
		}
	}
}
