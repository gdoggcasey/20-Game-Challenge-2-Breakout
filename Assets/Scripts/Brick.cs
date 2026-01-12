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
        // Spawn particles
        if (brickHitParticlesPrefab != null)
        {
            GameObject particles = Instantiate(brickHitParticlesPrefab, transform.position, Quaternion.identity);
            Debug.Log("Spawning particles");

            // Change particle color to match brick
            var main = particles.GetComponent<ParticleSystem>().main;
            main.startColor = brickColor;

            Destroy(particles, 1f); // clean up after 1 second
        }

        // Play sound
        AudioManager.Instance.PlaySound(AudioManager.Instance.brickHit);

        // Update game state
        GameManager.Instance.AddScore();
        GameManager.Instance.BrickDestroyed();

        // Destroy brick
        Destroy(gameObject);
    }
}
