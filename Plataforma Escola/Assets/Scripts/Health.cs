using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Health : MonoBehaviour
{
    public GameObject heartPrefab; // Prefab do coração
    public Transform heartsParent; // Objeto vazio no Canvas para armazenar os corações
    public Sprite fullHeart;       // Sprite do coração cheio
    public Sprite emptyHeart;      // Sprite do coração vazio

    private List<Image> hearts = new List<Image>(); // Lista de corações na UI

    private void Start()
    {
        UpdateHearts(GameManager.playerLives);
    }

    public void UpdateHearts(int currentLives)
    {
        // Remove todos os corações antigos
        foreach (Image heart in hearts)
        {
            Destroy(heart.gameObject);
        }
        hearts.Clear();

        // Cria novos corações com base nas vidas atuais
        for (int i = 0; i < GameManager.maxLives; i++)
        {
            GameObject heartObj = Instantiate(heartPrefab, heartsParent);
            Image heartImage = heartObj.GetComponent<Image>();

            if (i < currentLives)
                heartImage.sprite = fullHeart;  // Mostra coração cheio
            else
                heartImage.sprite = emptyHeart; // Mostra coração vazio

            hearts.Add(heartImage);

            // Ajuste de posição para espaçar os corações
            RectTransform rt = heartObj.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(i, 0); // Espaçamento horizontal

        }
    }


}
