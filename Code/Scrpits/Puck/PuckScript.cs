using System;
using System.Collections;
using Manager;
using UnityEngine;

namespace Scrpits.Puck
{
    public class PuckScript : MonoBehaviour
    {
        [SerializeField] private ScoreManager scoreInstance;
        [SerializeField] private float maxSpeed;
        [SerializeField] private AudioManager audioManager;
        [SerializeField] private int maxScoreToWin = 3;
        public static bool WasGoal { get; private set; }
        private Rigidbody2D rb;
        [SerializeField]private int playerGoalCount;
        [SerializeField]private int enemyGoalCount;

        public event Action PlayerWining;
        public event Action EnemyWining;
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            WasGoal = false;
            playerGoalCount = 0;
            enemyGoalCount = 0;
        }

        private void FixedUpdate()
        {
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!WasGoal)
            {
                if (other.CompareTag("EnemyGoal"))
                {
                    scoreInstance.Increment(ScoreManager.Score.PlayerScore);
                    WasGoal = true;
                    audioManager.PlayGoal();
                    playerGoalCount++;
                    StartCoroutine(ResetPuck(false));
                    
                    if (playerGoalCount == maxScoreToWin)
                    {
                        PlayerWining?.Invoke();
                        playerGoalCount = 0;
                        enemyGoalCount = 0;
                    }
                }
                else if (other.CompareTag("PlayerGoal"))
                {
                    scoreInstance.Increment(ScoreManager.Score.EnemyScore);
                    WasGoal = true;
                    audioManager.PlayGoal();
                    enemyGoalCount++;
                    StartCoroutine(ResetPuck(true));

                    if (enemyGoalCount == maxScoreToWin)
                    {
                        EnemyWining.Invoke();
                        playerGoalCount = 0;
                        enemyGoalCount = 0;
                    }
                }
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            audioManager.PlayPuck();
        }

        private IEnumerator ResetPuck(bool didEnemyScore)
        {
            yield return new WaitForSecondsRealtime(1);
            WasGoal = false;
            rb.velocity = rb.position = new Vector2(0, 0);

            if (didEnemyScore)
            {
                rb.position = new Vector2(0, -1.5f);
            }
            else
            {
                rb.position = new Vector2(0, 1.5f);
            }
        }
    }
}