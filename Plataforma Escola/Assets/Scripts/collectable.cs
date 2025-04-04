using UnityEngine;

public class collectable : MonoBehaviour
{
    public string collectableName; // Nome do coletável
    public Sprite collectableSprite; // Ícone do coletável

    public float floatSpeed = 1f; // Velocidade da oscilação
    public float floatAmplitude = 0.5f; // Altura máxima da oscilação
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position; // Guarda a posição inicial
        CollectableUI.instance.InitializeCollectable(collectableName, collectableSprite, 0);
    }

    void Update()
    {
        // Movimento suave usando seno para criar o efeito de flutuação
        float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    void OnTriggerEnter2D(Collider2D collision){
        if (collision.CompareTag("Player"))
        {
            GameManager.instance.collectedItems[collectableName] += 1; 
            CollectableUI.instance.UpdateCollectablesUI(collectableName, GameManager.instance.collectedItems[collectableName]);
            Destroy(gameObject);
        }
    }
}
