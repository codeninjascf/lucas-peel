using UnityEngine;

namespace Avoidance
{
	/// <summary>
	/// Plays audio clip on object hit.
	/// </summary>
    public class PlayClipInteractable : MonoBehaviour, Interactable
    {
		/// <summary>
		/// The clip to play.
		/// </summary>
        public AudioClip clipToPlay;

		/// <summary>
		/// Plays the specified clip.
		/// </summary>
		/// <param name="interacted">Object that invoked method.</param>
        public void Interact(GameObject interacted)
        {
            if(clipToPlay != null)
            {
                MusicAudioPlayer.instance.PlayOneShot(clipToPlay);
            }
        }
    }
}