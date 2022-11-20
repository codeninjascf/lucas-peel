using UnityEngine;
using System.Collections;

namespace Avoidance
{
	/// <summary>
	/// Responsible for spawning projectiles in a 360 arc, when scale complete.
	/// </summary>
    [RequireComponent(typeof(Scale))]
    public class Spawn360ProjectilesOnScaleComplete : MonoBehaviour
    {
		/// <summary>
		/// The projectile prefab.
		/// </summary>
        public GameObject projectilePrefab;

		/// <summary>
		/// The number of projectiles to spawn minimum/maximum. A random number is selected each time.
		/// </summary>
        public Vector2 numToSpawnMinMax = new Vector2(4, 9);

		/// <summary>
		/// The object to pool when scale complete.
		/// </summary>
        public GameObject objectToPool;

		/// <summary>
		/// The delay between each projectile spawn.
		/// </summary>
        public float delay = 0.5f;

		/// <summary>
		/// The speed to scale down after shooting.
		/// </summary>
        public float scaleDownSpeed;

		/// <summary>
		/// The clip to play on projectile spawn.
		/// </summary>
        public AudioClip clipOnProjectileSpawn;

        private Scale _scale;
        private Scale _projectileScale;
        private int _angleToAdd;
        private Scaler _scaleDown;
        private int _numToSpawn;
        private int _currentIndex = -1;

        void Awake()
        {
            _scale = GetComponent<Scale>();
        }

        void OnEnable()
        {
            _currentIndex = -1;

            _numToSpawn = (int)Random.Range(numToSpawnMinMax.x, numToSpawnMinMax.y);
            _angleToAdd = 360 / _numToSpawn;

            _scale.onScaleComplete += SpawnObject;
            _scaleDown = null;
        }

        void OnDisable()
        {
            _scale.onScaleComplete -= SpawnObject;

            RemoveProjectileScaleListener();
        }

        void Update()
        {
            if (_scaleDown != null)
            {
                if (!_scaleDown.IsComplete())
                {
                    _scaleDown.Scale();
                }
                else
                {
                    ObjectPool.instance.PoolObject(objectToPool);
                }
            }
        }

        private void SpawnObject()
        {
            StartCoroutine(SpawnObjectAfterDelay());
        }

        private IEnumerator SpawnObjectAfterDelay()
        {
            for (int i = 0; i < _numToSpawn; i++)
            {
                var spawnedObject = GetActiveObjectFromPool();

                int index = GetNextIndex();

                float x = Mathf.Sin(index * _angleToAdd * Mathf.PI / 180f);
                float y = Mathf.Cos(index * _angleToAdd * Mathf.PI / 180f);

                spawnedObject.GetComponent<ApplyForceOnScaleComplete>().direction = new Vector2(x, y);

                if (i == _numToSpawn - 1)
                {
                    RemoveProjectileScaleListener();
                    _projectileScale = spawnedObject.GetComponentInChildren<Scale>();
                    _projectileScale.onScaleComplete += ScaleDown;
                }

                if(clipOnProjectileSpawn != null)
                {
                    MusicAudioPlayer.instance.PlayOneShot(clipOnProjectileSpawn);
                }

                yield return new WaitForSeconds(delay);
            }
        }
      
        private int GetNextIndex()
        {
            if (_currentIndex == -1)
            {
                _currentIndex = Random.Range(0, _numToSpawn + 1);
            }
            else
            {
                _currentIndex = (_currentIndex + 1) % _numToSpawn;
            }

            return _currentIndex;
        }

        private GameObject GetActiveObjectFromPool()
        {
            var spawnedObject = ObjectPool.instance.GetObjectForType(projectilePrefab.name, false);
            spawnedObject.SetActive(true);
            spawnedObject.transform.position = transform.position;

            return spawnedObject;
        }

        private void RemoveProjectileScaleListener()
        {
            if (_projectileScale != null)
            {
                _projectileScale.onScaleComplete -= ScaleDown;
            }
        }

        private void ScaleDown()
        {
            var data = new ScaleData(transform, transform.localScale, scaleDownSpeed);
            _scaleDown = new ScalerFactory().Make(Scale.ScaleType.Down, data);
        }
    }
}
