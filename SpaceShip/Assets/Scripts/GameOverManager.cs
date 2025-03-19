using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    public TMP_Text winStatusText;
    public TMP_Text scoreText;
    public Button RestartButton;

    void Start()
    {
        scoreText.text = "Final Score: " + GameManager.score;

        if (GameManager.win)
        {
            winStatusText.text = "Vitória! :) ";
        }
        else
        {
            winStatusText.text = "Derrota! :( ";
        }

        RestartButton.onClick.AddListener(RestartGame);
    }

    // Reinicia o jogo
    public void RestartGame()
    {
        GameManager.score = 0;
        GameManager.lives = 3;
        GameManager.win = true;
        SceneManager.LoadScene("Fase");
    }
}
