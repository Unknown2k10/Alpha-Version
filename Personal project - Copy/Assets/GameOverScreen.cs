

using System.Collections;  // Import the namespace for collections
using System.Collections.Generic;  // Import the namespace for generic collections
using UnityEngine;  // Import the Unity engine namespace
using UnityEngine.UI;  // Import the Unity UI namespace

// Class definition for GameOverScreen, inherits from MonoBehaviour
public class GameOverScreen : MonoBehaviour
{
    // Public variable for the Text UI element displaying points
    public Text pointsText;

    // Public method to set up the GameOverScreen with a specified score
    public void Setup(int score)
    {
        // Activate the GameOverScreen game object
        gameObject.SetActive(true);

        // Set the text of the pointsText to display the provided score
        pointsText.text = score.ToString() + " POINTS";
    }
}
