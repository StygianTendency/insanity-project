using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private bool isAttacking = false;
    private Animator animator;
    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0) && !isAttacking) {
            Attack();
        } else if (Input.GetKeyDown(KeyCode.Q)){
            animator.SetTrigger("isAttack2");
        }
    }

    void Attack() {
        animator.SetTrigger("isAttack1");
        isAttacking = true;

    }

     private void EndAttackAnimation()
    {
        isAttacking = false; // Reset the flag when the attack animation is finished
        Debug.Log("Attack animation ended. isAttacking set to false.");
    }

    private void Awake() {
        rogueMovement = new RogueInputs();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
}
