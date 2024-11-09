using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] LineDirection patrolLineDirection = LineDirection.Horizontal;
    [SerializeField] float moveSpeed = 3f;

    Rigidbody2D rb;
    SpriteRenderer sr;
    Direction currentDirection;

    enum LineDirection
    {
        Horizontal, Vertical
    }

    enum Direction {
        Top, Bottom, Right, Left
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        if (patrolLineDirection.Equals(LineDirection.Horizontal))
        {
            rb.velocity = new Vector3(moveSpeed, 0f, 0f);
            currentDirection = Direction.Right;
        }
        else
        {
            rb.velocity = new Vector3(0f, moveSpeed, 0f);
            currentDirection = Direction.Top;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Foreground") && !collision.gameObject.CompareTag("Hazard"))
        {
            return;
        }

        if (patrolLineDirection.Equals(LineDirection.Horizontal))
        {
            if (currentDirection.Equals(Direction.Right))
            {
                sr.flipX = true;
                rb.velocity = new Vector3(-moveSpeed, 0f, 0f);
                currentDirection = Direction.Left;
            }
            else
            {
                sr.flipX = false;
                rb.velocity = new Vector3(moveSpeed, 0f, 0f);
                currentDirection = Direction.Right;
            }
        }
        else
        {
            if (currentDirection.Equals(Direction.Top))
            {
                rb.velocity = new Vector3(0f, -moveSpeed, 0f);
                currentDirection = Direction.Bottom;
            }
            else
            {
                rb.velocity = new Vector3(0f, moveSpeed, 0f);
                currentDirection = Direction.Top;
            }
        }
    }
}
