using UnityEngine;

public class invaderSpawner : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    [SerializeField] private GameObject[] enemyTypes; // Array com os diferentes tipos de inimigos

    [Header("Grid Config")]
    [SerializeField] private int columns = 10;  // Número de inimigos por linha
    [SerializeField] private int rows = 6;      // Número total de linhas
    [SerializeField] private float spacingX = 1.5f;  // Espaço horizontal entre inimigos
    [SerializeField] private float spacingY = 1.5f;  // Espaço vertical entre inimigos

    private void Start()
    {
        SpawnEnemies();
    }

    void SpawnEnemies()
    {
        for (int row = 0; row < rows; row++)
        {
            int enemyIndex = (row / 2) % enemyTypes.Length; // Muda o inimigo a cada 2 linhas
            GameObject enemyPrefab = enemyTypes[enemyIndex];

            for (int col = 0; col < columns; col++)
            {
                Vector2 spawnPos = new Vector2(transform.position.x + col * spacingX, transform.position.y + 0.5f - row * spacingY);
                Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
            }
        }
    }
}

