using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private bool isAttacking = false;
    private Rigidbody2D rb;
    private Animator animator;

    public float rollSpeed = 4f;
    public float rollDuration = 1f;
    private float rollTimer = 0f;
    public bool isRolling = false;

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0) && !isAttacking) {
            Attack();
        } else if (Input.GetKeyDown(KeyCode.Q)){
            animator.SetTrigger("isAttack2");
        } else if (Input.GetKeyDown(KeyCode.LeftShift) && !isRolling){
             if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
            {
                animator.SetTrigger("isRoll");
                isRolling = true;
            } else {
                animator.SetTrigger("isRoll");
                isRolling = true;
            }
            
        }
    }

    private void FixedUpdate(){
        if(isRolling){
            HandleRoll();
        }

    }

    void Attack() {
        animator.SetTrigger("isAttack1");
        isAttacking = true;
    }

     private void EndAttackAnimation()
    {
        isAttacking = false; // Reset the flag when the attack animation is finished
        // Debug.Log("Attack animation ended. isAttacking set to false.");
    }

    private void Awake() {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    

    private void HandleRoll(){
    // Determine the roll direction based on the character's facing direction
    Vector2 rollDirection = transform.right; // Roll to the right by default

    // Check if the character is facing left, then roll to the left
    if (transform.localScale.x < 0)
    {
        rollDirection = -transform.right;
    }

    // Calculate the forward movement based on the roll direction
    Vector2 forwardMovement = rollDirection * rollSpeed * Time.deltaTime;

    // Move the player forward slightly
    transform.position += (Vector3)forwardMovement + Vector3.up * rb.velocity.y;

    rollTimer += Time.deltaTime;

    Debug.Log("ForwardMovement : " + forwardMovement);
    Debug.Log("RollTimer : " + rollTimer);

    }

    private void EndRollAnimation(){
        isRolling = false;
        rollTimer = 0f; // Reset roll timer
    }


}
