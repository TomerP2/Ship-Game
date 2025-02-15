using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnRange = 10;
    public float spawnTime = 10;
    void Start()
    {
        InvokeRepeating("spawnEnemy", 0, spawnTime);
    }

    void spawnEnemy()
    {
        Vector3 loc = new Vector3(Random.Range(-spawnRange, spawnRange), 0, Random.Range(-spawnRange, spawnRange));
        Quaternion rot = Quaternion.Euler(0, Random.Range(0f, 360f), 0);
        Instantiate(enemyPrefab, loc, rot);
    }

    void Update()
    {
        
    }
}
