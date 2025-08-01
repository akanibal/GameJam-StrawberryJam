using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;
    public float moveSpeed = 5f;
    void Start()
    {

    }
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        Vector3 movement = new Vector3(x, 0f, z).normalized;
        controller.Move(movement * moveSpeed * Time.deltaTime);
    }
}
