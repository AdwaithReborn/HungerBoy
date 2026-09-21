using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    public Transform player;

    public float spawnDistance = 12f;

    public float spawnInterval = 4f;

    public float minY = -2f;
    public float maxY = 0f;

    private float timer;

    void Start()
    {
        timer = spawnInterval;
    }

    void Update()
    {
        if (player == null)
            return;

        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            SpawnEnemy();
            timer = spawnInterval;
        }
    }

    void SpawnEnemy()
    {
        float side = Random.value < 0.5f ? -1f : 1f;

        float spawnX =
            player.position.x +
            side * spawnDistance;

        Vector3 spawnPosition = new Vector3(
            spawnX,
            transform.position.y,
            0f
        );

        Instantiate(
            enemyPrefab,
            spawnPosition,
            Quaternion.identity
        );
    }
}