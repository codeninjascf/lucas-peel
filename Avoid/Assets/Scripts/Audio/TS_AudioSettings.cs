using UnityEngine;
using System;
using System.Collections;

namespace Avoidance
{
	/// <summary>
	/// Audio channel.
	/// </summary>
    public enum AudioChannel { Master, SFX, Music}

	/// <summary>
	/// Centralised audio settings.
	/// </summary>
    public class TS_AudioSettings : MonoBehaviour
    {

        [Range (0f, 1f),SerializeField]
        private float _masterVolume = 1f;
		/// <summary>
		/// Gets the master volume.
		/// </summary>
		/// <value>The master volume.</value>
        public float masterVolume { get { return _masterVolume; } }

        [Range(0f, 1f), SerializeField]
        private float _effectsVolume = 1f;
		/// <summary>
		/// Gets the effects volume.
		/// </summary>
		/// <value>The effects volume.</value>
        public float effectsVolume { get { return _effectsVolume; } }

        [Range(0f, 1f), SerializeField]
        private float _musicVolume = 1f;
		/// <summary>
		/// Gets the music volume.
		/// </summary>
		/// <value>The music volume.</value>
        public float musicVolume { get { return _musicVolume; } }

		/// <summary>
		/// The volume changed action. Invoked when volume changed.
		/// </summary>
        public Action VolumeChanged;

        void Awake ()
        {
            _masterVolume = 1f;
            _effectsVolume = 1f;
            _musicVolume = 1f;
        }

		/// <summary>
		/// Sets the volume for channel.
		/// </summary>
		/// <param name="channel">Channel.</param>
		/// <param name="volumePercent">Volume percent.</param>
        public void SetVolume (AudioChannel channel, float volumePercent)
        {
            var newVol = Mathf.Clamp(volumePercent, 0, 1f);

            switch (channel)
            {
                case AudioChannel.Master:
                    _masterVolume = newVol;
                    break;
                case AudioChannel.SFX:
                    _effectsVolume = newVol;
                    break;
                case AudioChannel.Music:
                    _musicVolume = newVol;
                    break;
            }

            if(VolumeChanged != null)
            {
                VolumeChanged();
            }
        }
    }
}