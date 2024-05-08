using UnityEngine;
using System.Collections;

public class SkeletonMeleeAttack : MonoBehaviour
{
    public float attackRange = 12f; // Adjust as needed
    public int damageAmount = 10; // Amount of damage to deal to enemies
    public float attackDuration = 1f; // Duration of the attack in seconds
    private bool isAttacking = false; // Flag to indicate if the skeleton is currently attacking

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collided object is the player
        if (other.CompareTag("Player"))
        {
            Debug.Log("Skeleton detected player in range!");

            // Start the attack
            StartCoroutine(PerformAttack(other.gameObject));
        }
    }

    IEnumerator PerformAttack(GameObject player)
    {
        // Set the attacking flag to true
        isAttacking = true;

        // Perform the attack animation or any other attack-related actions here
        Debug.Log("Skeleton started attack animation!");

        // Perform raycast for the duration of the attack
        float timer = 0f;
        while (timer < attackDuration)
        {
            // Perform raycast
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, attackRange);

            if (hit.collider != null)
            {
                // Check if the object hit by the raycast is the player
                if (hit.collider.CompareTag("Player"))
                {
                    Debug.Log("Skeleton hit the player!");
                    // Call a method on the player to apply damage
                    hit.collider.GetComponent<HealthBar>().TakeDamage(damageAmount);
                } else {
                    Debug.Log("Raycast hit something, but it's not the player. Collider tag: " + hit.collider.tag);
                }
            }

            // Increment timer
            timer += Time.deltaTime;

            // Wait for the next frame
            yield return null;
        }

        // End the attack
        Debug.Log("Skeleton ended attack!");
        
        // Set the attacking flag to false
        isAttacking = false;
    }
}
