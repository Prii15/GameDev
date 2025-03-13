using UnityEngine;

public class bigInvaderSpawn : MonoBehaviour
{
    [SerializeField] private GameObject bigInvaderPrefab;
    private float minSpawnTime = 5f;
    private float maxSpawnTime = 10f;
    private float spawnTimer;

    void Start()
    {
        ResetSpawnTimer();
    }

    void Update()
    {
        spawnTimer -= Time.deltaTime; // Conta o tempo decrescente

        if (spawnTimer <= 0)
        {
            SpawnMotherShip();
            ResetSpawnTimer();
        }
    }

    void SpawnMotherShip()
    {
        Vector2 spawnPosition = new Vector2(-7f, 4.7f); // Canto superior esquerdo
        Instantiate(bigInvaderPrefab, spawnPosition, Quaternion.identity);
    }

    void ResetSpawnTimer()
    {
        spawnTimer = Random.Range(minSpawnTime, maxSpawnTime); // Define novo tempo
    }

}
