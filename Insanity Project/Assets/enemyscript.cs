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
    public float speed = 3f;
    public float attackDistance = .7f;
    public Animator animator;


    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private void Start(){
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        GetTarget();
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
        } else if (target.position.x > transform.position.x + positionTolerance){
            spriteRenderer.flipX = false;
        } else {

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
