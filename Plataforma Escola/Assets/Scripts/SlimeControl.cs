using UnityEngine;
using System.Collections;

public class SlimeControl : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private SpriteRenderer spriteRenderer;

    [Header("Movimentação")]
    [SerializeField] private float speed = 2f; // Velocidade do inimigo
    [SerializeField] private float leftLimit = -5f; // Limite esquerdo
    [SerializeField] private float rightLimit = 5f; // Limite direito
    private bool movingRight = true; // Direção atual do inimigo

    [Header("Dano")]
    [SerializeField] private float points = 50;
    [SerializeField] private float life = 5;
    [SerializeField] private float invulnerableTime = 0.5f; // Tempo de invulnerabilidade após tomar dano
    [SerializeField] private float blinkInterval = 0.2f; // Tempo entre cada piscada

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Movimenta o slime para a direita ou esquerda
        rb2d.linearVelocity = new Vector2(speed * (movingRight ? 1 : -1), rb2d.linearVelocity.y);

        // Se atingir os limites, vira para o outro lado
        if (transform.position.x >= rightLimit && movingRight)
        {
            Flip();
        }
        else if (transform.position.x <= leftLimit && !movingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        movingRight = !movingRight; // Alterna a direção
    }

    void TakeDamage(){
        if (life > 0)
        {
            life--;

            if (life <= 0)
            {
                Destroy(gameObject);
            }

            StartCoroutine(BlinkEffect());
        }
    }

    private IEnumerator BlinkEffect()
    {
        float timer = 0f;
        bool isVisible = true;

        while (timer < invulnerableTime)
        {
            isVisible = !isVisible;
            spriteRenderer.enabled = isVisible; // Alterna entre visível e invisível
            yield return new WaitForSeconds(blinkInterval);
            timer += blinkInterval;
        }

        spriteRenderer.enabled = true; // Garante que fica visível no final
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Shoot"))
        {
            TakeDamage();
        }
    }

}
