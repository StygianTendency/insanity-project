using UnityEngine;
using TMPro;
using Cinemachine;
using System.Collections;

public class PromptManager : MonoBehaviour
{
    public TMP_Text promptText;
    public float promptIntervalMin = 20f;
    public float promptIntervalMax = 60f;
    public float maxHPDecrease = 10f;
    public int maxPrompts = 2; // Maximum number of prompts

    // Prompt messages based on player's previous choice
    private string[] acceptPrompts = {
        "Do you accept my gift?",
        "Do you truly have any choice in the matter?"
    };
    private string[] declinePrompts = {
        "Are you sure?",
        "Think carefully about your decision."
    };

    private bool isPromptActive = false;
    private bool isFirstPrompt = true; // Flag to track the first prompt
    private bool lastResponse = false; // true if player accepted last time, false if declined
    private int acceptedCount = 0; // Counter for the number of times the player accepted the gift

    // Reference to the Cinemachine Virtual Camera named "PlayerView"
    public CinemachineVirtualCamera playerViewCamera;

    void Start()
    {
        // Start prompting at random intervals
        InvokeRepeating("ShowPrompt", Random.Range(promptIntervalMin, promptIntervalMax), Random.Range(promptIntervalMin, promptIntervalMax));
    }

    void ShowPrompt()
    {
        // Check if the maximum number of prompts has been reached
        if (acceptedCount >= maxPrompts)
        {
            // Stop prompting
            CancelInvoke("ShowPrompt");
            return;
        }

        // Determine the prompt message
        string promptMessage = "";

        if (isFirstPrompt)
        {
            promptMessage = "Do you accept His gift?";
        }
        else if (lastResponse)
        {
            if (promptText.text == acceptPrompts[1])
            {
                promptMessage = "No, you don't.";
                StartCoroutine(AutoPressY());
            }
            else
            {
                promptMessage = acceptPrompts[1];
            }
        }
        else
        {
            if (promptText.text == declinePrompts[1])
            {
                promptMessage = "No, you don't.";
                StartCoroutine(AutoPressY());
            }
            else
            {
                promptMessage = declinePrompts[1];
            }
        }

        // Display the prompt message
        promptText.text = promptMessage;
        isPromptActive = true;

        // Reset isFirstPrompt flag after the first prompt
        isFirstPrompt = false;
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
    // Set last response to true (accepted)
    lastResponse = true;

    // Increase the accepted count
    acceptedCount++;

    // Access the MeleeAttack script to increase damage
    MeleeAttack meleeAttack = GetComponent<MeleeAttack>();
    if (meleeAttack != null)
    {
        // Increase damage by 20%
        Debug.Log(meleeAttack.damageAmount);
        meleeAttack.damageAmount = (int)(meleeAttack.damageAmount * 1.2f);
        Debug.Log(meleeAttack.damageAmount);

    }
    else
    {
        Debug.LogError("MeleeAttack script not found!");
    }

    // Access the HealthBar script to decrease health
    HealthBar healthBar = GetComponent<HealthBar>();
    if (healthBar != null)
    {
        // Decrease current health by 20%
        Debug.Log(healthBar.currentHealth);
        healthBar.currentHealth *= 0.8f;
        Debug.Log(healthBar.currentHealth);
    }
    else
    {
        Debug.LogError("HealthBar script not found!");
    }

    // Reset prompt
    isPromptActive = false;
    promptText.text = "";

    // Trigger screen shake
    StartCoroutine(TriggerScreenShake());
}

    void DeclineGift()
    {
        // Set last response to false (declined)
        lastResponse = false;

        // Apply alternative effects for declining the gift
        // Reset prompt
        isPromptActive = false;
        promptText.text = "";
    }

    IEnumerator TriggerScreenShake()
    {
        if (playerViewCamera != null)
        {
            // Get the noise profile from the Cinemachine Virtual Camera
            CinemachineBasicMultiChannelPerlin noiseProfile = playerViewCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

            // Enable the noise profile
            noiseProfile.m_AmplitudeGain = 5f;
            noiseProfile.m_FrequencyGain = 5f;

            // Wait for a short duration
            yield return new WaitForSeconds(1f);

            // Disable the noise profile after the shake
            noiseProfile.m_AmplitudeGain = 0f;
        }
        else
        {
            Debug.LogError("PlayerView Camera is not assigned!");
        }
    }

    IEnumerator AutoPressY()
    {
        // Wait for a short duration before pressing Y
        yield return new WaitForSeconds(1f);

        // Press Y automatically
        AcceptGift();
    }
}
