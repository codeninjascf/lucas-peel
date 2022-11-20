using UnityEngine;
using System.Collections;

namespace Avoidance
{
	/// <summary>
	/// Contract for any class that will be started at gameplay start. This method is called on all objects when the play button is pressed.
	/// </summary>
	public abstract class Startable : MonoBehaviour
	{
		public abstract void OnStart ();
	}
}
