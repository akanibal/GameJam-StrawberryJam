using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public enum TaskType
{
    Banheiro,
    Cozinha,
    Sala
}
public class Task : MonoBehaviour
{
    private GameObject brokenIcon = default;
    public MeshFilter meshFilter;
    public Mesh brokenMesh;
    public Mesh fixedMesh;
    public TaskType type;
    public bool broken;
    public bool playerIn = false;
    public bool enemyIn = false;
    public int repairCount;
    public int maxRepairCount;

    public GameObject healthBar = default;

    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        brokenIcon = transform.GetChild(0).gameObject;
        if (meshFilter != null)
        {
            fixedMesh = meshFilter.mesh;
        }
        switch (type)
        {
            case TaskType.Banheiro:
                brokenIcon.GetComponent<BrokenIcon>().setSprite(0);
                break;
            case TaskType.Cozinha:
                brokenIcon.GetComponent<BrokenIcon>().setSprite(1);
                break;
            case TaskType.Sala:
                brokenIcon.GetComponent<BrokenIcon>().setSprite(2);
                break;
            default:
                Debug.Log("Invalid Icon");
                break;
        }
        Break();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (playerIn) repairCount--;
            if (repairCount == 0) Fix();
        }
    }

    public void Break()
    {
        repairCount = maxRepairCount;
        broken = true;
        brokenIcon.SetActive(broken);
        meshFilter.mesh = brokenMesh;
        healthBar.GetComponent<HealthBarUI>().Reduction += 1;
    }

    void Fix()
    {
        broken = false;
        brokenIcon.SetActive(broken);
        meshFilter.mesh = fixedMesh;
        healthBar.GetComponent<HealthBarUI>().Reduction -= 1;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerIn = true;
            Debug.Log("player in!");
        }
        else if (other.gameObject.tag == "Enemy") enemyIn = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerIn = false;
            Debug.Log("player out!");
        }
        else if (other.gameObject.tag == "Enemy") enemyIn = false;
    }


}
