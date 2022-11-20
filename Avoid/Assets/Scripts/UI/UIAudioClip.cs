using UnityEngine;
using System.Collections;

namespace Avoidance
{
	/// <summary>
	/// Encapsulates playing audio clip on button press.
	/// </summary>
    public class UIAudioClip : MonoBehaviour
    {
		/// <summary>
		/// The clip to play on button press.
		/// </summary>
        public AudioClip clipToPlayOnButtonPress;

		/// <summary>
		/// Plays the button pressed audio clip.
		/// </summary>
        public void PlayButtonPressedAudioClip()
        {
            if(clipToPlayOnButtonPress != null)
            {
                MusicAudioPlayer.instance.PlayOneShot(clipToPlayOnButtonPress);
            }
        }
    }
}
