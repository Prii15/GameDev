using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuckControl : MonoBehaviour
{
    private Rigidbody2D puck;               // Define o corpo rigido 2D que representa a bola
    
    // Start is called before the first frame update
    void Start()
    {
        puck = GetComponent<Rigidbody2D>(); // Inicializa o objeto bola
    }

    void OnCollisionEnter2D (Collision2D coll) {
        if(coll.collider.CompareTag("Player")){
            Vector2 vel;
            vel.x = puck.velocity.x;
            vel.y = (puck.velocity.y / 2) + (coll.collider.attachedRigidbody.velocity.y / 3);
            puck.velocity = vel;
        }
    }

    // Reinicializa a posição e velocidade da bola
    void ResetBall(){
        puck.velocity = Vector2.zero;
        transform.position = Vector2.zero;
    }

    // Reinicializa o jogo
    public void RestartGame(){
        ResetBall();
        // // Zerar a pontuação
        // GameManager.PlayerScore1 = 0;
        // GameManager.PlayerScore2 = 0;
    }
}
