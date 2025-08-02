using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyController : MonoBehaviour
{
    public float speed = 5f;
    public float speedBoost = 3f;

    private bool canBreak = false;

    public float radius = 5f;
    Vector3 nextPosition;

    public float limitXP;
    public float limitXN;
    public float limitZP;
    public float limitZN;
    public GameObject[] taskList;
    public GameObject[] zones;
    public PlayerMovement playerController;


    private float roamingDuration = 3f;

    public int lifes = 2;

    void Start()
    {
        StartCoroutine(EnemyRoutine());
    }

    IEnumerator EnemyRoutine()
    {
        // 1. Primeira movimentacao aleatoria
        yield return StartCoroutine(Wander(roamingDuration, true));

        // 2. Vai ate uma task
        yield return StartCoroutine(GoToTarget(taskList[Random.Range(0, taskList.Length)].transform.position, true));
        
        yield return new WaitForSeconds(2f);

        yield return StartCoroutine(Wander(roamingDuration));

        // 4. Vai para a "morte"
        yield return StartCoroutine(GoToTarget(GerarDestinoAleatorio(zones)));

        Destroy(gameObject); // logica de fim
    }

    IEnumerator Wander(float duration, bool goToTarget = false)
    {
        float timer = 0f;

        RandomNextPosition(); // sorteia o primeiro destino

        while (timer < duration)
        {
            timer += Time.deltaTime;

            Vector3 direction = (nextPosition - transform.position).normalized;
            float distance = Vector3.Distance(transform.position, nextPosition);

            if (distance > 0.1f)
            {
                transform.position += direction * speed * Time.deltaTime;
            }
            else if (goToTarget)
            {
                RandomNextPosition();
            }

            yield return null;
        }
    }

    IEnumerator GoToTarget(Vector3 target, bool allowBreak = false)
    {
        if (allowBreak)
        {
            canBreak = true;
        }
        Vector3 fixedTarget = new Vector3(target.x, transform.position.y, target.z);

        while (Vector3.Distance(transform.position, fixedTarget) > 0.1f)
        {
            Vector3 direction = (fixedTarget - transform.position).normalized;

            // Aplica o movimento s� em X e Z
            direction.y = 0;

            transform.position += direction * speed * speedBoost * Time.deltaTime;
            yield return null;
        }

        
    }

    void Update() {
        if (lifes <= 0) {
            playerController.AddKill();
            Destroy(gameObject);
        }
    }

    public void Hit()
    {
        lifes--;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Task") && canBreak) {
            canBreak = false;
            Task task = other.GetComponent<Task>();
            if (task != null) {
                task.Break();
            }
        }
    }

    void RandomNextPosition() {
        float randomX = Random.Range(limitXN, limitXP);
        float randomZ = Random.Range(limitZN, limitZP);


        nextPosition = new Vector3(
            randomX,
            transform.position.y,
            randomZ
        );

    }

    Vector3 GerarDestinoAleatorio(GameObject[] zones)
    {
        if (zones == null || zones.Length == 0)
        {
            Debug.LogWarning("Lista de zonas esta vazia ou nula.");
            return transform.position;
        }

        GameObject alvo = zones[Random.Range(0, zones.Length)];
        Debug.Log("Objeto alvo escolhido: " + alvo.name);

        Collider rend = alvo.GetComponent<Collider>();

        if (rend != null)
        {
            Bounds b = rend.bounds;

            Debug.Log("Bounds do alvo:");
            Debug.Log("Min: " + b.min);
            Debug.Log("Max: " + b.max);

            float randomX = Random.Range(b.min.x, b.max.x);
            float randomY = transform.position.y;
            float randomZ = Random.Range(b.min.z, b.max.z);

            Vector3 destino = new Vector3(randomX, randomY, randomZ);

            Debug.Log("Destino aleatorio gerado: " + destino);

            return destino;
        }

        Debug.LogWarning("Renderer nao encontrado no objeto: " + alvo.name);
        return transform.position;
    }

    public void PlayerReference(PlayerMovement playerRef)
    {
        playerController = playerRef;
    }
}