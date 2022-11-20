using UnityEngine;
using System;
using System.Collections;

namespace Avoidance
{
	/// <summary>
	/// Scales an object over time.
	/// </summary>
	[RequireComponent(typeof(ScaleToScreenSize))]
	public class Scale : MonoBehaviour 
	{
		/// <summary>
		/// Scale type.
		/// </summary>
		public enum ScaleType
		{
			Up,
			Down
		}
		public ScaleType scaleType = ScaleType.Up;

		/// <summary>
		/// The scale lerp speed per second.
		/// </summary>
		public float scaleSpeed = 2f;

		/// <summary>
		/// Raised when scale complete.
		/// </summary>
		public Action onScaleComplete;

		private Scaler _scaler;
		private bool _completeActionRaised;
		private ScaleToScreenSize _scaleToScreenSize;

		void Awake()
		{
			_scaleToScreenSize = GetComponent<ScaleToScreenSize> ();
		}

		void OnEnable()
		{
			_completeActionRaised = false;

			Vector2 targetScale = _scaleToScreenSize.Simulate (false);
			
			var data = new ScaleData (transform, targetScale, scaleSpeed);

			_scaler = new ScalerFactory ().Make (scaleType, data);
		}

		void Update()
		{
			if(!_scaler.IsComplete ())
			{
				_scaler.Scale ();
			}
			else if(!_completeActionRaised)
			{
				if (onScaleComplete != null) 
				{
					onScaleComplete ();
				}

				_completeActionRaised = true;
			}
		}
	}

	/// <summary>
	/// Scaler factory. Responsible for creating Scaler class.
	/// </summary>
	class ScalerFactory
	{
		/// <summary>
		/// Makes a Scaler based on specified type and data.
		/// </summary>
		/// <param name="type">Type of Scaler.</param>
		/// <param name="data">Data.</param>
		public Scaler Make(Scale.ScaleType type, ScaleData data)
		{
			Scaler scaler = null;
			
			if(type == Scale.ScaleType.Down)
			{
				scaler = new ScaleDown (data);
			}
			else
			{
				scaler = new ScaleUp (data);
			}

			return scaler;
		}

	}

	/// <summary>
	/// Scaler contract.
	/// </summary>
	interface Scaler
	{
		void Scale();	
		bool IsComplete();
	}

	/// <summary>
	/// Scales transform up.
	/// </summary>
	class ScaleUp : Scaler
	{
		private ScaleData _scaleData;

		public ScaleUp(ScaleData data)
		{
			_scaleData = data;
		}

		public void Scale()
		{
			_scaleData.currentScale += _scaleData.speed * Time.deltaTime;
			_scaleData.owner.localScale = Vector3.Lerp (Vector3.zero, _scaleData.initialScale, _scaleData.currentScale);
		}

		public bool IsComplete()
		{
			return _scaleData.currentScale >= 1f;
		}
	}

	/// <summary>
	/// Scales tranform down.
	/// </summary>
	class ScaleDown : Scaler
	{
		private ScaleData _scaleData;

		public ScaleDown(ScaleData data)
		{
			_scaleData = data;
		}

		public void Scale()
		{
			_scaleData.currentScale += _scaleData.speed * Time.deltaTime;
			_scaleData.owner.localScale = Vector3.Lerp (_scaleData.initialScale, Vector3.zero, _scaleData.currentScale);
		}

		public bool IsComplete()
		{
			return _scaleData.currentScale >= 1f;
		}
	}

	/// <summary>
	/// Encapsulates scale data.
	/// </summary>
	struct ScaleData
	{
		/// <summary>
		/// The owner to scale.
		/// </summary>
		public Transform owner;
		public Vector3 initialScale;
		public float speed;
		public float currentScale;

		public ScaleData(Transform owner, Vector3 initialScale, float speed)
		{
			this.owner = owner;
			this.initialScale = initialScale;
			this.speed = speed;
			this.currentScale = 0f;
		}
	}
}
