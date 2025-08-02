using Unity.VisualScripting;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            EnemyController enemy = other.GetComponent<EnemyController>();
            enemy.Hit();
        }
    }
}
