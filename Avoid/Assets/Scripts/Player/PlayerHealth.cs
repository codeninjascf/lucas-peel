using UnityEngine;
using System.Collections;

namespace Avoidance
{
	/// <summary>
	/// Maintains players health. Responsible for spawning damage effects and playing hit audio.
	/// </summary>
	public class PlayerHealth : MonoBehaviour 
	{
		/// <summary>
		/// The effect to spawn on hit.
		/// </summary>
		public GameObject playerDamageEffect;

		/// <summary>
		/// The number of hits the player can take.
		/// </summary>
		public int hitPoints = 10;

		/// <summary>
		/// Clips to play on hit. A random clip is selected.
		/// </summary>
        public AudioClip[] onHitAudioClips;

		private float _currentScale;
		private float _scaleMultiplier;
		private Vector2 _initialScale;
		private Vector2 _minimumScale;
		private bool _isDead;

		void Start()
		{
			_isDead = false;
			_initialScale = transform.localScale;
			_minimumScale = _initialScale * 0.6f;

		}

		/// <summary>
		/// Applies damage, spawns damage effect, plays audio, and scales player down.
		/// </summary>
		public void ApplyDamage()
		{
			if (!_isDead) 
			{
                if(onHitAudioClips != null && onHitAudioClips.Length > 0)
                {
                    MusicAudioPlayer.instance.PlayOneShot(onHitAudioClips[Random.Range(0, onHitAudioClips.Length)]);
                }

				SpawnDamageEffect ();
				ScaleDown ();
			}
		}

		void OnEnable()
		{
			_currentScale = 0f;
			_scaleMultiplier = 1f / hitPoints;
		}

		private void SpawnDamageEffect()
		{
			var deathEffect = ObjectPool.instance.GetObjectForType (playerDamageEffect.name, false);
			deathEffect.transform.position = transform.position;
			deathEffect.SetActive (true);
		}

		private void ScaleDown()
		{
			_currentScale += _scaleMultiplier;

			transform.localScale = Vector2.Lerp (_initialScale, _minimumScale, _currentScale);

			if(_currentScale >= 1f)
			{
				OnDead ();
			}
		}


		private void OnDead()
		{
			_isDead = true;

			GameStateController.instance.OnGameOver ();
			Destroy (gameObject);
		}
	}
}
