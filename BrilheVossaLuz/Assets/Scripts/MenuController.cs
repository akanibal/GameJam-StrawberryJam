using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject configPanel, hintPanel, creditPanel;

    public void Start()
    {
        configPanel.SetActive(false);
        hintPanel.SetActive(false);
    }
    public void Play() 
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Config()
    {
        configPanel.SetActive(true);
    }

    public void ConfigExit()
    {
        configPanel.SetActive(false);
    }

    public void Hint()
    {
        hintPanel.SetActive(true);
    }

    public void HintExit()
    {
        hintPanel.SetActive(false);
    }

    public void Credit()
    {
        creditPanel.SetActive(true);
    }

    public void CreditExit()
    {
        creditPanel.SetActive(false);
    }
}
