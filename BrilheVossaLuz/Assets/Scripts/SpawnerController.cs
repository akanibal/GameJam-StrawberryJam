using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    public float spawnInterval = 100f;

    private float timer;
    public GameObject enemyPrefab;

    public EnemyController enemyScript;

    public PlayerMovement playerController;

    public GameObject[] taskList;

    public GameObject[] zones; //Objeto qualquer com collider

    public float spawnY;

    public float spawXP;
    public float spawXN;
    public float spawZP;
    public float spawZN;

    void Start() {
        enemyScript = GetComponent<EnemyController>();
        SpawnEnemy();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    void SpawnEnemy()
    {
        float randomX = Random.Range(spawXN, spawXP);
        float randomZ = Random.Range(spawZN, spawZP);
        Vector3 spawnPosition = new Vector3(randomX, spawnY, randomZ);

        GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        enemyScript = newEnemy.GetComponent<EnemyController>();

        enemyScript.limitXP = spawXP;
        enemyScript.limitXN = spawXN;
        enemyScript.limitZP = spawZP;
        enemyScript.limitZN = spawZN;
        enemyScript.taskList = taskList;
        enemyScript.zones = zones;
        enemyScript.PlayerReference(playerController);
    }
}
