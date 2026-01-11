using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    [SerializeField] private float launchSpeed = 4f;
    [SerializeField] private Transform paddle;

    [SerializeField] private float bounceAngleFactor = 2f;
    [SerializeField] private float maxBounceAngle = 60f;

    private Rigidbody2D rb;
    private bool hasLaunched = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        ResetBall();
    }

    private void Update()
    {
        if (!hasLaunched && Input.GetKeyDown(KeyCode.Space))
        {
            LaunchBall();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Paddle"))
        {
            HandlePaddleBounce(collision);
        }
    }

    private void HandlePaddleBounce(Collision2D collision)
    {
        Transform paddle = collision.transform;

        float paddleWidth = paddle.GetComponent<BoxCollider2D>().bounds.size.x;

        float hitPoint = transform.position.x - paddle.position.x;
        float normalizedHitPoint = hitPoint / (paddleWidth / 2f);

        float bounceAngle = normalizedHitPoint * maxBounceAngle;
        float bounceAngleRad = bounceAngle * Mathf.Deg2Rad;

        Vector2 direction = new Vector2(
            Mathf.Sin(bounceAngleRad),
            Mathf.Cos(bounceAngleRad)
        );

        rb.velocity = direction.normalized * rb.velocity.magnitude;
    }

    private void LaunchBall()
    {
        hasLaunched = true;

        Vector2 launchDirection = new Vector2(
            Random.Range(-0.5f, 0.5f),
            1f
        ).normalized;

        rb.velocity = launchDirection * launchSpeed;
    }

    private void ResetBall()
    {
        hasLaunched = false;
        rb.velocity = Vector2.zero;

        transform.position = paddle.position + Vector3.up * 0.5f;
    }
}
