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
            Debug.Log("attacked!");
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("interacted!");
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

    }

    void FaseThree() { 

    }

    void EndGame() { 
    
    }
}
