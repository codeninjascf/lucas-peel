using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;


namespace Avoidance
{
	/// <summary>
	/// Responsible for updating game state.
	/// </summary>
	public class GameStateController : Singleton<GameStateController> 
	{
		[Header("Main Menu")]
		/// <summary>
		/// The main menu UI container.
		/// </summary>
		public GameObject playUI;

		/// <summary>
		/// The highscore text on the main menu.
		/// </summary>
		public Text mainMenuHighScoreText;

		/// <summary>
		/// The delay before game starts.
		/// </summary>
        public float delayBeforeGameStarts = 1f;
		
		[Header("In Game")]
		/// <summary>
		/// The in game UI container.
		/// </summary>
		public GameObject inGameUI;

		[Header("Pause")]
		/// <summary>
		/// The pause menu UI container.
		/// </summary>
		public GameObject pauseMenuUI;

		/// <summary>
		/// The score text shown on pause screen.
		/// </summary>
		public Text pauseScoreText;

		/// <summary>
		/// The highscore text shown on pause screen.
		/// </summary>
		public Text pauseHighscoreText;

		[Header("Game Over")]
		/// <summary>
		/// The delay between player killed and game over UI shown.
		/// </summary>
		public float gameOverDelay = 0.5f;

		/// <summary>
		/// The game over UI container.
		/// </summary>
		public GameObject gameOverUI;

		/// <summary>
		/// The score text shown on game over screen.
		/// </summary>
		public Text gameOverScoreText;

		/// <summary>
		/// The highscore text shown on game over screen.
		/// </summary>
		public Text gameOverHighscoreText;

        [Header("Audio")]
		/// <summary>
		/// Object responsible for playing BGM.
		/// </summary>
        public BGMManager bgmManager;

		/// <summary>
		/// Gets a value indicating whether this <see cref="Avoidance.GameStateController"/> has reached game over state.
		/// </summary>
		/// <value><c>true</c> if is game over; otherwise, <c>false</c>.</value>
		public bool isGameOver { get; private set;}

		private Startable[] _startables;
        private Startable _playerInput;
		private GameObject _straightToPlay;
		private string _straightToPlayTag = "StraightToGame";

		protected override void Awake ()
		{
			base.Awake ();

            _startables = GameObject.FindObjectsOfType<Startable>();

            foreach(var s in _startables)
            {
                if(s is MoveToTouchPosition)
                {
                    _playerInput = s;
                }
            }

            _straightToPlay = GameObject.FindGameObjectWithTag (_straightToPlayTag);
		}

      
		void Start()
		{
			if (_straightToPlay != null) 
			{
                GoStraightIntoGame();
            } 
			else 
			{
                OnMainMenu();
			}
		}

		/// <summary>
		/// Hides main menu and starts game.
		/// </summary>
		public void OnPlay()
		{
			bgmManager.PlayGameAudio();
			HidePlayMenu ();
			ShowInGameUI ();
			StartGame ();
		}

		/// <summary>
		/// Plays game over audio. Shows game over menu.
		/// </summary>
		public void OnGameOver()
		{
			isGameOver = true;

			bgmManager.PlayGameOverAudio();

			StartCoroutine (DoGameOver ());
		}

		/// <summary>
		/// Reloads scene.
		/// </summary>
		public void OnRestart()
		{
			if (_straightToPlay == null)
			{
				_straightToPlay = new GameObject("Straight To Play");
				_straightToPlay.AddComponent<PersistentObject>();
				_straightToPlay.tag = _straightToPlayTag;
			}

			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}

		/// <summary>
		/// Stops time and shows pause menu.
		/// </summary>
		public void OnPause()
		{
			StopTime ();
			HideInGameUI ();
			SetPauseText ();
			ShowPauseMenu ();
		}

		/// <summary>
		/// Resumes time and hides pause menu.
		/// </summary>
		public void OnResume()
		{
			ShowInGameUI ();
			ResumeTime ();
			HidePauseMenu ();
		}

        private void OnMainMenu()
        {
            bgmManager.PlayUIAudio();
            mainMenuHighScoreText.text = Score.instance.highScore.ToString("d2");
            HideGameOverUI();
            HidePauseMenu();
            HideInGameUI();
            ShowPlayMenu();
        }

        private void GoStraightIntoGame()
        {
            ResumeTime();
            OnPlay();
        }

		private IEnumerator DoGameOver()
		{
			yield return new WaitForSeconds (gameOverDelay);

			HideInGameUI ();

			StopTime ();

			Score.instance.CalculateHighScore ();

			SetGameOverText ();
			ShowGameOverUI ();
		}

		private void StartGame()
		{
            StartCoroutine(DoStartAfterDelay());
		}

        private IEnumerator DoStartAfterDelay()
        {
            _playerInput.OnStart();

            yield return new WaitForSeconds(delayBeforeGameStarts);

            foreach (var s in _startables)
            {
                s.OnStart();
            }
        }

		private void StopTime()
		{
			Time.timeScale = 0f;
		}

		private void ResumeTime()
		{
			Time.timeScale = 1f;
		}

		private void SetGameOverText()
		{
			gameOverScoreText.text = Score.instance.currentScore.ToString ("d2");
			gameOverHighscoreText.text = Score.instance.highScore.ToString ("d2");
		}

		private void SetPauseText()
		{
			pauseScoreText.text = Score.instance.currentScore.ToString ("d2");
			pauseHighscoreText.text = Score.instance.highScore.ToString ("d2");
		}

		private void ShowGameOverUI()
		{
			gameOverUI.SetActive (true);
		}

		private void HideGameOverUI()
		{
			gameOverUI.SetActive (false);
		}

		private void HideInGameUI()
		{
			inGameUI.SetActive (false);
		}

		private void ShowInGameUI()
		{
			inGameUI.SetActive (true);
		}

		private void HidePauseMenu()
		{
			pauseMenuUI.SetActive (false);
		}

		private void ShowPauseMenu()
		{
			pauseMenuUI.SetActive (true);
		}

		private void HidePlayMenu()
		{
			playUI.SetActive (false);
		}

		private void ShowPlayMenu()
		{
			playUI.SetActive (true);
		}

	}
}