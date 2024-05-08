using UnityEngine;
using TMPro;
using Cinemachine;
using System.Collections;

public class PromptManager : MonoBehaviour
{
    public TMP_Text promptText;
    public float promptIntervalMin = 20f;
    public float promptIntervalMax = 60f;
    public string promptMessage = "Do you accept my gift?";
    public float maxHPDecrease = 10f;

    private bool isPromptActive = false;

    // Reference to the Cinemachine Virtual Camera named "PlayerView"
    public CinemachineVirtualCamera playerViewCamera;

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
        // Decrease player's max HP
        // Apply other effects as needed
        // Reset prompt
        isPromptActive = false;
        promptText.text = "";

        // Trigger screen shake
        StartCoroutine(TriggerScreenShake());
    }

    void DeclineGift()
    {
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
}
