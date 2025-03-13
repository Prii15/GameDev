using UnityEngine;

public class playerControl : MonoBehaviour
{
    private Rigidbody2D rb2d;

    [Header("Move Player")]
    [SerializeField] private float moveSpeed = 10f;       // Define a velocidade do moveimrnto
    [SerializeField] private float boundX = 12f;   // Define os limites em X
    private float xInput;                           //Define a variavel que vai armazenar as teclas usadas para movimento

    [Header("Bullet")]
    [SerializeField] GameObject missilePrefab;
    [SerializeField] private float fireRate = 0.5f, nextFire;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    //Cria o missil ao atirar, com o prefab do missil, na posição do player sem rotação
    void Fire()
    {
        Instantiate(missilePrefab, transform.position, Quaternion.identity);
        nextFire = Time.time + fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        //Movimenta o player
        xInput = Input.GetAxisRaw("Horizontal"); //Acessa as teclas de movimento horizontal
        rb2d.linearVelocity = new Vector2(xInput * moveSpeed, rb2d.linearVelocity.y);

        //Mantem o player dentro dos limites
        var pos = transform.position;           // Acessa a Posição do player
        if (pos.x > boundX) {                  
            pos.x = boundX;                     // Corrige a posicao do player caso ele ultrapasse o limite esuqerdo
        }
        else if (pos.x < -boundX) {
            pos.x = -boundX;                    // Corrige a posicao do player caso ele ultrapasse o limite direito
        }
        transform.position = pos;               // Atualiza a posição do player

        //Player atira
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= nextFire)
        {
            Fire();
        }
    }
}
