using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;

    public Transform[] spawnPoints;

    public float spawnInterval = 5f;

    public int maxEnemies = 8;

    private float timer;

    void Start()
    {
        timer = spawnInterval;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            SpawnEnemy();

            timer = spawnInterval;
        }
    }

    void SpawnEnemy()
    {
        GameObject[] enemies =
            GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Length >= maxEnemies)
            return;

        if (spawnPoints.Length == 0)
            return;

        int randomIndex =
            Random.Range(0, spawnPoints.Length);

        Transform spawnPoint =
            spawnPoints[randomIndex];

        Instantiate(
            enemyPrefab,
            spawnPoint.position,
            Quaternion.identity
        );
    }
}