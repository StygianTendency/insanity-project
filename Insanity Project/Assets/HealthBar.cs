using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthBar;
    public float maxHealth = 100f; // Set your rogue's max health here
    private float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Return)){
            TakeDamage(20);
        }
        if(Input.GetKeyDown(KeyCode.Space)){
            Heal(5);
        }
    }

    public void SetHealth(float health)
    {
        currentHealth = health;
        UpdateHealthBar();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        UpdateHealthBar();
    }

    public void Heal(float healingAmount){
        currentHealth += healingAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        healthBar.fillAmount = currentHealth / maxHealth;
    }
}
