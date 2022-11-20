using UnityEngine;
using System.Collections;

namespace Avoidance
{
	/// <summary>
	/// Responsible for playing all audio (including one shots).
	/// </summary>
    [RequireComponent(typeof(TS_AudioSettings))]
	public class MusicAudioPlayer : Singleton<MusicAudioPlayer>
	{
		private AudioSource[] _audioSources;
		private int _activeSourceIndex = 0;

		private AudioSource activeAudioSource { get { return _audioSources [_activeSourceIndex]; } }

		private TS_AudioSettings _settings;

		private float _effectsVolume { get {return _settings.effectsVolume * _settings.masterVolume; }}
		private float _musicVolume { get { return _settings.musicVolume * _settings.masterVolume; } }

		protected override void Awake ()
		{
            base.Awake();

			_settings = GetComponent<TS_AudioSettings> ();
            _settings.VolumeChanged += OnVolumeChange;

			_audioSources = new AudioSource[2];

			for (int i = 0; i < _audioSources.Length; i++) {
				var obj = new GameObject ("Audio Source " + (i + 1));
				_audioSources [i] = obj.AddComponent<AudioSource> ();
                _audioSources[i].loop = true;
                _audioSources[i].spatialBlend = 0f;
				obj.transform.SetParent (transform);
			}

            UpdateAudioVolume();
		}

		/// <summary>
		/// Plays clip: one shot.
		/// </summary>
		/// <param name="clip">Clip.</param>
        public void PlayOneShot(AudioClip clip)
        {
			activeAudioSource.PlayOneShot(clip, _effectsVolume);
        }

		/// <summary>
		/// Plays the specified clip. Fades to clip over fade duration.
		/// </summary>
		/// <param name="clip">Clip.</param>
		/// <param name="fadeDuration">Fade duration.</param>
		public void Play (AudioClip clip, float fadeDuration = 1f)
		{
			_activeSourceIndex = 1 - _activeSourceIndex;
            activeAudioSource.clip = clip;

			activeAudioSource.Play ();

            StartCoroutine(CrossFadeMusic(fadeDuration));
		}

		/// <summary>
		/// Gets the length of the active audio clip.
		/// </summary>
		/// <returns>The active audio clip length.</returns>
        public float GetActiveAudioClipLength ()
        {
            return activeAudioSource.clip != null ? activeAudioSource.clip.length : 0f;
        }

		/// <summary>
		/// Sets the pitch for the active audio source.
		/// </summary>
		/// <param name="pitch">Pitch.</param>
        public void SetPitch(float pitch)
        {
            activeAudioSource.pitch = pitch;
        }

		private IEnumerator CrossFadeMusic (float duration)
		{
			float percent = 0f;

			while (percent < 1f) {
				percent += Time.deltaTime * (1 / duration);

				activeAudioSource.volume = Mathf.Lerp (0, _musicVolume, percent);

				_audioSources [1 - _activeSourceIndex].volume = Mathf.Lerp (_musicVolume, 0, percent);

				yield return null;
			}
		}

        private void OnVolumeChange ()
        {
            UpdateAudioVolume();
        }

        private void UpdateAudioVolume()
        {
            foreach (var audio in _audioSources)
            {
                if (_musicVolume == 0f)
                {
                    audio.volume = 0.001f;
                }
                else
                {
                    audio.volume = _musicVolume;
                }

             
            }
        }
	}
}