using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private int damage = 1;

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Colisão detectada com: " + other.gameObject.name); // Teste de debug
        if (other.CompareTag("Asteroid"))
        {
            Asteroid asteroid = other.GetComponent<Asteroid>();
            if (asteroid != null)
            {
                Debug.LogWarning("nao é nulo");
                asteroid.TakeDamage(damage);
                Destroy(gameObject); // Destroi a bala após atingir o asteroide
            }
            else
            {
                Debug.LogWarning("Asteroide encontrado, mas sem script Asteroid.cs! Verifique se o script está anexado corretamente.");
            }
        }
    }
}
