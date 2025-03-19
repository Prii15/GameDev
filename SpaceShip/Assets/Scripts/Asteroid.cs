using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private int health = 1;
    [SerializeField] private int scoreValue = 10;

    void Update()
    {
        if (transform.position.x < -7f) // Se o asteroide sair da tela pela esquerda
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            GameManager.AddScore(scoreValue);
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.PlayerHit();
            Destroy(gameObject);
        }
    }
}
