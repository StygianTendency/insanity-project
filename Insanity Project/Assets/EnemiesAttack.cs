using UnityEngine;
using System.Collections;

public class EnemiesAttack : MonoBehaviour
{
    public float attackRange = .5f; // Adjust as needed for proximity check
    public int damageAmount = 20; // Amount of damage to deal to player
    public float attackCooldown = .9f; // Cooldown between attacks
    private bool isAttacking = false; // Flag to indicate if the skeleton is currently attacking
    private GameObject player; // Reference to the player game object

    void Start()
    {
        // Find the player game object using its tag "Player"
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        // Check if the player is within attack range and the skeleton is not already attacking
        if (Vector2.Distance(transform.position, player.transform.position) <= attackRange && !isAttacking)
        {
            // Start the attack
            StartCoroutine(PerformAttack());
        }
    }

    IEnumerator PerformAttack()
    {
        // Set the attacking flag to true
        isAttacking = true;

        // Deal damage to the player
        Debug.Log("Skeleton is attacking!");
        player.GetComponent<HealthBar>().TakeDamage(damageAmount);

        // Wait for the attack cooldown before allowing another attack
        yield return new WaitForSeconds(attackCooldown);

        // Set the attacking flag to false
        isAttacking = false;
    }
}
