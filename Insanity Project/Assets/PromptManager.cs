using UnityEngine;
using TMPro;

public class PromptManager : MonoBehaviour
{
    public TMP_Text promptText;
    public float promptIntervalMin = 20f;
    public float promptIntervalMax = 60f;
    public string promptMessage = "Do you accept my gift?";
    public float maxHPDecrease = 10f;
    public float screenShakeIntensity = 0.5f;

    private bool isPromptActive = false;

    void Start()
    {
        // Start prompting at random intervals
        InvokeRepeating("ShowPrompt", Random.Range(promptIntervalMin, promptIntervalMax), Random.Range(promptIntervalMin, promptIntervalMax));
    }

    void ShowPrompt()
    {
        // Display the prompt message
        promptText.text = promptMessage;
        isPromptActive = true;
    }

    void Update()
    {
        if (isPromptActive)
        {
            // Check for player input
            if (Input.GetKeyDown(KeyCode.Y))
            {
                AcceptGift();
            }
            else if (Input.GetKeyDown(KeyCode.N))
            {
                DeclineGift();
            }
        }
    }

    void AcceptGift()
    {
        // Decrease player's max HP and apply screen shake
        // For demonstration purposes, let's just log the action
        Debug.Log("Player accepted the gift!");
        
        // Apply effects
        // For demonstration purposes, let's just decrease max HP
        // and apply screen shake
        DecreaseMaxHP();
        ApplyScreenShake();
        
        // Reset prompt
        isPromptActive = false;
        promptText.text = "";
    }

    void DeclineGift()
    {
        // Apply alternative effects for declining the gift
        // For demonstration purposes, let's just log the action
        Debug.Log("Player declined the gift!");

        // Reset prompt
        isPromptActive = false;
        promptText.text = "";
    }

    void DecreaseMaxHP()
    {
        // Apply decrease to player's max HP
        // For demonstration purposes, let's just decrease max HP
        // by the specified amount
        // You should implement your own logic here
    }

    void ApplyScreenShake()
    {
        // Apply screen shake effect
        // For demonstration purposes, let's just log the action
        GetComponent<ScreenShake>().Shake();
    }
}
