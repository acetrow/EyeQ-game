using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class CounterControl : MonoBehaviour
{
    public TextMeshProUGUI counterText; 
    public GameObject image1; 
    public GameObject image2; 
    public GameObject submitImage; 

    private int counter = 0; 
    private const int minCounter = 0; // Minimum counter value
    private const int maxCounter = 30; // Maximum counter value
    private const int requiredCounterValue = 14; 

    void Start()
    {
        // Initialize the counter text
        UpdateCounterText();
    }

    public void OnImage1Press()
    {
        // Decrease the counter
        counter = Mathf.Max(counter - 1, minCounter);
        UpdateCounterText();
    }

    public void OnImage2Press()
    {
        // Increase the counter
        counter = Mathf.Min(counter + 1, maxCounter);
        UpdateCounterText();
    }

    public void OnSubmitPress()
    {
        // Check if the counter is at the required value
        if (counter == requiredCounterValue)
        {
            LoadNextScene();
        }
        else
        {
            Debug.Log("Your answer is wrong");
        }
    }

    private void UpdateCounterText()
    {

        counterText.text = counter.ToString();
    }

    private void LoadNextScene()
    {

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
