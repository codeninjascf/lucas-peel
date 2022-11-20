using UnityEngine;
using System.Collections;

namespace Avoidance
{
	/// <summary>
	/// Increments current score on object collision.
	/// </summary>
    public class IncrementScoreInteractable : MonoBehaviour, Interactable
    {
        public void Interact(GameObject interacted)
        {
            Score.instance.IncrementScore();
        }
    }
}
