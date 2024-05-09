using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonSpearmanScript : MonoBehaviour
{
    public float rotateSpeed = 0.0025f;
    public Transform target;
    public float speed = 2f;
    public float attackDistance = .5f;
    public Animator animator;
    public BoxCollider2D colliderBox; // Reference to the BoxCollider2D component
    private Vector2 originalColliderSize;
    private float originalXOffset; // Original X offset of the collider
    public int maxHealth = 600; // Maximum health of the enemy
    public int currentHealth; // Current health of the enemy

    public float attackRange = .1f; // Adjust as needed for proximity check
    public int damageAmount = 15; // Amount of damage to deal to player
    public float attackCooldown = .8f; // Cooldown between attacks
    private bool isAttacking = false; // Flag to indicate if the skeleton is currently attacking
    private GameObject player; // Reference to the player game object

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        colliderBox = GetComponent<BoxCollider2D>(); // Get the BoxCollider2D component
        GetTarget();
        originalColliderSize = colliderBox.size; // Store the original size of the collider
        originalXOffset = colliderBox.offset.x; // Store the original X offset of the collider
        currentHealth = maxHealth; // Set initial health to maxHealth

        // Find the player game object using its tag "Player"
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        //get target
        if (!target)
        {
            GetTarget();
            return;
        }
        FaceTarget();
        CheckAttackRange();

        // Check if the player is within attack range and the skeleton is not already attacking
        if (Vector2.Distance(transform.position, player.transform.position) <= attackRange && !isAttacking)
        {
            // Start the attack
            StartCoroutine(PerformAttack());
        }
    }

    private void FixedUpdate()
    {
        //move forward
        MoveTowardsTarget();
    }

    private void FaceTarget()
    {
        float positionTolerance = 0.1f;
        if (target.position.x < transform.position.x - positionTolerance)
        {
            spriteRenderer.flipX = true;
            AdjustColliderBox(true);
        }
        else if (target.position.x > transform.position.x + positionTolerance)
        {
            spriteRenderer.flipX = false;
            AdjustColliderBox(false);
        }
    }

    private void AdjustColliderBox(bool facingLeft)
    {
        if (colliderBox != null)
        {
            // Adjust offset based on facing direction
            float xOffset = facingLeft ? -originalXOffset : originalXOffset;
            colliderBox.offset = new Vector2(xOffset, colliderBox.offset.y);
        }
    }

    private void GetTarget()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            target = player.transform;
        }
    }

    private void MoveTowardsTarget()
    {
        if (target != null)
        {
            Vector2 direction = target.position - transform.position;
            rb.velocity = direction.normalized * speed;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void CheckAttackRange()
    {
        if (target != null){
            float distanceToTarget = Vector2.Distance(transform.position, target.position);
            if(distanceToTarget <= attackDistance){
                animator.SetBool("attackA",true);
            } else {
                animator.SetBool("attackA", false);
            }
        }
    }

    IEnumerator PerformAttack()
    {
        // Set the attacking flag to true
        isAttacking = true;

        // Deal damage to the player
        player.GetComponent<HealthBar>().TakeDamage(damageAmount);

        // Wait for the attack cooldown before allowing another attack
        yield return new WaitForSeconds(attackCooldown);

        // Set the attacking flag to false
        isAttacking = false;
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
