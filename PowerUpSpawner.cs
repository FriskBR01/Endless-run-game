using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    public GameObject powerUpPrefab;
    public Transform player;
    public float spawnDistance = 20f;
    public float spawnInterval = 10f;

    private float nextSpawn = 0f;

    void Update()
    {
        if (Time.time > nextSpawn)
        {
            Vector3 spawnPos = new Vector3(player.position.x + spawnDistance, Random.Range(-2f, 2f), 0);
            Instantiate(powerUpPrefab, spawnPos, Quaternion.identity);
            nextSpawn = Time.time + spawnInterval;
        }
    }
}
