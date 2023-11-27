/* using UnityEngine;
using System.Collections; // Add this line

public class CubeController : MonoBehaviour
{
    public Transform playerTransform;
    public SpawnManager spawnManager;

    void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is with the player
        if (collision.gameObject.CompareTag("Player"))
        {
            // Handle the collision (e.g., play a sound, increase score, etc.)

            // Destroy the cube
            Destroy(gameObject);

            // Respawn the cube after a delay
            RespawnAfterDelay(2.0f); // Adjust the delay as needed
        }
    }

    void RespawnAfterDelay(float delay)
    {
        // Use a coroutine to respawn the cube after the specified delay
        StartCoroutine(RespawnCoroutine(delay));
    }

    IEnumerator RespawnCoroutine(float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Respawn the cube using the SpawnManager reference
        spawnManager.SpawnCube();
    }
}

*/