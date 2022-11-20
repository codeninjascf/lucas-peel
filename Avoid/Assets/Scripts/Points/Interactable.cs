using UnityEngine;
using System.Collections;

namespace Avoidance
{
	/// <summary>
	/// Interactable interface. All interactables are retrieved when collidiong with player.
	/// </summary>
	public interface Interactable  
	{
		void Interact (GameObject interacted);
	}
}
