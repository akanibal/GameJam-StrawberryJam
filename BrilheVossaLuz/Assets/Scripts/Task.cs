using UnityEngine;

public enum TaskType
{
    Bathroom,
    Kitchen,
    LivingRoom
}
public class Task : MonoBehaviour
{
    private GameObject brokenIcon = default;
    public MeshFilter meshFilter;
    public Mesh brokenMesh;
    public Mesh fixedMesh;
    public TaskType type;
    public bool broken = true;
    public bool playerIn = false;
    public bool enemyIn = false;
    public int repairCount;
    public int maxRepairCount;

    public GameObject healthBar = default;

    public int damageValue;

    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        brokenIcon = transform.GetChild(0).gameObject;
        type = (TaskType)System.Enum.Parse(typeof(TaskType), transform.parent.gameObject.name);
        if (meshFilter != null)
        {
            fixedMesh = meshFilter.mesh;
        }
        switch (type)
        {
            case TaskType.Bathroom:
                brokenIcon.GetComponent<BrokenIcon>().setSprite(0);
                break;
            case TaskType.Kitchen:
                brokenIcon.GetComponent<BrokenIcon>().setSprite(1);
                break;
            case TaskType.LivingRoom:
                brokenIcon.GetComponent<BrokenIcon>().setSprite(2);
                break;
            default:
                Debug.Log("Invalid Icon");
                break;
        }

        broken = false;
        brokenIcon.SetActive(broken);
        meshFilter.mesh = fixedMesh;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K) && broken)
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
        healthBar.GetComponent<HealthBarUI>().Reduction += damageValue;
        Debug.Log("Break");
    }

    public void Fix()
    {
        broken = false;
        brokenIcon.SetActive(broken);
        meshFilter.mesh = fixedMesh;
        healthBar.GetComponent<HealthBarUI>().Reduction -= damageValue;
        Debug.Log("Fix");
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
