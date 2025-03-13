using UnityEngine;

public class bigInvader : MonoBehaviour
{
    // private Rigidbody2D rb2d;
    [SerializeField] private float speed = 2f;
    
    [Header("Score Settings")]
    [SerializeField] private int scoreValue = 10;
    [SerializeField] private Sprite explosionSprite;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // rb2d.linearVelocity = new Vector2(speed, 0);
    }

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime); // Movimento suave sem Rigidbody2D
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall")) // Se tocar na parede direita
        {
            Destroy(gameObject); // Remove a nave
        }
    }

    public void Die()
    {
        spriteRenderer.sprite = explosionSprite;
        GameManager.AddScore(scoreValue);

        Destroy(gameObject, 0.5f);
    }
}
