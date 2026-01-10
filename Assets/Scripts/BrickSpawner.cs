using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickSpawner : MonoBehaviour
{
    [Header("Brick Setup")]
    [SerializeField] private GameObject brickPrefab;

    [Header("Grid Settings")]
    [SerializeField] private int rows = 6;
    [SerializeField] private int columns = 10;
    [SerializeField] private float brickWidth = 1f;
    [SerializeField] private float brickHeight = 0.5f;
    [SerializeField] private float spacing = 0.1f;

    [Header("Positioning")]
    [SerializeField] private Vector2 startPosition = new Vector2(-4.5f, 3.5f);

    private void Start()
    {
        SpawnBricks();
    }

    private void SpawnBricks()
    {
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                Vector2 spawnPosition = new Vector2(
                    startPosition.x + col * (brickWidth + spacing),
                    startPosition.y - row * (brickHeight + spacing)
                );

                Instantiate(brickPrefab, spawnPosition, Quaternion.identity, transform);
            }
        }
    }
}
