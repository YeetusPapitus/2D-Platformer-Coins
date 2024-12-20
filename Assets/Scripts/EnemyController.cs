using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Transform[] patrolPoints;
    private int patrolPointIndex = 0;
    private Transform currentPatrolPoint;
    private Rigidbody2D rb;
    [SerializeField] private float moveSpeed = 0.5f;
    private int direction;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentPatrolPoint = patrolPoints[patrolPointIndex];
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDirection();
        MoveTowardsPatrolPoint();
    }

    private void UpdateDirection()
    {
        if(currentPatrolPoint.position.x > transform.position.x)
        {
            direction = 1;
        }
        else
        {
            direction = -1;
        }

        spriteRenderer.flipX = direction > 0;
    }

    private void MoveTowardsPatrolPoint()
    {

        rb.velocity = new Vector2(direction * moveSpeed, rb.velocity.y);

        if(Vector3.Distance(transform.position, currentPatrolPoint.position) < 0.1f)
        {
            UpdatePatrolPoint();
        }
    }

    private void UpdatePatrolPoint()
    {
        patrolPointIndex++;

        if(patrolPointIndex >= patrolPoints.Length)
            patrolPointIndex = 0;

        currentPatrolPoint = patrolPoints[patrolPointIndex];

    }
}
