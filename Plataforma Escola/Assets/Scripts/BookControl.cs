using UnityEngine;

public class BookControl : MonoBehaviour
{
    private Rigidbody2D rb2d;
    [SerializeField] private float speed = 10f;
    [SerializeField] private int damage = 1;

    void Start(){
        rb2d = GetComponent<Rigidbody2D>();     // Inicializa o rigidbody
    }

    void Update(){

        rb2d = GetComponent<Rigidbody2D>();  

        // Direção baseada no Player
        float direction = PlayerControl.facingRight ? 1 : -1;

        // Define a velocidade inicial
        rb2d.linearVelocity = new Vector2(speed * direction, rb2d.linearVelocity.y);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Floor"))
        {
            Destroy(gameObject); // Destroi o míssil ao atingir a parede
        }
        else if (collision.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }

}
