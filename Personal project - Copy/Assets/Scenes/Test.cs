using System.Collections;  // Import the namespace for collections
using System.Collections.Generic;  // Import the namespace for generic collections
using UnityEngine;  // Import the Unity engine namespace

// Class definition for Test, inherits from MonoBehaviour
public class Test : MonoBehaviour
{
    // Reference to the Scoring script
    public Scoring score;

    // Called when a collider enters the trigger collider attached to this object
    void OnTriggerEnter(Collider other)
    {
        // Check if the entering collider's game object has the tag "PowerUp"
        if (other.gameObject.CompareTag("PowerUp"))
        {
            // Call the AddScore method from the Scoring script, incrementing the score by 1
            score.AddScore(1);
        }
    }
}
