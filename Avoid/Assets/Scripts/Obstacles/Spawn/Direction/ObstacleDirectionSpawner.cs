using UnityEngine;
using System.Collections;

namespace Avoidance
{
	public interface ObstacleDirectionSpawner  
	{
		GameObject SpawnNew(string prefabName);
	}

	public class ObstacleDirectionalSpawner : ObstacleDirectionSpawner
	{
		
		private ObstacleDirectionalData[] _dataBuilders;
		private int _currentIndex;

		public ObstacleDirectionalSpawner(ObstacleDirectionalData dataDirectionOne, 
			ObstacleDirectionalData dataDirectionTwo)
		{
			_dataBuilders = new ObstacleDirectionalData[2];
			_dataBuilders [0] = dataDirectionOne;
			_dataBuilders [1] = dataDirectionTwo;

			_currentIndex = Random.Range (0, 2);
		}
		
		public GameObject SpawnNew(string prefabName) 
		{
			if(ObjectPool.instance == null)
			{
				throw new UnityException ("Object pool not found");
			}
			
			var dataContainer = _dataBuilders [_currentIndex].Build ();

			var obsObject = ObjectPool.instance.GetObjectForType (prefabName, false);
			obsObject.transform.position = Vector3.zero;

			var dataBuilder = obsObject.GetComponent<ObstacleData> ();

			ApplyData (dataBuilder, dataContainer);

			SwitchLocations ();

			return obsObject;
		}

		private void SwitchLocations()
		{
			_currentIndex = (_currentIndex + 1) % _dataBuilders.Length;
		}

		private void ApplyData(ObstacleData data, ObstacleDataContainer dataContainer)
		{

			
			ApplyIndividualObstacleData (data, ObstacleData.ObstacleSide.Left, dataContainer.leftSide);
			ApplyIndividualObstacleData (data, ObstacleData.ObstacleSide.Right, dataContainer.rightSide);

			data.SetupParent (dataContainer.parentBoundsLocation, dataContainer.parentLockedAxis, 
				dataContainer.moveDirection, dataContainer.moveDistancePerSecond);

		
		}

		private void ApplyIndividualObstacleData(ObstacleData mainData, ObstacleData.ObstacleSide side, 
			ObstacleDataContainerSide sideData)
		{
			mainData.SetupObstacle (side, sideData.scale, 
				sideData.lockedScaleSide, sideData.boundsLocation, 
				sideData.lockedAxis, sideData.basedOnHeightOnly);
		}
	}

}
