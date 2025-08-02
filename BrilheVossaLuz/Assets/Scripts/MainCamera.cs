using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public Vector3[] positions = {
        new Vector3(-14f, 9.3f, 7f),
        new Vector3(-13.75f, 10f, -1.8f),
        new Vector3(-9.42f, 14f, -3f)
    };

    public void changeCamera(int index)
    {
        transform.position = positions[index];
    }
}
