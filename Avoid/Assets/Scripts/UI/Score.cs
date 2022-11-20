using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Avoidance
{
	/// <summary>
	/// Maintains current and high score.
	/// </summary>
	public class Score : Singleton<Score> 
	{
		/// <summary>
		/// In game UI displaying current score.
		/// </summary>
		public Text scoreText;

		private static readonly string HIGHSCORE_KEY = "highscore";

		private int _currentScore;

		/// <summary>
		/// Gets the current score.
		/// </summary>
		/// <value>The current score.</value>
		public int currentScore {get { return _currentScore; }}

		private int _highScore;

		/// <summary>
		/// Gets the high score.
		/// </summary>
		/// <value>The high score.</value>
		public int highScore {get {return _highScore; }}

		protected override void Awake()
		{
			base.Awake ();
			_currentScore = 0;
			_highScore = PlayerPrefs.GetInt (HIGHSCORE_KEY);
		}

		void Start()
		{
			UpdateScoreUI ();
		}

		/// <summary>
		/// Increments the score and updates UI.
		/// </summary>
		public void IncrementScore()
		{
			_currentScore++;

			UpdateScoreUI ();
		}

		/// <summary>
		/// Determines if current score is greater than highscore and updates highscore accordingly.
		/// </summary>
		public void CalculateHighScore()
		{
			if(_currentScore > _highScore)
			{
				_highScore = _currentScore;
				PlayerPrefs.SetInt (HIGHSCORE_KEY, _highScore);
			}
		}

		private void UpdateScoreUI()
		{
			scoreText.text = _currentScore.ToString ("d2");
		}

	}
}
