using System.Collections;
using UnityEngine;

public class SimpleMovement : MonoBehaviour
{
    public float speed = 5f;
    Rigidbody existingRigidbody;
    bool canMove = true;  // Flag to control movement

    void Start()
    {
        StartCoroutine(StartMovingDelayed());

        // Check if Rigidbody component is already present
        existingRigidbody = gameObject.GetComponent<Rigidbody>();

        // Add Rigidbody to the player (assuming the player is moving) if not present
        if (gameObject.CompareTag("Player") && existingRigidbody == null)
        {
            Rigidbody playerRigidbody = gameObject.AddComponent<Rigidbody>();
            playerRigidbody.isKinematic = true;  // Set to true if the player should not be influenced by physics
            playerRigidbody.useGravity = false;  // Disable gravity for the player
        }
        // Add Rigidbody to other objects if not present
        else if ((gameObject.CompareTag("OrangeCube") || gameObject.CompareTag("BrownCube") || gameObject.CompareTag("GreenCapsule") || gameObject.CompareTag("PinkCube")) && existingRigidbody == null)
        {
            Rigidbody objectRigidbody = gameObject.AddComponent<Rigidbody>();
            objectRigidbody.isKinematic = false; // Set to false to allow collisions
            objectRigidbody.useGravity = false;  // Disable gravity for other objects
        }
    }

    IEnumerator StartMovingDelayed()
    {
        float delay = 0f;

        if (gameObject.CompareTag("Player"))
            delay = 2f;
        else if (gameObject.CompareTag("OrangeCube"))
            delay = 4f;
        else if (gameObject.CompareTag("BrownCube"))
            delay = 6f;
        else if (gameObject.CompareTag("GreenCapsule"))
            delay = 8f;
        else if (gameObject.CompareTag("PinkCube"))
            delay = 10f;

        yield return new WaitForSeconds(delay);

        // Add logic here to start movement after the delay
    }

    void Update()
    {
        Vector3 movement = Vector3.zero;

        if (existingRigidbody.position.y > 0.6f)
        {
            movement = new Vector3(existingRigidbody.position.x, 0.4f, existingRigidbody.position.z);
        }
        else if (existingRigidbody.position.y < 0.2f)
        {
            movement = new Vector3(existingRigidbody.position.x, 0.4f, existingRigidbody.position.z);
        }
        else if (canMove)  // Only move if canMove is true
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            movement = new Vector3(horizontalInput, 0f, verticalInput) * speed * Time.deltaTime;
        }

        transform.Translate(movement);
        CheckAndDestroyOffScreen();
    }

    void FixedUpdate()
    {
        // Freeze rotation to prevent the player from rotating
        if (existingRigidbody != null)
        {
            existingRigidbody.freezeRotation = true;
        }
    }

    void CheckAndDestroyOffScreen()
    {
        float threshold = 20f;

        if (transform.position.x > threshold || transform.position.x < -threshold ||
            transform.position.y > threshold || transform.position.y < -threshold ||
            transform.position.z > threshold || transform.position.z < -threshold)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))  
        {
            Destroy(collision.gameObject);  // Destroy the obstacle when colliding with it

            // If the colliding GameObject is the player, destroy it as well
            if (gameObject.CompareTag("Player"))
            {
                Destroy(gameObject);
            }

            canMove = false;  // Set canMove to false when colliding with an obstacle
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))  
        {
            canMove = true;  // Set canMove to true when no longer colliding with an obstacle
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            Debug.Log($"{gameObject.name} triggered a power-up!");

            // Activate the power-up effect
            StartCoroutine(ActivatePowerUp());

            // Destroy the power-up object
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Enemy"))
        {
            if (gameObject.CompareTag("OrangeCube") || gameObject.CompareTag("BrownCube") || gameObject.CompareTag("GreenCapsule") || gameObject.CompareTag("PinkCube"))
            {
                Debug.Log($"{gameObject.name} triggered with an enemy!");
                // Handle enemy trigger behavior here
            }
        }
    }

    IEnumerator ActivatePowerUp()
    {
        // Assuming the power-up effect is temporary, adjust the duration as needed
        float powerUpDuration = 5f;

        // Increase the speed during the power-up
        float originalSpeed = speed;
        speed *= 2f; // Double the speed (adjust as needed)

        // Wait for the power-up duration
        yield return new WaitForSeconds(powerUpDuration);

        // Restore the original speed after the power-up duration
        speed = originalSpeed;
    }
}
