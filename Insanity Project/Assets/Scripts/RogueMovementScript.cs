using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class RogueMovementScript : MonoBehaviour
{
    //Movement Speed of the character
    private float moveSpeed = 5f;
    //Input actiosn for character movement
    private RogueInputs rogueMovement;
    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator animator;

    //UI Text for [E] interact message
    public TextMeshProUGUI interactText;
    //Flag to indicate if the character is near a chest
    private bool isNearChest = false;
    private bool wasMoving = false;

    //Called when the script is first initialized
    private void Awake(){
        rogueMovement = new RogueInputs();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void OnEnable(){
        rogueMovement.Enable();
    }

    private void OnDisable(){
        rogueMovement.Disable();
    }

    // Start is called before the first frame update
    private void Start()
    {
        if(interactText != null){
            interactText.enabled = false;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        //Update movement input
        rogueInput();

        //Check if "E" is pressed and character is near a chest
        if(Input.GetKeyDown(KeyCode.E) && isNearChest){
            Debug.Log("Opening chest");
        }

        //Update position of [E] interact text if character is near chest
        if(isNearChest && interactText.enabled){
            Vector3 roguePosition = Camera.main.WorldToScreenPoint(transform.position);
            Vector3 textInteractPosition = roguePosition + new Vector3(0, -80.0f, 0);
            interactText.transform.position = textInteractPosition;
        }

        bool isMoving = Mathf.Abs(movement.x) > 0.1f || Mathf.Abs(movement.y) > 0.1f;

        // Update the animator parameters based on movement
        animator.SetBool("isMoving", isMoving);
        animator.SetBool("isNotMoving", !isMoving);

        if (movement.x < 0) // Moving left
        {
            transform.localScale = new Vector3(-5, 5, 0); // Flip horizontally
        }
        else if (movement.x > 0) // Moving right
        {
            transform.localScale = new Vector3(5, 5, 0); // Normal scale
        }

    }

    private void FixedUpdate(){
        rogueMove();

    }

    //Read movement input from controls
    private void rogueInput(){
        movement = rogueMovement.Move.Movement.ReadValue<Vector2>();
    }

    //Move character based on input
    private void rogueMove(){
        // rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));

        rb.velocity = movement * moveSpeed;
    }

    //Called when a collision occurs
    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.CompareTag("Chest")){
            isNearChest = true;
            ShowInteractMessage(true);
        }
    }

    //Called when collision with an game object ends. In this case I only had a chest game object.
    private void OnCollisionExit2D(Collision2D collision){
        if(collision.gameObject.CompareTag("Chest")){
            isNearChest = false;
            ShowInteractMessage(false);
        }
    }
    
    //Show or hide [E] interact message based on parameter
    private void ShowInteractMessage(bool show){
        if(interactText != null){
            interactText.enabled = show;
        }
    }
}
