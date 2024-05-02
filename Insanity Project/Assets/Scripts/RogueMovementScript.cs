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

    //UI Text for [E] interact message
    public TextMeshProUGUI interactText;
    //Flag to indicate if the character is near a chest
    private bool isNearChest = false;

    //Called when the script is first initialized
    private void Awake(){
        rogueMovement = new RogueInputs();
        rb = GetComponent<Rigidbody2D>();
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
    }

    private void FixedUpdate(){
        rogueMove();
    }

    //Read movement input from controls
    private void rogueInput(){
        Vector2 movementInput = rogueMovement.Move.@Movement.ReadValue<Vector2>();
        
        movement = new Vector2(movementInput.x, movementInput.y);

    }

    //Move character based on input
    private void rogueMove(){
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
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
        if (collision.tag == "Enemy") {
           // other.GetComponent<Enemy.().TakeDamage(damage);
           Debug.Log("Enemy hit");
        }
    }
    
    //Show or hide [E] interact message based on parameter
    private void ShowInteractMessage(bool show){
        if(interactText != null){
            interactText.enabled = show;
        }
    }

    
}
