using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Return()
    {
        SceneManager.LoadScene(1);
    }

      public void Ecsit()
    {
        SceneManager.LoadScene(0);
    }
}
