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
        "D̵͕̥͔͝ͅo̷̹̩͈̤͙͇͋̑͛͂ͅ ̶̩̦̬̘̳̰̞̰̃̽̇͆̈́̐͂͗̒̃̕y̸̨̱͎̠̩̺̞̖̲͇̌͒̌̌̐̈́̒͑̓̽̿̎͘̚̚͝ǒ̶̢̨̖̼̱̦̤̦͇̙̔ͅụ̸́̇̕ ̵̹̂̓̑̌̔̇̕͜ͅā̸̗͙̌͂̈́̂̅̈͗́̔͂̕͝͝ç̶̞͕̥͗͋͆̾̆͒̋͝͠ͅc̶̢͓̫̱̹̤͇̪͍̝̈́̈́̒̅̀̾̐̕ę̵̱̗̭̝̹̬̭̰̆̀́̀̍́̿̈́͜͝p̸̥͔̲̍t̶̞͚̜͚̘̎͌͆́́́́͐͑͗̈́͘̚̕͝͠ ̴̨̛͕͈͎̹̮̗͉͎̰̞̦̩̀̈́͒̂́̊́̀̉͘Ḩ̷͔̞̤̼̙̃į̷̛̛̤͔͈̻̜̥͈͔̘͓̙͇͔̺͚̳͛͗̈́̑̐̃̈̽̆̓̚͠s̵̤̪̱̞͇̥̗̆̆͊̐͋̕ ̶̡͖̺̤̭̻̦͖͚̗̋̽̊͋̓̑̈́̌̽́̈̕̕͜g̶̢̜̟͉͇̈͐͆͐̐̎̐̓͋͒́̑̽͌í̶̺̬̼̪̩̪͓̩̫̝̣̮͈͓̹̰̈́͒̌͑́̽̒̒͒̔͠ͅf̵̛͔̞͑̀̊̎͗̎̽́̐̒̚͜͝ͅt̵̢̧̡̡̻̖̰͙͍̙̺͖͇͓͎̘͚͂̾̉̍̍͌̌̈̈͐͒͊͝?̴̳̗͙͓̥̼̼̰͑͆̿͌͒̍̚̕̚͝͠ͅ",
        "Ḑ̵̢̗̜͎͉̭̳̤̥̰̖̤̗͚͕̓̓ọ̶̡̨̹̫̞͛̽̽̀̄ ̵̨͍̳̩̲̫̖̟̲̭͔̙̠̻̰̅͛̈́́̇͐͗̈́͑̈͜ͅẙ̴̛̩̼̯͉̗̺͂̾͆̿̽́̇́͝o̸̢̻̰͙͋̈́̉͋͝ú̷̧̨͇͕̬͉̙̺̬̭̐̓̃̀̒͆̂̓͂̀̃́͒͘͝ ̴̢̡͓̗̞͉̲̤̘͖̼̀͋t̷̨͈̝͖͙̞̮̰͚̠̹̠̟̭̮̂̏̋͒̇̉̾̋̉̀́̕ŗ̶̖̳̲̮͙̬̱̓̾͘ū̸͕̺͓͚̹̲̟̞̗͚̦̅̈́̈́̅̐͠l̷̢̨̤͈̱͚̱͙͓̟̪̬̹̜̜̑̚͜y̵͇̏̆̅̈́̽̾̑̌͒̄̀̚͝ ̷̹̱̻̦̊́̎͒̐͂̈́̅͛͌͑̇̏͠͝h̷̨̗̬̟̰̦͂̂͐̆͆̄̅̉̓̿̐̒̃͛͘͠a̸̢̡̟͚̥̻͓͙̞̗̍͂̏͒̈̓̈́̈́̂̃́́͝͝v̸̰̭̪̍e̴̡̛̛̗̯̣͙̰̝͖͌̿̑̄́ ̷̨̨̨̨̨͖̻͙̜̬͙͙̼̻̟̬̎̋͑̌͂̄̔̊̄͋̍ͅa̵̧̤̼̦̟̱̘̟̭̦͒̽͂̑̾̿́̽͆̀̍̃̇̏͘̕ǹ̴̛̻̟͖͙̺̼̼̙̬͉̠̼͎̟̟̀̋͗̌̾̔̊̚̚͝y̷̥̩̟̭͇̮͉͆̅͂̍̀ ̵͖̬͚̹͔̱͍͇͈̽̋͘ç̵̊̐̊̈́̎͌̒̂̐̉̃ȟ̴̟̻͖̩̼̙̎̾͘ȍ̴̧̦͇̗̼̺̞͘ī̵̢̡̩̪̳̲͍̳͙̜̦̯̲̺͊̕͝ͅc̷̡̛͚̦̻̩̬͌̾͑́̒̏͂̌̀̿̅̅͘͠ẻ̵̛̻̦͉͚̼̤̟̱̞̬̤͚̰͊̐̊̓̐͒̄͐̾̍̔̚͘̕͜͜͠ ̴͚̑̑̉̄i̵̼͓̝͕͋̏̉̉́̂͘ň̸͕͖̰̼́̒͑̽͑̀̀̊̋͂̾͛̕ͅ ̴̛̺̩́̏̌̎̊̿̂̚t̶̞͙̣̠̺͈͋͒͊̄̒̔̈́̄̿̕͠h̸̛̜̱̙̬̭̼͋͊͆̏̀̒͑́ȩ̷͈̦̳͇̹̖̱͓͓̼̖̤͙̙̣̗̎̊̋ ̷̛̛̙̬̩̗̣̠̮̖͔̙̻̖͓͚̖͖͊̔́̌͊̾͘m̶̡̨̛̪̤̦͍̬̙̥̟͓̬̬̰̹̾͊̀̅͐̈́̽͠a̶̰͈̤͂͐̕t̵̙̝̭̱̮̮̰͈̖͗̅͛̈́̋̀́͜͜͠t̸̡̲̼̣̩͆̄̔̅̒͗̌̄̀̊̈̇͑̕͜ĕ̴͔͎̙̳͚̱̼̭̂͋͑̎͌̌̀̈́̄̒͗̓̅̚͜ͅͅͅr̷̢̠̗̜̬̥̗̮̤̠͛͆̌̄͋͌́̽̎͆̿̈͆̎̚͝͝?̴̠̜͍̳̜̰̀͊̌͆́̾̚"
    };
    private string[] declinePrompts = {
        "A̵̡̮̖͉̰͔̎̄͊̌̇̄̓͘͜ͅͅŗ̷̹̰͙̜̹͌̀̈́̒̈́͛̍̓̈́̑̂͑̚̚͠e̵͕̫̙̜͙͌̎̒̒̆̍̽ ̵̢͕͎͙̲̪̍̀̀͒̀̂ỹ̷̨͇̝̪̘͎͔̦̥̹͓̒͂́̈͛̒̊͊̓͐͐̌̓̀̅ơ̴̡̧̫͎͚̰̬̬̹̱̖̓̂̄́́͗͂͒̒͂̄̾̒͝͝ͅu̷̡̨̙̩̙̝̟̘̣̪͔̱͑̏̽͛͒̎̀͝͠ ̶̼̯̘̺̹̳͙̎̒͐̃̇̆̐ͅs̸͓̖̝̻͚̪͍̏͑͋̋̄͒̆͋͗̍̓͗͑͠u̴̧̡̧̟̠̙̝͈̜̞͎͎͕̰͔͓̙͌̑r̶̡̛̗̼͈̙̣͓̟̙̳͓̺͓̝̤̮̉̊͆̽͗͋̈́̈͝ͅe̴̮͖̫͓͓̩̞̥̙̅̍͆̿̆̈͋̆̈͌̔̑̈́̕̚͝?̵͓̞͖̫̙̜̟̪̙̲̙̟͇̜́͂̈́̀́͘",
        "T̸̪̭̤̫̳̫̙͎̯͈̦͈̻̓͑̅̔̌̚͝͠͝ͅh̷̦̼̰̼͚̉̉̋̾̿̋̍i̴̻͙͎̙̝͑̀̈͑͐͘͘͝͝n̴̢͚͎̙̬̦͎̖̙͖̣̼̜͍͚͓̽̏͑͒̍̅̈̓̊͂̉̈́̽͐̃̚͝k̸̳͉̝̮̥̯͙̀́̓́̋̒́͆ ̶̨̠̻̗͎͎̣̳̠̤͒̄̂͋͗́̀̀̑̈͛́̄̚̕͘͠c̸̨̯̘̣̪̋̐̃̋̀̏â̴̛̠͂̾̀̾̚r̵͖̳͕̻͍̱̤̳̗̂͗̿̈́͐̌͜͜ͅe̸͙̐̽̀́̍̑͂͛̄̀̓̑̓͂̕͝͝f̶̡̡̦͈̳̖̱̝̈́̒̋́͌̔̓̑͗̅̍͌̚ṵ̸̝̺̠̲͇͙̮͖̰̩̯͚͔͚̆͆͊͛̈̓̔̌̓͝ͅl̴̦̪͈̹̜͇̍̽̄͊̌̏̄͛̈́̕ļ̴̜͙̺̱̠̩̝̣̣̱̘̲̫͌̒̈́͒̑͒͗̋̄͠ý̶̧̫̝͖̠̰̘͖͇͚͇̝̻̃̾ͅ ̶̛̖̬̺̠̫͈̰͓̳̾̉͗à̶̧̠͍̘͉̭͉́̇͛́͗̈́̎̏̚͜͜͠b̴̤̟̩̥̓̃͂̐̚o̷̢̱̻̼͉͉̰̙͔̖̝̾̃̽̉̇͋́͆͋͘̚͠ͅu̴̧͕̠̞͈͉̲̤̙̣̦̍̍̽͊́͛̓͊͘̚͝t̵͎̺̗̟̻̳̬͖͚̺̯̠̔̽̆̎̈́̈́̓̇͋̔͒̍̚͝ ̶̧̩̬̳̖͇̠̞̖͍͙͕͙͔̥̞͊͊́̋̍̒̇̕͝ͅy̷̨̨̬̭̮̮͉̦͕̞͇͆͗̓͒̒̈́̕o̸̢̡̞̣̘̟̥̭͐̋͊̔̽͌̔̊̅̔͑̃̌́̕͠͝u̶̧̙̻͓̝̙̼͔̞͉̖͎̞̫̫̇̅̀͋̉̒r̷̳͙͇͖̮̂͂ ̷͇͕͋́̅̽̽͆͆̆̇̚̚d̸̛̻̑̽͊̍̑̉͂̚e̸͈̥̊̈́̈́̆̈́͆́̽̒̅c̶̨͙̻̼̖̻̆͒͒̊̈́͂͝i̸̱̝̤̲̹̝͕̞͓͕̅͗̈́͊͆̉ͅś̶̲͍̟̥̦ị̶̲̤̻̍̓́̌̚ǒ̵̢̗͔̱̮̦̝̯̳͊̄͆̂͘n̵̘̜̮̠̞͍̙̘̭͚̐̄͐́̿̈́̀̊͘̕͝.̷̛̤͉̯̮̰̝͇̪͉̦͓̤̅̈́̓̈͐͑̈́̃́̀̂͜͝"
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
            promptMessage = "D̵͕̥͔͝ͅo̷̹̩͈̤͙͇͋̑͛͂ͅ ̶̩̦̬̘̳̰̞̰̃̽̇͆̈́̐͂͗̒̃̕y̸̨̱͎̠̩̺̞̖̲͇̌͒̌̌̐̈́̒͑̓̽̿̎͘̚̚͝ǒ̶̢̨̖̼̱̦̤̦͇̙̔ͅụ̸́̇̕ ̵̹̂̓̑̌̔̇̕͜ͅā̸̗͙̌͂̈́̂̅̈͗́̔͂̕͝͝ç̶̞͕̥͗͋͆̾̆͒̋͝͠ͅc̶̢͓̫̱̹̤͇̪͍̝̈́̈́̒̅̀̾̐̕ę̵̱̗̭̝̹̬̭̰̆̀́̀̍́̿̈́͜͝p̸̥͔̲̍t̶̞͚̜͚̘̎͌͆́́́́͐͑͗̈́͘̚̕͝͠ ̴̨̛͕͈͎̹̮̗͉͎̰̞̦̩̀̈́͒̂́̊́̀̉͘Ḩ̷͔̞̤̼̙̃į̷̛̛̤͔͈̻̜̥͈͔̘͓̙͇͔̺͚̳͛͗̈́̑̐̃̈̽̆̓̚͠s̵̤̪̱̞͇̥̗̆̆͊̐͋̕ ̶̡͖̺̤̭̻̦͖͚̗̋̽̊͋̓̑̈́̌̽́̈̕̕͜g̶̢̜̟͉͇̈͐͆͐̐̎̐̓͋͒́̑̽͌í̶̺̬̼̪̩̪͓̩̫̝̣̮͈͓̹̰̈́͒̌͑́̽̒̒͒̔͠ͅf̵̛͔̞͑̀̊̎͗̎̽́̐̒̚͜͝ͅt̵̢̧̡̡̻̖̰͙͍̙̺͖͇͓͎̘͚͂̾̉̍̍͌̌̈̈͐͒͊͝?̴̳̗͙͓̥̼̼̰͑͆̿͌͒̍̚̕̚͝͠ͅ";
        }
        else if (lastResponse)
        {
            if (promptText.text == acceptPrompts[1])
            {
                promptMessage = "N̷̡̢͓̹̫̻̋͋̎̀̈́͒̈́͊̈́̇̍͛̊͝o̷̧̡̲̱̳̥̣̼̤̣͙̽̀̇͌͑̅͂̎͛̽͒͜͜ͅͅ,̶̲͈̱̗̰̥̹͍͚̭̃̅̐͊͑̋̓ ̶̡̨̛̲͍̘͎̦̫͕̰͈̊̈́̎̈̿͐̀͌́̎̂̉̔y̷̛̬̖̜̺͕͎̹̩̗͍̪̰̠̦̌̓̄́͌̓̀̿̋̓̿̔̕̚o̷̢̭͕͓͍̦͖͋̏̾̾̈́̏̑̓̏ͅṵ̵͓͉̹̙̯̝̯̮̈́̓́̎̆̀̂̅̇̈́̂͠ͅ ̵̯͚̭̳̝̰̞̘̋̈́̈́͜ḑ̴̨̛͍̝̪̯̪̟̞͈͇̻̟͈̘̅̏̓̎̉̿̃́͐̚̕͝ǫ̶̛̳̳̼͇͙̀̂̀̀͌n̶͖͚̗̥͕̥͕̓̈̿̊̏̾'̸̢̖͈̯̦̺̘̰̤̑̈́͂͊̒̔t̸͍̜̪̹̙̳͇̫͖͚̬̕.̵̡̛̹͍̮͚͈̘̰̻̟̜̱͓͒̎͐͗̽̀͜ͅ";
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
                promptMessage = "N̷̡̢͓̹̫̻̋͋̎̀̈́͒̈́͊̈́̇̍͛̊͝o̷̧̡̲̱̳̥̣̼̤̣͙̽̀̇͌͑̅͂̎͛̽͒͜͜ͅͅ,̶̲͈̱̗̰̥̹͍͚̭̃̅̐͊͑̋̓ ̶̡̨̛̲͍̘͎̦̫͕̰͈̊̈́̎̈̿͐̀͌́̎̂̉̔y̷̛̬̖̜̺͕͎̹̩̗͍̪̰̠̦̌̓̄́͌̓̀̿̋̓̿̔̕̚o̷̢̭͕͓͍̦͖͋̏̾̾̈́̏̑̓̏ͅṵ̵͓͉̹̙̯̝̯̮̈́̓́̎̆̀̂̅̇̈́̂͠ͅ ̵̯͚̭̳̝̰̞̘̋̈́̈́͜ḑ̴̨̛͍̝̪̯̪̟̞͈͇̻̟͈̘̅̏̓̎̉̿̃́͐̚̕͝ǫ̶̛̳̳̼͇͙̀̂̀̀͌n̶͖͚̗̥͕̥͕̓̈̿̊̏̾'̸̢̖͈̯̦̺̘̰̤̑̈́͂͊̒̔t̸͍̜̪̹̙̳͇̫͖͚̬̕.̵̡̛̹͍̮͚͈̘̰̻̟̜̱͓͒̎͐͗̽̀͜ͅ";
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
        if(acceptedCount == 1) {

        
        Debug.Log(meleeAttack.damageAmount);
        meleeAttack.setDamage(1000);
        Debug.Log(meleeAttack.damageAmount);
        }
        if(acceptedCount == 2) {
            meleeAttack.setDamage(10000);
            Debug.Log(meleeAttack.damageAmount);
        }
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
        healthBar.TakeDamage(20);
        // healthBar.currentHealth - 20;
        
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
