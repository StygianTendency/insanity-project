using UnityEngine;
using System.Collections;
public class MeleeAttack : MonoBehaviour
{
    public float attackRange = 100f; // Adjust as needed
    public int damageAmount = 5; // Amount of damage to deal to enemies
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
                SkeletonWarriorScript warrior = hit.collider.GetComponent<SkeletonWarriorScript>();
                SkeletonScript skeleton = hit.collider.GetComponent<SkeletonScript>();
                ArcherScript archer = hit.collider.GetComponent<ArcherScript>();
                BlobScript blob = hit.collider.GetComponent<BlobScript>();
                SkeletonSpearmanScript spearman = hit.collider.GetComponent<SkeletonSpearmanScript>();

                if (warrior != null)
                {
                    Debug.Log("Hit a skeleton warrior!");

                    // Access the maxHealth variable of the SkeletonWarriorScript
                    int maxHealth = warrior.maxHealth;

                    // Call a method on the warrior to apply damage
                    warrior.TakeDamage(damageAmount);
                }
                else if (skeleton != null)
                {
                    Debug.Log("Hit a skeleton!");

                    // Access the maxHealth variable of the SkeletonScript
                    int maxHealth = skeleton.maxHealth;

                    // Call a method on the skeleton to apply damage
                    skeleton.TakeDamage(damageAmount);
                }
                else if (archer != null)
                {
                    Debug.Log("Hit an archer!");

                    // Access the maxHealth variable of the ArcherScript
                    int maxHealth = archer.maxHealth;

                    // Call a method on the archer to apply damage
                    archer.TakeDamage(damageAmount);
                }
                else if (blob != null)
                {
                    Debug.Log("Hit a blob!");

                    // Access the maxHealth variable of the BlobScript
                    int maxHealth = blob.maxHealth;

                    // Call a method on the blob to apply damage
                    blob.TakeDamage(damageAmount);
                }
                else if (spearman != null)
                {
                    Debug.Log("Hit a skeleton spearman!");

                    // Access the maxHealth variable of the SkeletonSpearmanScript
                    int maxHealth = spearman.maxHealth;

                    // Call a method on the spearman to apply damage
                    spearman.TakeDamage(damageAmount);
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
