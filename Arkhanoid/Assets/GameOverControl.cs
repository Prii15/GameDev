using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameOverControl : MonoBehaviour{

    public TMP_Text gameOverText;
    public TMP_Text scoreText;

    public Button RestartButton;
    public Button MenuButton; 

    void Start()
    {
        scoreText.text = "Final Score: " + GameManager.totalScore;

        if (GameManager.win)
        {
            gameOverText.text = "Vitória! :) ";
        }
        else
        {
            gameOverText.text = "Derrota! :( ";
        }

        RestartButton.onClick.AddListener(RestartGame);
        MenuButton.onClick.AddListener(GoToMenu);
    }

    // Reinicia a cena atual
    public void RestartGame()
    {
        SceneManager.LoadScene("Fase1");
    }

    // Volta para a tela do Menu
    public void GoToMenu()
    {
        SceneManager.LoadScene("Start");  // Certifique-se que a cena do menu se chama "Start"
    }
}
