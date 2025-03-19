using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Texts")]
    public static int score = 0;
    public static int lives = 3;
    public static bool win = true;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text livesText;

    [Header("Parallax Slow")]

    private float parallaxTimer = 5f;  // Tempo para restaurar a velocidade
    private bool isParallaxSlowed = false;
    private int lastSlowedScore = 0;  // Armazena o último score onde a desaceleração foi aplicada

    void Update()
    {
        scoreText.text = "Score: " + score;
        livesText.text = "Lives: " + lives;

        // Verifique se o score ultrapassou um múltiplo de 50 e se a desaceleração ainda não foi aplicada
        if (score / 50 > lastSlowedScore / 50 && !isParallaxSlowed)
        {
            foreach (var parallax in FindObjectsOfType<Parallax>())
            {
                parallax.SlowParallax();
            }
            isParallaxSlowed = true;
            lastSlowedScore = score;  // Atualiza o último score que causou a desaceleração
            Debug.Log("Velocidade do parallax reduzida para todos os objetos.");
        }

        // Temporizador para restaurar a velocidade
        if (isParallaxSlowed)
        {
            parallaxTimer -= Time.deltaTime;
            if (parallaxTimer <= 0)
            {
                foreach (var parallax in FindObjectsOfType<Parallax>())
                {
                    parallax.RestoreParallaxSpeed();
                }
                isParallaxSlowed = false;
                parallaxTimer = 5f;  // Resetando o timer
            }
        }
    }

    public static void AddScore(int amount)
    {
        score += amount;

        if (score >= 500)
        {
            Victory(); // Chama a função de vitória
        }
    }

    public static void PlayerHit()
    {
        lives -= 1;

        if (lives <= 0)
        {
            GameOver();
        }
    }

    public static void GameOver()
    {
        win = false;
        SceneManager.LoadScene("GameOver");
    }

    private static void Victory()
    {
        win = true;
        SceneManager.LoadScene("GameOver");
    }

}
