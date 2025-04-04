using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length;
    public float parallaxEffect;
    private float currentParallaxSpeed;  // Velocidade atual do parallax

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        currentParallaxSpeed = parallaxEffect;  // Inicializa com a velocidade normal
        Debug.Log($"Parallax {gameObject.name} iniciado com velocidade: {currentParallaxSpeed}");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * Time.deltaTime * parallaxEffect;

        if (transform.position.x < -length)
        {
            if (gameObject.CompareTag("Background")) 
            {
                transform.position = new Vector3(length, transform.position.y, transform.position.z);
            }
            else if (gameObject.CompareTag("Asteroid"))
            {
                if (transform.position.x < -7){
                    Destroy(gameObject); // Destroi asteroides
                }
                
            }
        }

    }

    // Reduz a velocidade do parallax pela metade
    public void SlowParallax()
    {
        currentParallaxSpeed = parallaxEffect / 4;  // Reduz pela metade
        Debug.Log($"{gameObject.name} velocidade atual do parallax: {currentParallaxSpeed}");
    }

    // Restaura a velocidade normal
    public void RestoreParallaxSpeed()
    {
        currentParallaxSpeed = parallaxEffect;  // Restaura a velocidade normal
        Debug.Log($"Parallax {gameObject.name} velocidade restaurada para: {currentParallaxSpeed}");
    }
}
