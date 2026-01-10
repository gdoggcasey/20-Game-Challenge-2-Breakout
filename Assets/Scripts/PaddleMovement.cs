using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;

    [SerializeField] private Transform leftWall;
    [SerializeField] private Transform rightWall;


    private float halfPaddleWidth;
    private float screenHalfWidth;


    private void Start()
    {
        halfPaddleWidth = GetComponent<SpriteRenderer>().bounds.extents.x;
    }

    private void Update()
    {
        float input = Input.GetAxisRaw("Horizontal");

        Vector3 position = transform.position;
        position.x += input * moveSpeed * Time.deltaTime;

        float leftLimit = leftWall.position.x + halfPaddleWidth;
        float rightLimit = rightWall.position.x - halfPaddleWidth;

        position.x = Mathf.Clamp(position.x, leftLimit, rightLimit);

        transform.position = position;
    }
}
