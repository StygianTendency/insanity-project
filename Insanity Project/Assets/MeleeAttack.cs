using UnityEngine;
using System.Collections;
public class MeleeAttack : MonoBehaviour
{
    public float attackRange = 12f; // Adjust as needed
    public int damageAmount = 100; // Amount of damage to deal to enemies
    public float attackDuration = 1f; // Duration of the attack in seconds
    private bool isAttacking = false; // Flag to indicate if the player is currently attacking

    void Update()
    {
        // Check if the player clicks the left mouse button and is not already attacking
        if (Input.GetMouseButtonDown(0) && !isAttacking) // "Fire1" corresponds to left mouse button
        {
            Debug.Log("Left mouse button clicked!");

            // Start the attack
            StartCoroutine(PerformAttack());
        }
    }

    IEnumerator PerformAttack()
    {
        // Set the attacking flag to true
        isAttacking = true;

        // Perform the attack animation or any other attack-related actions here
        Debug.Log("Started attack animation!");

        // Perform raycast for the duration of the attack
        float timer = 0f;
        while (timer < attackDuration)
        {
            // Perform raycast
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, attackRange);

            if (hit.collider != null)
            {
                // Check if the object hit by the raycast is an enemy
                if (hit.collider.CompareTag("Enemy"))
                {
                    Debug.Log("Hit an enemy!");
                    // Call a method on the enemy to apply damage
                    hit.collider.GetComponent<EnemyHealth>().TakeDamage(damageAmount);
                }
            }

            // Increment timer
            timer += Time.deltaTime;

            // Wait for the next frame
            yield return null;
        }

        // End the attack
        Debug.Log("Ended attack!");
        
        // Set the attacking flag to false
        isAttacking = false;
    }
}
