using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public InputActionReference interact;
    public SpriteRenderer spriteRend;
    public Sprite[] sprites;
    public float moveSpeed = 5f;
    public float rotationSpeed = 50f;
    public int kills = 0;

    void Start()
    {
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

    void Rotate()
    {
        
    }
    void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        Vector3 movement = new Vector3(x, 0f, z).normalized;
        controller.Move(movement * moveSpeed * Time.deltaTime);
        if (movement.x == -1)
        {
            spriteRend.sprite = sprites[0];
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
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
