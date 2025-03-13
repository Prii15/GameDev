using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    public TMP_Text winlooseText;
    public TMP_Text scoreText;
    public Button RestartButton;

    void Start()
    {
        scoreText.text = "Final Score: " + GameManager.score;

        if (GameManager.win)
        {
            winlooseText.text = "Vitória! :) ";
        }
        else
        {
            winlooseText.text = "Derrota! :( ";
        }

        RestartButton.onClick.AddListener(RestartGame);
    }

    // Reinicia o jogo
    public void RestartGame()
    {
        GameManager.score = 0;
        GameManager.life = 3;
        GameManager.win = true;
        GameManager.GlobalSpeedMultiplier = 1;
        SceneManager.LoadScene("Fase1");
    }
}
