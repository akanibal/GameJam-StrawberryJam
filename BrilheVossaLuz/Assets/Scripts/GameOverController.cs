using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    public GameObject gameOverPanel;
    void Start()
    {
        gameOverPanel.SetActive(false);
    }
    public void Return()
    {
        SceneManager.LoadScene(1);
    }

      public void Ecsit()
    {
        SceneManager.LoadScene(0);
    }
}
