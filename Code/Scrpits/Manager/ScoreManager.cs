using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Manager
{
    public class ScoreManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI EnemyScoreText, PlayerScoreText;
        
        private int enemyScore, playerScore;
        
        public enum Score
        {
            EnemyScore,
            PlayerScore
        }
        
        public void Increment(Score whichScore)
        {
            if (whichScore == Score.EnemyScore)
            {
                EnemyScoreText.text = (++enemyScore).ToString();
            }
            else
            {
                PlayerScoreText.text = (++playerScore).ToString();
            }
        }

        public void Reset()
        {
            PlayerScoreText.text = (playerScore = 0).ToString();
            EnemyScoreText.text = (enemyScore = 0).ToString();
        }
    }
}