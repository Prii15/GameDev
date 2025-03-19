using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Rigidbody2D rb2d;               // Define o corpo rigido 2D que representa o player

    [Header("Move Player")]
    [SerializeField] private float speed = 10f;       // Define a velocidade do moveimrnto
    [SerializeField] private float boundY = 3.25f;   // Define os limites em Y
    private float yInput;                           //Define a variavel que vai armazenar as teclas usadas para movimento

    [Header("Player Bullet")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float fireRate = 0.5f;
    private float nextFireTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();     // Inicializa a raquete
    }

    // Update is called once per frame
    void Update () {
        Move();
        Shoot();
    }

    void Move(){
        //Movimenta o player
        yInput = Input.GetAxisRaw("Vertical"); //Acessa as teclas de movimento horizontal
        rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, yInput * speed);

        //Mantem o player dentro dos limites
        var pos = transform.position;           // Acessa a Posição do player
        if (pos.y > boundY) {                  
            pos.y = boundY;                     // Corrige a posicao do player caso ele ultrapasse o limite esuqerdo
        }
        else if (pos.y < -boundY) {
            pos.y = -boundY;                    // Corrige a posicao do player caso ele ultrapasse o limite direito
        }
        transform.position = pos;               // Atualiza a posição do player
    }

    void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= nextFireTime)
        {
            Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            nextFireTime = Time.time + fireRate;
        }
    }
}
