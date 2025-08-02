using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public InputActionReference interact;
    public SpriteRenderer spriteRend;
    public Sprite[] sprites;

    [SerializeField]
    private Camera mainCamera;


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

    void Start()
    {
<<<<<<< Updated upstream
        controller = GetComponent<CharacterController>();
        spriteRend = GetComponent<SpriteRenderer>();
=======
        collider1.enabled = true;
        collider2.enabled = true;
        light1.SetActive(false);
        light2.SetActive(false);
        light3.SetActive(false);

        spawner1.SetActive(true);
        spawner2.SetActive(false);
        spawner3.SetActive(false);
>>>>>>> Stashed changes
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            Debug.Log("attacked!");
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

    private void LateUpdate()
    {
        Vector3 cameraPosition = mainCamera.transform.position;
        cameraPosition.x = transform.position.x;
        cameraPosition.y = transform.position.y * 2;
        transform.LookAt(cameraPosition);
        transform.Rotate(0f, 180f, 0f);
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
        Move(movement);
    }

    void Move(Vector3 movement)
    {
        controller.Move(movement * moveSpeed * Time.deltaTime);
        if (movement.x == -1) spriteRend.sprite = sprites[0];
        else if (movement.x == 1) spriteRend.sprite = sprites[1];
        else if (movement.z == -1) spriteRend.sprite = sprites[2];
        else if (movement.z == 1) spriteRend.sprite = sprites[3];
    }

    public void AddKill()
    {
        kills++;
        if (kills == 5) {
            FaseTwo();
        } else if (kills == 10) {
            FaseThree();
        } else if (kills == 15) {
            EndGame();
        }
    }

    void FaseTwo() {
        DestroyAllEnemies();

        collider1.enabled = false;
        light1.SetActive(true);
        spawner2.SetActive(true);
    }

    void FaseThree() {
        DestroyAllEnemies();
        collider2.enabled = false;
        light2.SetActive(true);
        spawner3.SetActive(true);
    }

    void EndGame() {
        DestroyAllEnemies();

        spawner1.SetActive(false);
        spawner2.SetActive(false);
        spawner3.SetActive(false);

        light3.SetActive(true);
    }


    void DestroyAllEnemies()
    {
        GameObject[] objetos = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject obj in objetos)
        {
            Destroy(obj);
        }

        Debug.Log("Destruídos " + objetos.Length + " objetos com a tag: Enemy");
    }
}