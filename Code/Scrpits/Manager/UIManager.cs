using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Scrpits.Puck;


namespace Manager
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private RectTransform startPanel;
        [SerializeField] private Button startButton;
        [SerializeField] private RectTransform endPanel;
        [SerializeField] private Button[] restartButton;
        [SerializeField] private TextMeshProUGUI winText;
        [SerializeField] private TextMeshProUGUI loseText;
        [SerializeField] private Button resetPuckButton;
        [SerializeField] private Button pauseButton;
        [SerializeField] private Button resumeButton;
        [SerializeField] private RectTransform pausePanel;
        [SerializeField] private Button[] menuButton;

        private GameManager gameManager;
        private ScoreManager scoreManager;
        private PuckScript puck;

        public event Action OnStarted;
        public event Action OnRestarted;

        private void Awake()
        {
            gameManager = this.gameObject.GetComponent<GameManager>();
            scoreManager = this.gameObject.GetComponent<ScoreManager>();
            
            startButton.onClick.AddListener(StartGame);
            
            restartButton[0].onClick.AddListener(OnRestart);
            
            restartButton[1].onClick.AddListener(OnRestart);

            resetPuckButton.onClick.AddListener(gameManager.ResetPuck);
            
            pauseButton.onClick.AddListener(OpenPausePanel);
            pauseButton.onClick.AddListener(gameManager.PauseGame);
            
            resumeButton.onClick.AddListener(ClosePausePanel);
            resumeButton.onClick.AddListener(gameManager.ResumeGame);
            
            menuButton[0].onClick.AddListener(gameManager.LoadMainMenu);
            menuButton[1].onClick.AddListener(gameManager.LoadMainMenu);
        }

        private void StartGame()
        {
            startPanel.gameObject.SetActive(false);
            resetPuckButton.gameObject.SetActive(true);
            pauseButton.gameObject.SetActive(true);
            OnStarted?.Invoke();
        }

        private void OnRestart()
        {
            endPanel.gameObject.SetActive(false);
            pausePanel.gameObject.SetActive(false);
            scoreManager.Reset();
            OnRestarted?.Invoke();
        }

        private void OpenPausePanel()
        {
            pausePanel.gameObject.SetActive(true);
        }

        private void ClosePausePanel()
        {
            pausePanel.gameObject.SetActive(false);
        }

        public void PlayerWinUI()
        {
            endPanel.gameObject.SetActive(true);
            winText.gameObject.SetActive(true);
            loseText.gameObject.SetActive(false);            
        }

        public void EnemyWinUI()
        {
            endPanel.gameObject.SetActive(true);
            winText.gameObject.SetActive(false);
            loseText.gameObject.SetActive(true);
        }
    }
}
