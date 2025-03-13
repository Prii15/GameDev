using UnityEngine;

public class invaderMissile : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    void Update()
    {
        // Move o míssil para cima (direção Y)
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.LoseLife();
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Wall"))
        {
            Destroy(gameObject); // Destroi o míssil ao atingir a parede
        }
    }
}
