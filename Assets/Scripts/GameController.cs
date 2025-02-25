using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int killCount = 0;

    public GameObject enemyPrefab;
    public float spawnRange = 10;
    public float spawnTime = 10;
    public int maxEnemiesAtOnce = 10;

    public TextMeshProUGUI killCountText;

    void Start()
    {
        killCount = 0;
        killCountText.text = "Kills: " + killCount;
        InvokeRepeating("spawnEnemy", 0, spawnTime);
    }

    void spawnEnemy()
    {
        int enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

        if (enemyCount <= maxEnemiesAtOnce)
        {
            Vector3 loc = new Vector3(Random.Range(-spawnRange, spawnRange), Random.Range(-spawnRange, spawnRange), 0);
            Quaternion rot = Quaternion.Euler(0, 0, Random.Range(0f, 360f));
            Instantiate(enemyPrefab, loc, rot);
        }
    }

    // Update is called once per frame
    void Update()
    {
        killCountText.text = killCount + "Kills";
    }
}
