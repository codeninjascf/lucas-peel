using UnityEngine;
using System.Collections;

namespace Avoidance
{
	/// <summary>
	/// Plays the specified clip when the gameobject enters camera bounds.
	/// </summary>
    [RequireComponent(typeof(BoundsCheck))]
    public class PlayClipWhenEnterBounds : MonoBehaviour
    {
		/// <summary>
		/// The clip to play on enter bounds.
		/// </summary>
        public AudioClip clipOnEnterBounds;

        private BoundsCheck _boundsCheck;

        void Awake()
        {
            _boundsCheck = GetComponent<BoundsCheck>();
        }

        void OnEnable()
        {
            _boundsCheck.onEnterBounds += PlayClip;
        }

        void OnDisable()
        {
            _boundsCheck.onEnterBounds -= PlayClip;
        }

        private void PlayClip()
        {
            if (clipOnEnterBounds != null)
            {
                MusicAudioPlayer.instance.PlayOneShot(clipOnEnterBounds);
            }
        }
    }
}
