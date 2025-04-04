using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
    private Rigidbody2D rb2d;               // Define o corpo rigido 2D que representa o player
    private Animator anim;

    [Header("Move Player")]
    [SerializeField] private float runSpeed = 6f;       // Define a velocidade do moveimento
    [SerializeField] private float walkSpeed = 3f;       // Define a velocidade do moveimento
    [SerializeField] private float speed = 3f;       // Define a velocidade do moveimento
    [SerializeField] private float jumpForce = 8.5f;    //Define a velocidade do pulo
    private float xInput;                           //Define a variavel que vai armazenar as teclas usadas para movimento
    public static bool facingRight = true;
    private bool isAttacking = false;
    private bool isJumping = false;
    private bool isRunning = false;

    [Header("Bullet")]
    [SerializeField] GameObject bookPrefab;
    [SerializeField] private float attackCooldown = 0.5f; // Tempo entre os ataques 
    private float lastAttackTime = 0f;   // Armazena o tempo do último ataque

    [Header("Collision Check")]
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private Transform groundCheck;
    public bool isGrounded;
    public LayerMask WhatIsGround;

    [Header("Take Damage")]
    [SerializeField] private float invulnerableTime = 2f; // Tempo de invulnerabilidade após tomar dano
    [SerializeField] private float blinkInterval = 0.2f; // Tempo entre cada piscada
    private SpriteRenderer spriteRenderer;



    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();     // Inicializa o rigidbody
        anim = GetComponent<Animator>();        // Inicializa o animator
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update () {

        CollisionChecks();
        AnimationController();
        FlipController();
        FallOut();

        xInput = Input.GetAxisRaw("Horizontal"); //Acessa as teclas de movimento horizontal
        Move();

        if(Input.GetKeyDown(KeyCode.LeftControl)){
            isRunning = !isRunning;
            RunWalk();
        }

        if(Input.GetKeyDown(KeyCode.UpArrow)){
            Jump();
        }

        if(Input.GetKeyDown(KeyCode.Space)){
            Attack();
        }

        if (isAttacking && Time.time >= lastAttackTime + attackCooldown)
        {
            if(facingRight){
                Instantiate(bookPrefab, (Vector2)transform.position + new Vector2(0, 0.4f), Quaternion.identity);
            }
            else if(!facingRight){
                Instantiate(bookPrefab, (Vector2)transform.position + new Vector2(0, 0.4f), Quaternion.identity);
            }
            isAttacking = false;
        }
        

    }

    private void AnimationController(){
        anim.SetFloat("xVelocity", rb2d.linearVelocity.x);
        anim.SetBool("isAttacking", isAttacking);
        anim.SetBool("isJumping", !isGrounded);
        anim.SetBool("isRunning", isRunning);
    }

    private void FlipController(){
        if(rb2d.linearVelocity.x < 0 && facingRight){
            Flip();
        }
        else if(rb2d.linearVelocity.x > 0 && !facingRight){
            Flip();
        }
    }

    private void Flip(){
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    private void Move(){
        //Movimenta o player
        rb2d.linearVelocity = new Vector2(xInput * speed, rb2d.linearVelocity.y);
    }

    private void Jump(){
        if(isGrounded){
            rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, jumpForce);
        }
    }

    private void RunWalk(){
        if(isRunning){
            speed = runSpeed;
        }
        else{
            speed = walkSpeed;
        }
    }

    private void Attack(){
        if (!isAttacking) // Verifica se o cooldown já passou
        {
            isAttacking = true;
            lastAttackTime = Time.time; // Atualiza o tempo do último ataque
        }
    }

    private void CollisionChecks(){
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, WhatIsGround);
    }

    private void OnDrawGizmos(){
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }

    private void TakeDamage()
    {
        GameManager.instance.LoseLife();
        StartCoroutine(BlinkEffect());
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

    private void FallOut(){
        if (transform.position.y < -6)
        {
            Debug.Log("O jogador caiu para fora do mapa!");
            GameManager.GameOver();
        }
    }

    void OnTriggerEnter2D(Collider2D collision){

        if (collision.CompareTag("Enemy"))
        {
            TakeDamage();
        }
    }

}
