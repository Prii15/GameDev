using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField] private float speed = 5f; // Velocidade do míssil
    

    void Update()
    {
        // Move o míssil para cima (direção Y)
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            invader invader = collision.GetComponent<invader>();
            invader.Die();
            Destroy(gameObject); // Destroi o míssil
        }
        else if (collision.CompareTag("Wall"))
        {
            Destroy(gameObject); // Destroi o míssil ao atingir a parede
        }
    }
}
