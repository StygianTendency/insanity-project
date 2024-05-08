using UnityEngine;
using System.Collections;
public class ScreenShake : MonoBehaviour
{
    public float shakeDuration = 0.1f;
    public float shakeIntensity = 0.1f;

    private Vector3 originalPosition;

    void Start()
    {
        // Store the original position of the camera
        originalPosition = transform.position;
    }

    public void Shake()
    {
        // Start the screen shake coroutine
        StartCoroutine(ShakeCoroutine());
    }

    IEnumerator ShakeCoroutine()
    {
        float elapsed = 0.0f;

        while (elapsed < shakeDuration)
        {
            // Generate random offsets for the camera position
            float xOffset = Random.Range(-1f, 1f) * shakeIntensity;
            float yOffset = Random.Range(-1f, 1f) * shakeIntensity;

            // Apply the offset to the camera's position
            transform.position = originalPosition + new Vector3(xOffset, yOffset, 0);

            // Increment the elapsed time
            elapsed += Time.deltaTime;

            // Wait for the next frame
            yield return null;
        }

        // Reset the camera's position to its original position
        transform.position = originalPosition;
    }
}

