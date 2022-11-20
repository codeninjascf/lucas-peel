using UnityEngine;
using System;
using System.Collections;

namespace Avoidance
{
	/// <summary>
	/// Raises action when associated Gameobject is disabled.
	/// </summary>
	public class ActionOnDisabled : MonoBehaviour 
	{
		/// <summary>
		/// Action raised when object disabled.
		/// </summary>
		public Action onDisabled;

		void OnDisable()
		{
			if(!IsGameOver () && onDisabled != null)
			{
				onDisabled ();
			}	
		}

		private bool IsGameOver()
		{
			return GameStateController.instance == null || GameStateController.instance.isGameOver;
		}
	}
}
