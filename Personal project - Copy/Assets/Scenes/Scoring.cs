using UnityEngine;  // Import the Unity engine namespace
using UnityEngine.UI;  // Import the Unity UI namespace

// Class definition for Scoring, inherits from MonoBehaviour
public class Scoring : MonoBehaviour
{
    // Public variables for score tracking
    public int score = 0;
    public Text scoreText;  // Reference to the Text UI element
    public int maxScore;

    // Public game objects for UI elements
    public GameObject Score;
    public GameObject YouText;

    // Called at the start of the script
    void Start()
    {
        // Initialize the score to zero at the start
        score = 0;
    }

    // Public method to add a specified score value
    public void AddScore(int newScore)
    {
        // Increment the score by the provided value
        score += newScore;

        // Update the score on the UI whenever it changes
        UpdateScore();
    }

    // Function to update the score on the UI
    public void UpdateScore()
    {
        // Set the scoreText to display the current score with leading zeros
        scoreText.text = "Score: " + score.ToString("D2");
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the current score equals the maximum score
        if (score == maxScore)
        {
            // Disable the Score object and enable the YouText object
            Score.SetActive(false);
            YouText.SetActive(true);
        }
    }
}
