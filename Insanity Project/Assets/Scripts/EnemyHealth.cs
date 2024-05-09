using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100; // Maximum health of the enemy
    public int currentHealth; // Current health of the enemy

    void Start()
    {
        currentHealth = maxHealth; // Set initial health to maxHealth
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount; // Reduce current health by damageAmount

        if (currentHealth <= 0)
        {
            Die(); // If health drops to or below zero, call the Die method
        }
    }

    void Die()
    {
        // Perform death-related actions here, such as playing death animation or disabling GameObject
        Destroy(gameObject); // Destroy the enemy GameObject
    }
}
