using UnityEngine;
using System.Collections;

namespace Avoidance
{
	/// <summary>
	/// Returns GameObject to pool when collision occurs with object that has specified tag.
	/// </summary>
	public class PoolWhenHitObjectWithTag : MonoBehaviour 
	{
		/// <summary>
		/// Effect to instantiate on hit.
		/// </summary>
		public GameObject onHitEffect;

		/// <summary>
		/// Audio clips to play on hit. One is randomly selected.
		/// </summary>
        public AudioClip[] onHitAudioClips;

		/// <summary>
		/// The object to pool on hit. Normally set to the objects parent object.
		/// </summary>
		public GameObject objectToPool;

		/// <summary>
		/// The tag of the collision  object.
		/// </summary>
		public string hitTag = "Projectile";

		void OnTriggerEnter2D(Collider2D other)
		{
			if(other.CompareTag (hitTag))
			{
				OnDeath ();
			}
		}

		private void OnDeath()
		{
            if(onHitAudioClips != null && onHitAudioClips.Length > 0)
            {
                MusicAudioPlayer.instance.PlayOneShot(onHitAudioClips[Random.Range(0, onHitAudioClips.Length)]);
            }

			if (onHitEffect != null) 
			{
				SpawnDamageEffect ();
			}

			ObjectPool.instance.PoolObject (objectToPool);
		}

		private void SpawnDamageEffect()
		{
			var deathEffect = ObjectPool.instance.GetObjectForType (onHitEffect.name, false);
			deathEffect.transform.position = transform.position;
			deathEffect.SetActive (true);
		}
	}
}
