using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public int health = 1; // Quantidade de vezes que precisa ser atingido
    public int scoreValue = 100; // Pontos ao quebrar o bloco

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage()
    {
        health--;

        if (health <= 0)
        {
            GameManager.AddScore(scoreValue); // Adiciona pontos ao score
            Destroy(gameObject);
        }
    }
}
