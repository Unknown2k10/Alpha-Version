using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    public Text level1Label;
    public Text level2Label;

    public void LoadLevel1()
    {
        Debug.Log("LoadLevel1 function called");
        SceneManager.LoadScene("Level1");
    }

    public void LoadLevel2()
    {
        SceneManager.LoadScene("Level2");
    }

    // Add other functions as needed...

    // Example function to update a Text label
    public void UpdateLevel1Label(string newText)
    {
        if (level1Label != null)
        {
            level1Label.text = newText;
        }
    }
}
