using System;
using UnityEngine;
using UnityEngine.InputSystem;

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
    public int kills = 0;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        spriteRend = GetComponent<SpriteRenderer>();
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
        if (movement.x == -1) currentIndex = 0; 
        else if (movement.x == 1) currentIndex = 1;
        else if (movement.z == -1) currentIndex = 2;
        else if (movement.z == 1) currentIndex = 3;

        spriteRend.sprite = sprites[currentIndex];
        directionPivot = transform.GetChild(0).gameObject;
        float[] yRotations = { 75f, 255f, -15f, 165f };
        directionPivot.transform.rotation = Quaternion.Euler(0, yRotations[currentIndex], 0);
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

    }

    void FaseThree() { 

    }

    void EndGame() { 
    
    }
}
