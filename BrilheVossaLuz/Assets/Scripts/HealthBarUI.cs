using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public float Health, MaxHealth, Reduction, Width, Height;

    [SerializeField] private RectTransform healthBar;

    public void Start()
    {
        SetMaxHealth(100);
        SetHealth(100.00f);
    }

    public void Update()
    {
        if (Reduction > 0)
        {
            SetHealth(0.25f * Reduction * Time.deltaTime * -1);
        }
    }

    public void SetMaxHealth(float maxHealth)
    {
        MaxHealth = maxHealth;
    }

    public void SetHealth(float health)
    {
        Health += health;
        float newWidth = (Health / MaxHealth) * Width;
        healthBar.sizeDelta = new Vector2(newWidth, Height);

        if (Health <= 0) {
            GameOver();
        }
    }

    void GameOver()
    {

    }
}
