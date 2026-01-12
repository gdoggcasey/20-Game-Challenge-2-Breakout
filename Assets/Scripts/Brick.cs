using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] private Color brickColor;
    [SerializeField] private GameObject brickHitParticlesPrefab;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetColor(Color color)
    {
        brickColor = color;
        spriteRenderer.color = new Color(color.r, color.g, color.b, 1f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            BreakBrick();
        }
    }

    private void BreakBrick()
    {
        // Play sound
        AudioManager.Instance.PlaySound(AudioManager.Instance.brickHit);

        // Update game state
        GameManager.Instance.AddScore();
        GameManager.Instance.BrickDestroyed();

        // Destroy brick
        Destroy(gameObject);
    }
}
