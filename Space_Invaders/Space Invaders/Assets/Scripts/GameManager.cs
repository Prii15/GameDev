using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // Singleton para acessar de qualquer lugar
    public static float GlobalSpeedMultiplier = 1f;

    public TMP_Text scoreText;
    public TMP_Text livesText;

    [Header ("Game Score")]
    public static int score = 0; // Pontuação do jogador
    public static int life = 3; // Número de vidas do jogador
    public static bool win = true;

    // Adiciona pontos ao score
    public static void AddScore(int points)
    {
        score += points;
    }

    // Decrementa a vida do jogador
    public static void LoseLife()
    {
        life--;
        
        if (life <= 0)
        {
            win = false;
            SceneManager.LoadScene("GameOver");
        }
    }

    

    void Update()
    {
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Enemy");
        
        if(gos.Length == 0){
            SceneManager.LoadScene("GameOver");
        }
    }

    void LateUpdate(){
        scoreText.text = "Score: " + score;
        livesText.text = "Lives: " + life;
    }
    
    

}
