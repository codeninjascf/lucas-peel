using UnityEngine;
using System;
using System.Collections;

namespace Avoidance
{
	public delegate bool CheckDelegate();

	/// <summary>
	/// Encapsulates a boolean check.
	/// </summary>
	public class CheckAction
	{
		private bool _boundsCheckPassed = false;

		private CheckDelegate[] _boundsCheckToPerform;

		/// <summary>
		/// Initializes a new instance of the <see cref="Avoidance.CheckAction"/> class.
		/// </summary>
		/// <param name="checksToPerform">Checks to be encapsulated within this object.</param>
		public CheckAction(CheckDelegate[] checksToPerform)
		{
			_boundsCheckToPerform = checksToPerform;
		}

		/// <summary>
		/// Reset this instance.
		/// </summary>
		public void Reset()
		{
			_boundsCheckPassed = false;
		}

		/// <summary>
		/// Determines whether this instance has passed all specified checks.
		/// </summary>
		/// <returns><c>true</c> if this instance has passed check; otherwise, <c>false</c>.</returns>
		public bool HasPassedCheck()
		{
			if(_boundsCheckPassed)
			{
				return true;
			}

			_boundsCheckPassed = AllChecksPassed ();

			return _boundsCheckPassed;
		}

		private bool AllChecksPassed()
		{
			for(int i = 0; i < _boundsCheckToPerform.Length; i++)
			{
				if(!_boundsCheckToPerform[i]())
				{
					return false;
				}
			}

			return true;
		}
	}
}