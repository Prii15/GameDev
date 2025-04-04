using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    [Header ("Game Score")]
    public static int score = 0; // Pontuação do jogador
    public static int playerLives = 5; // Número de vidas do jogador
    public static int maxLives = 5;
    public static bool win = true;

    // Dicionário para armazenar os coletáveis (nome -> quantidade)
    public Dictionary<string, int> collectedItems = new Dictionary<string, int>();

    void Awake()
    {
        if (instance == null) 
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            // Inicializando itens coletaveis
            collectedItems["book"] = 0;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }


    // Adiciona pontos ao score
    // public static void AddScore(int points)
    // {
    //     score += points;
    // }

    // Método para obter a quantidade de um coletável específico
    public int GetCollectableCount(string itemName)
    {
        return collectedItems.ContainsKey(itemName) ? collectedItems[itemName] : 0;
    }

    public void LoseLife()
    {
        if (playerLives > 0)
        {
            playerLives--;
            FindObjectOfType<Health>().UpdateHearts(playerLives);

            if (playerLives <= 0)
            {
                GameOver();
            }
        }
    }

    public void GainLife()
    {
        playerLives++;
        FindObjectOfType<Health>().UpdateHearts(playerLives);
    
    }

    public static void GameOver(){
        win = false;
        SceneManager.LoadScene("GameOver");
    }
    
    
}
