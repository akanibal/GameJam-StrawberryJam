using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private GameObject attackArea = default;
    private bool attacking = false;
    private float attackDuration = 0.15f;
    private float attackTimer = 0f;
    private float attackCooldown = 0f;
    void Start()
    {
        attackArea = transform.GetChild(0).GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            Attack();
        }
        if (attacking)
        {
            attackTimer += Time.deltaTime;
            if (attackTimer >= attackDuration)
            {
                attackTimer = 0;
                attacking = false;
                attackArea.SetActive(attacking);
            }
        }
        attackCooldown -= Time.deltaTime;
    }

    private void Attack()
    {
        Debug.Log("attack!");
        if (attackCooldown <= 0)
        {
            attacking = true;
            attackCooldown = 1;
            attackArea.SetActive(attacking);

        }
    }
}
