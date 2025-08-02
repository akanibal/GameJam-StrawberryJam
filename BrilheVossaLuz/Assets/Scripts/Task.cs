using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class Task : MonoBehaviour
{
    public MeshFilter meshFilter;
    public Mesh brokenMesh;
    public Mesh fixedMesh;
    public bool broken;
    public bool playerIn = false;
    public bool enemyIn = false;
    public int repairCount;
    public int maxRepairCount;

    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        if (meshFilter != null)
        {
            fixedMesh = meshFilter.mesh;
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

    void Break()
    {
        repairCount = maxRepairCount;
        broken = true;
        meshFilter.mesh = brokenMesh;
    }

    void Fix()
    {
        broken = false;
        Debug.Log("fixed!");
        meshFilter.mesh = fixedMesh;
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
