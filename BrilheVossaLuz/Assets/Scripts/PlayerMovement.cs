using System;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public InputActionReference interact;
    public SpriteRenderer spriteRend;
    public Sprite[] sprites;

    public int currentIndex;
    [SerializeField] private Camera mainCamera;

    private GameObject directionPivot = default;

    public float moveSpeed = 5f;
    public float rotationSpeed = 50f;
    int kills = 0;

    public Collider collider1;
    public Collider collider2;

    public GameObject light1;
    public GameObject light2;
    public GameObject light3;

    public GameObject spawner1;
    public GameObject spawner2;
    public GameObject spawner3;

    public int killsLimit1 = 5;
    public int killsLimit2 = 10;
    public int killsLimit3 = 15;

    public TextMeshProUGUI killsText;


    void Start()
    {

        controller = GetComponent<CharacterController>();
        spriteRend = GetComponent<SpriteRenderer>();

        collider1.enabled = true;
        collider2.enabled = true;
        light1.SetActive(false);
        light2.SetActive(false);
        light3.SetActive(false);

        spawner1.SetActive(true);
        spawner2.SetActive(false);
        spawner3.SetActive(false);

        mainCamera.GetComponent<MainCamera>().changeCamera(0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            // Debug.Log("attacked!");
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("interacted!");
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            AddKill();
            Debug.Log("Kills:" + kills);
        }
    }

    void FixedUpdate()
    {
        GetMoveInput();
    }

    void GetMoveInput()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        Vector3 movement = new Vector3(x, 0f, z).normalized;
        transform.position = new Vector3(transform.position.x, 1.49f, transform.position.z);
        Move(movement);
    }

    void Move(Vector3 movement)
    {
        controller.Move(movement * moveSpeed * Time.deltaTime);
        if (movement.x == -1) currentIndex = 0; 
        else if (movement.x == 1) currentIndex = 1;
        else if (movement.z == -1) currentIndex = 2;
        else if (movement.z == 1) currentIndex = 3;

        spriteRend.sprite = sprites[currentIndex];
        directionPivot = transform.GetChild(0).gameObject;
        float[] yRotations = { 90f, 270f, 0f, 180f };
        directionPivot.transform.rotation = Quaternion.Euler(0, yRotations[currentIndex], 0);
    }

    public void AddKill()
    {
        kills++;
        UpdateKillsUI();

        if (kills == killsLimit1) {
            FaseTwo();
        } else if (kills == killsLimit2) {
            FaseThree();
        } else if (kills == killsLimit3) {
            EndGame();
        }
    }

    void FaseTwo() {
        DestroyAllEnemies();

        collider1.enabled = false;
        light1.SetActive(true);
        spawner2.SetActive(true);

        mainCamera.GetComponent<MainCamera>().changeCamera(1);
    }

    void FaseThree() {
        DestroyAllEnemies();
        collider2.enabled = false;
        light2.SetActive(true);
        spawner3.SetActive(true);

        mainCamera.GetComponent<MainCamera>().changeCamera(2);
    }

    void EndGame()
    {
        DestroyAllEnemies();

        spawner1.SetActive(false);
        spawner2.SetActive(false);
        spawner3.SetActive(false);

        light3.SetActive(true);

        FixAllTasks();

        SceneManager.LoadScene(2);
    }

    void DestroyAllEnemies()
    {
        GameObject[] objetos = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject obj in objetos)
        {
            Destroy(obj);
        }

        Debug.Log("Destru�dos " + objetos.Length + " objetos com a tag: Enemy");
    }

    void FixAllTasks()
    {
        GameObject[] objetos = GameObject.FindGameObjectsWithTag("Task");

        foreach (GameObject obj in objetos)
        {
            Task task = obj.GetComponent<Task>();
            
                task.Fix();
            
        }

        Debug.Log("Consertados " + objetos.Length + " objetos com a tag: Task");
    }

    public int KillsForNextFase()
    {
        if (kills < killsLimit1)
            return killsLimit1 - kills;
        else if (kills < killsLimit2)
            return killsLimit2 - kills;
        else if (kills < killsLimit3)
            return killsLimit3 - kills;
        else
            return 0;
    }

    void UpdateKillsUI()
    {
        int restantes = KillsForNextFase();
        killsText.text = restantes + "x";
    }
}