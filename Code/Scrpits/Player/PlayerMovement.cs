using Scrpits.Puck;
using Scrpits.Utilities;
using UnityEngine;

namespace Scrpits.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Transform boundaryHolder;

        private bool isClicked;
        private bool canMove;
        private Rigidbody2D rb;
        private Boundary playerBoundary;
        private Collider2D playerCollider;


        void Start()
        {
            playerCollider = GetComponent<Collider2D>();

            rb = gameObject.GetComponent<Rigidbody2D>();

            playerBoundary = new Boundary(boundaryHolder.GetChild(0).position.y,
                boundaryHolder.GetChild(1).position.y,
                boundaryHolder.GetChild(2).position.x,
                boundaryHolder.GetChild(3).position.x);
        }

        void FixedUpdate()
        {
            if (Input.GetMouseButton(0))
            {
                if (!(Camera.main is null))
                {
                    Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                    if (isClicked)
                    {
                        isClicked = false;

                        if (playerCollider.OverlapPoint(mousePos))
                        {
                            canMove = true;
                        }
                        else
                        {
                            canMove = false;
                        }
                    }

                    if (canMove)
                    {
                        Vector2 clampedMousePos = new Vector2(
                            Mathf.Clamp(mousePos.x, playerBoundary.Left, playerBoundary.Right),
                            Mathf.Clamp(mousePos.y, playerBoundary.Down, playerBoundary.Up));

                        rb.MovePosition(clampedMousePos);
                    }
                }
            }
            else
            {
                isClicked = true;
            }

            if (PuckScript.WasGoal)
            {
                transform.position = new Vector2(0, -7);
            }
        }
    }
}
