using System.Collections;  // Import the namespace for Coroutine support
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool hasPowerup = false;  // Flag indicating whether the player has a power-up
    public GameObject powerupIndicator;  // Reference to the power-up indicator object
    private float powerupStrength = 15.0f;  // Strength of the power-up effect
    private float playerSpeed = 5.0f;  // Speed of the player's movement
    private float boundary = 10.0f;  // Boundary to constrain player movement - adjust as needed

    // Reference to the scoring system
    public Scoring scoring;

    void Start()
    {
        // Initialize any necessary setup here
    }

    // Coroutine for power-up countdown
    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);  // Wait for 7 seconds
        hasPowerup = false;  // Disable the power-up effect after the countdown
        if (powerupIndicator != null)
        {
            powerupIndicator.SetActive(false);  // Deactivate the power-up indicator object
        }
    }

    // Called when player collides with a trigger collider
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            // Activate power-up
            hasPowerup = true;
            Destroy(other.gameObject);  // Destroy the collected power-up object
            if (powerupIndicator != null)
            {
                powerupIndicator.SetActive(true);  // Activate the power-up indicator object
            }

            // Start power-up countdown coroutine
            StartCoroutine(PowerupCountdownRoutine());

            // Add score when player collects a power-up
            scoring.AddScore(1);
        }
    }

    // Called when player collides with a physical object
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            // Apply force to enemy when player has a power-up
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position).normalized;
            enemyRigidbody.AddForce(awayFromPlayer * 10 * powerupStrength, ForceMode.Impulse);
        }
    }

    // Move the player based on input
    void MovePlayer()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput) * playerSpeed * Time.deltaTime;
        transform.Translate(movement);  // Move the player
    }

    // Constrain the player's position within specified boundaries
    void ConstrainPlayerPosition()
    {
        float aspectRatio = 16f / 9f;  // Aspect ratio of 16:9
        float horizontalBoundary = 5.0f * aspectRatio;  // Adjust the boundary as needed for a larger clamping area on both sides
        float verticalBoundarySouth = 5.0f;  // Adjust the boundary as needed for the south side
        float verticalBoundaryNorth = 3.5f;  // Adjust the boundary as needed for the north side
        float constrainedXPosition = 0.0f;  // Desired x-axis position
        float maxYPosition = 10.0f;  // Maximum y-axis position (top wall)

        Vector3 clampedPosition = transform.position;

        // Apply x-axis clamping on both sides
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, constrainedXPosition - horizontalBoundary, constrainedXPosition + horizontalBoundary);

        // Apply z-axis clamping for the south side
        clampedPosition.z = Mathf.Clamp(clampedPosition.z, -verticalBoundarySouth, verticalBoundaryNorth);

        // Apply y-axis clamping (top wall)
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, 0f, maxYPosition);

        transform.position = clampedPosition;  // Update the player's position
    }

    void Update()
    {
        // Update the position of the power-up indicator relative to the player
        if (powerupIndicator != null)
        {
            powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
        }

        // Call movement and position constraint methods
        MovePlayer();
        ConstrainPlayerPosition();
    }
}
