using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scrpits.Enemy;
using Scrpits.Player;
using Scrpits.Puck;
using UnityEngine.SceneManagement;

namespace Manager
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] PlayerMovement playerMovement;
        [SerializeField] AIScript enemyScript;
        [SerializeField] PuckScript puckScript;

        private ScoreManager scoreManager;
        private UIManager uiManager;

        private void Start()
        {
            scoreManager = GetComponent<ScoreManager>();
            uiManager = GetComponent<UIManager>();

            uiManager.OnStarted += OnStart;
            puckScript.PlayerWining += PlayerWin;
            puckScript.EnemyWining += EnemyWin;
            uiManager.OnRestarted += OnRestart;

            playerMovement.enabled = false;
            enemyScript.enabled = false;
        }

        private void OnStart()
        {
            playerMovement.enabled = true;
            enemyScript.enabled = true;
        }

        private void OnRestart()
        {
            if (Time.timeScale < 1)
            {
                Time.timeScale = 1;
            }
            
            playerMovement.gameObject.transform.position = new Vector2(0, -7);
            enemyScript.gameObject.transform.position = new Vector2(0, 7);
            puckScript.gameObject.transform.position = new Vector2(0, 0);

            playerMovement.enabled = true;
            enemyScript.enabled = true;
        }

        private void PlayerWin()
        {
            uiManager.PlayerWinUI();
            playerMovement.enabled = false;
            enemyScript.enabled = false;
        }

        private void EnemyWin()
        {
            uiManager.EnemyWinUI();
            playerMovement.enabled = false;
            enemyScript.enabled = false;
        }

        public void ResetPuck()
        {
            puckScript.gameObject.transform.position = new Vector2(0, 0);
        }

        public void PauseGame()
        {
            Time.timeScale = 0;
        }

        public void ResumeGame()
        {
            Time.timeScale = 1;
        }

        public void LoadMainMenu()
        {
            if (Time.timeScale < 1)
            {
                Time.timeScale = 1;
            }
            
            SceneManager.LoadScene("Menu");
        }
    }
}
