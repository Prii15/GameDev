using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class invader : MonoBehaviour
{
    private Rigidbody2D rb2d;

    [Header("Movement Settings")]
    [SerializeField] private float baseSpeed = 5.0f;
    private float timer = 0.0f;
    private static float baseWaitTime = 1.0f; // Tempo inicial entre movimentos
    private float waitTime;
    private int moveSteps = 0;
    private int maxSteps = 3;

    [Header("Shooting Settings")]
    [SerializeField] private GameObject missilePrefab;
    [SerializeField] private float baseFireRate = 10f;
    private float fireRateTimer;
    private bool canFire = true;

    [Header("Score Settings")]
    [SerializeField] private int scoreValue = 10;
    [SerializeField] private Sprite explosionSprite;
    private SpriteRenderer spriteRenderer;

    // Lista de todos os invaders vivos
    private static List<invader> allInvaders = new List<invader>();

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        allInvaders.Add(this); // Adiciona o invader à lista global

        rb2d.linearVelocity = new Vector2(baseSpeed, 0);
        UpdateSpeed();

        ResetFireRateTimer();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= waitTime)
        {
            ChangeState();
            timer = 0.0f;
        }

        if (canFire)
        {
            fireRateTimer -= Time.deltaTime;

            if (fireRateTimer <= 0f)
            {
                Fire();
                ResetFireRateTimer();
            }
        }
    }

    void ChangeState()
    {
        moveSteps++;

        if (moveSteps < maxSteps)
        {
            rb2d.linearVelocity = new Vector2(-rb2d.linearVelocity.x, 0);
        }
        else
        {
            Descend();
            rb2d.linearVelocity = new Vector2(-rb2d.linearVelocity.x, 0);
            moveSteps = 0;
        }
    }

    void Descend()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y - 0.5f);
    }

    void Fire()
    {
        if (Random.value < 0.05f * GameManager.GlobalSpeedMultiplier) 
        {
            canFire = false;
            Instantiate(missilePrefab, transform.position, Quaternion.identity);
        }
    }

    void ResetFireRateTimer()
    {
        fireRateTimer = Random.Range(5f / GameManager.GlobalSpeedMultiplier, baseFireRate / GameManager.GlobalSpeedMultiplier);
        canFire = true;
    }

    public void Die()
    {
        Animator animator = GetComponent<Animator>();
        animator.enabled = false;

        spriteRenderer.sprite = explosionSprite;
        GameManager.AddScore(scoreValue);

        allInvaders.Remove(this); // Remove da lista de invaders vivos

        // Aumenta a velocidade global
        GameManager.GlobalSpeedMultiplier += 0.05f;

        // Atualiza a velocidade de TODOS os invaders vivos
        UpdateAllInvadersSpeed();

        Destroy(gameObject, 0.5f);
    }

void UpdateSpeed()
{
    // Mantém a direção atual (1 para direita, -1 para esquerda)
    float direction = Mathf.Sign(rb2d.linearVelocity.x);

    rb2d.linearVelocity = new Vector2(direction * baseSpeed * GameManager.GlobalSpeedMultiplier, 0);
    waitTime = baseWaitTime / GameManager.GlobalSpeedMultiplier;
}

    // Atualiza a velocidade de todos os invaders vivos
    public static void UpdateAllInvadersSpeed()
    {
        foreach (invader inv in allInvaders)
        {
            if (inv != null)
            {
                inv.UpdateSpeed();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BottomWall"))
        {
            GameManager.GameOver(); 
        }
    }

}
