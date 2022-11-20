using UnityEngine;

namespace Avoidance
{
    /// <summary>
    /// Responsible for playing background audio music for main menu, game scene, and game over scene.
    /// </summary>
	public class BGMManager : MonoBehaviour
	{
        /// <summary>
        /// Clip to play during game scene.
        /// </summary>
		public AudioClip audioGameClip;

        /// <summary>
        /// Clip to play during the main menu.
        /// </summary>
		public AudioClip audioUIClip;

        /// <summary>
        /// Clip to play during the game over scene.
        /// </summary>
        public AudioClip gameOverClip;

        /// <summary>
        /// Time to fade between clips.
        /// </summary>
        public float fadeTime = 1f;

		/// <summary>
		/// Plays the user interface audio.
		/// </summary>
        public void PlayUIAudio()
        {
            if (audioUIClip != null)
            {
                MusicAudioPlayer.instance.Play(audioUIClip, fadeTime);
            }
        }

		/// <summary>
		/// Plays the game audio.
		/// </summary>
        public void PlayGameAudio()
        {
            if (audioGameClip != null)
            {
                MusicAudioPlayer.instance.Play(audioGameClip, fadeTime);
            }
        }

		/// <summary>
		/// Plays the game over audio.
		/// </summary>
        public void PlayGameOverAudio()
        {
            if(gameOverClip != null)
            {
                MusicAudioPlayer.instance.Play(gameOverClip, fadeTime);
            }
        }
	}
}