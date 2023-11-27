using UnityEngine;
using UnityEngine.UI;

public class GameManager1 : MonoBehaviour
{
    private int score = 0;
    public Text scoreText;  // Reference to the Text UI element

    void Start()
    {
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    // Function to increase the score
    public void IncreaseScore(int amount)
    {
        score += amount;
        UpdateScoreText();
    }

    // Called when the player collides with another collider
    void OnCollisionEnter(Collision collision)
    {
        // Check if the colliding object has a specific tag (you can adjust this based on your game)
        if (collision.gameObject.CompareTag("Collectible"))
        {
            // You can change the amount based on your game's design
            IncreaseScore(1);

            // Optionally, destroy the collectible or perform other actions
            Destroy(collision.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Add any additional update logic if needed
    }
}
