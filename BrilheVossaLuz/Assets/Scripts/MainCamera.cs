using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public Vector3[] positions = {
        new Vector3(-13.5f, 7f, 7.8f),
        new Vector3(-13.5f, 9.5f, -2.4f),
        new Vector3(-6.25f, 14f, -7f)
    };

    public void changeCamera(int index)
    {
        transform.position = positions[index];
    }
}
