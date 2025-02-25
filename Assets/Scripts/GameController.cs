using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameController : MonoBehaviour
{
    public int killCount = 0;

    public GameObject enemyPrefab;
    public float spawnRange = 10;
    public float spawnTime = 10;
    public int maxEnemiesAtOnce = 10;

    public TextMeshProUGUI killCountText;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;

    void Start()
    {
        killCount = 0;
        killCountText.text = "Kills: " + killCount;
        InvokeRepeating("SpawnEnemy", 0, spawnTime);

        gameOverText.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
    }

    void SpawnEnemy()
    {
        int enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

        if (enemyCount <= maxEnemiesAtOnce)
        {
            Vector3 loc = new Vector3(Random.Range(-spawnRange, spawnRange), Random.Range(-spawnRange, spawnRange), 0);
            Quaternion rot = Quaternion.Euler(0, 0, Random.Range(0f, 360f));
            Instantiate(enemyPrefab, loc, rot);
        }
    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Update is called once per frame
    void Update()
    {
        killCountText.text = killCount + (killCount == 1 ? " Kill" : " Kills");
    }
}
