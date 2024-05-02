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
    private Rigidbody2D rb;
    private void Start(){
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update(){
        //get target
        if(!target){
            GetTarget();
        }else{
            RotateTowardsTarget();
        }
        //rotate to target

    }
    private void FixedUpdate(){
        //move forward
    }
    private void RotateTowardsTarget(){
        Vector2 targetDirection = target.position - transform.position;
        float angle =  Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90f;
        Quaternion q = Quaternion.Euler(new Vector3(0,0,angle));
        transform.localRotation = Quaternion.Slerp(transform.localRotation,q, rotateSpeed);
    }
    private void GetTarget(){
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
}
