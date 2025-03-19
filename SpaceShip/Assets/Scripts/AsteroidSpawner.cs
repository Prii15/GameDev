using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] asteroidPrefabs;
    [SerializeField] private float spawnRate = 1.5f;
    [SerializeField] private float minY = -3.25f;
    [SerializeField] private float maxY = 3.25f;
    [SerializeField] private float spawnX = 7f;

    void Start()
    {
        InvokeRepeating("SpawnAsteroid", 2f, spawnRate);
    }

    void SpawnAsteroid()
    {
        float randomY = Random.Range(minY, maxY);
        int randomIndex = Random.Range(0, asteroidPrefabs.Length);

        Vector2 spawnPosition = new Vector2(spawnX, randomY);
        Instantiate(asteroidPrefabs[randomIndex], spawnPosition, Quaternion.identity);
    }
}
