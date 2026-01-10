using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    [SerializeField] private float launchSpeed = 4f;
    [SerializeField] private Transform paddle;

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
