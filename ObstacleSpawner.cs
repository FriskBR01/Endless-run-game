using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    public GameObject[] powerUpPrefabs;
    public float spawnInterval = 1.25f;
    public float spawnX = 10f;
    public float minY = -1f;
    public float maxY = 1f;

    private float timer = 0f;

    void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.IsGameOver) return;

        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnSomething();
        }
    }

    void SpawnSomething()
    {
        float r = Random.value;

        if (powerUpPrefabs != null && powerUpPrefabs.Length > 0 && r < 0.12f)
        {
            GameObject p = powerUpPrefabs[Random.Range(0, powerUpPrefabs.Length)];
            Vector3 pos = new Vector3(spawnX, Random.Range(minY, maxY), 0f);
            Instantiate(p, pos, Quaternion.identity);
            return;
        }

        if (obstaclePrefabs == null || obstaclePrefabs.Length == 0) return;

        GameObject obj = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
        Vector3 position = new Vector3(spawnX, Random.Range(minY, maxY), 0f);
        Instantiate(obj, position, Quaternion.identity);
    }
}