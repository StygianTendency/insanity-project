using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class enemyscript : MonoBehaviour
{
    public float rotateSpeed = 0.0025f;
    public Transform target;
    public float speed = 4f;
    public float attackDistance = .7f;
    public Animator animator;
    public BoxCollider2D colliderBox; // Reference to the BoxCollider2D component
    private Vector2 originalColliderSize; 
    private float originalXOffset; // Original X offset of the collider


    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private void Start(){
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();        
        colliderBox = GetComponent<BoxCollider2D>(); // Get the BoxCollider2D component
        GetTarget();
        originalColliderSize = colliderBox.size; // Store the original size of the collider
        originalXOffset = colliderBox.offset.x; // Store the original X offset of the collider
    }

    private void Update(){
        //get target
        if(!target){
            GetTarget();
            return;
        }
        FaceTarget();
        CheckAttackRange();
        
        //rotate to target

    }
    private void FixedUpdate(){
        //move forward
        // Vector2 direction = (target.position - transform.position).normalized;
        // rb.velocity = transform.up * speed;
        MoveTowardsTarget();
    }

    private void FaceTarget(){
        
        float positionTolerance = 0.1f;
        if(target.position.x < transform.position.x - positionTolerance){
            spriteRenderer.flipX = true;
            AdjustColliderBox(true);
        } else if (target.position.x > transform.position.x + positionTolerance){
            spriteRenderer.flipX = false;
            AdjustColliderBox(false);
        } else {
            
        }

    }

    private void AdjustColliderBox(bool facingLeft){
        if (colliderBox != null){
            // Adjust offset based on facing direction
            float xOffset = facingLeft ? -originalXOffset : originalXOffset;
            colliderBox.offset = new Vector2(xOffset, colliderBox.offset.y);
        }
    }
    
    // private void RotateTowardsTarget(){
    //     // Vector2 targetDirection = target.position - transform.position;
    //     // float angle =  Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90f;
    //     // transform.rotation = Quaternion.Euler(0,0,angle);
    //     // Quaternion q = Quaternion.Euler(new Vector3(0,0,angle));
    //     // transform.localRotation = Quaternion.Slerp(transform.localRotation,q, rotateSpeed);
    //     Vector2 direction = target.position - transform.position;
    //     float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
    //     Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
    //     transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
    // }

    private void GetTarget(){
        // if(GameObject.FindGameObjectWithTag("Player")){
        //     target = GameObject.FindGameObjectWithTag("Player").transform;
        // }
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            target = player.transform;
        }
    }

    private void MoveTowardsTarget(){
        // Vector2 direction = target.position - transform.position;
        // rb.velocity = direction.normalized * speed;
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

    private void CheckAttackRange(){
        if (target != null){
            float distanceToTarget = Vector2.Distance(transform.position, target.position);
            if(distanceToTarget <= attackDistance){
                animator.SetBool("attackA",true);
            } else {
                animator.SetBool("attackA", false);
            }
        }
    }
    
}
