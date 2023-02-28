using Scrpits.Puck;
using Scrpits.Utilities;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Scrpits.Enemy
{
    public class AIScript : MonoBehaviour
    {
        //[SerializeField] private PuckScript puck;
        [SerializeField] private float maxMovementSpeed;
        [SerializeField] private Rigidbody2D puckRb;
        [SerializeField] private Transform playerBoundaryHolder;
        [SerializeField] private Transform puckBoundaryHolder;
        
        private Rigidbody2D rb;
        private Vector2 startPosition;
        private Vector2 targetPosition;
        private Boundary playerBoundary;
        private Boundary puckBoundary;
        private bool isFirstTimeInOpponentsHalf = true;
        private float offsetXFromTarget;
        
        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            startPosition = rb.position;
        
            playerBoundary = new Boundary(playerBoundaryHolder.GetChild(0).position.y,
                playerBoundaryHolder.GetChild(1).position.y,
                playerBoundaryHolder.GetChild(2).position.x,
                playerBoundaryHolder.GetChild(3).position.x);
        
            puckBoundary = new Boundary(puckBoundaryHolder.GetChild(0).position.y,
                puckBoundaryHolder.GetChild(1).position.y,
                puckBoundaryHolder.GetChild(2).position.x,
                puckBoundaryHolder.GetChild(3).position.x);

            switch (GameValue.Difficulty)
            {
                case GameValue.Difficulties.Easy:
                    maxMovementSpeed = 15;
                    break;
                case GameValue.Difficulties.Medium:
                    maxMovementSpeed = 25;
                    break;
                case GameValue.Difficulties.Hard:
                    maxMovementSpeed = 40;
                    break;
            }
        }

        private void FixedUpdate()
        {
            if (!PuckScript.WasGoal)
            {
                float movementSpeed;

                if (puckRb.position.y < puckBoundary.Down)
                {
                    if (isFirstTimeInOpponentsHalf)
                    {
                        isFirstTimeInOpponentsHalf = false;
                        offsetXFromTarget = Random.Range(-2.5f, 2.5f);
                    }
            
                    movementSpeed = maxMovementSpeed * Random.Range(0.1f, 0.5f);
                    targetPosition = new Vector2(Mathf.Clamp(puckRb.position.x + offsetXFromTarget, playerBoundary.Left, playerBoundary.Right),
                        startPosition.y);
                }
                else
                {
                    isFirstTimeInOpponentsHalf = true;
                
                    movementSpeed = Random.Range(maxMovementSpeed * 0.4f, maxMovementSpeed);
                    targetPosition = new Vector2(Mathf.Clamp(puckRb.position.x, playerBoundary.Left, playerBoundary.Right),
                        Mathf.Clamp(puckRb.position.y, playerBoundary.Down, playerBoundary.Up));
            
                }
        
                rb.MovePosition(Vector2.MoveTowards(rb.position, targetPosition, movementSpeed * Time.fixedDeltaTime));
            }
            else
            {
                transform.position = new Vector2(0,7);
            }
        }
    }
}
