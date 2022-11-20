using UnityEngine;
using System.Collections;

namespace Avoidance
{
	/// <summary>
	/// Invokes all Interactable on colliding objects.
	/// </summary>
	public class Interact : MonoBehaviour 
	{
		void OnTriggerEnter2D(Collider2D other)
		{
			Interactable[] interactions = other.GetComponents<Interactable> ();
			
			if(interactions != null && interactions.Length > 0)
			{
				foreach(var i in interactions)
				{
					i.Interact (gameObject);
				}
			}
		}
	}
}
