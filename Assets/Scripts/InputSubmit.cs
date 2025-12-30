using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class InputSubmit : MonoBehaviour
{
    public TMP_InputField userInputField; 
    public GameObject submitImage;       
    public GameObject feedbackImage;     
    [SerializeField] private string correctAnswer = "1"; 

    private bool isFeedbackActive = false; 

    void Start()
    {
        
        userInputField.onSelect.AddListener(ActivateMobileKeyboard);
    }

   
    private void ActivateMobileKeyboard(string text)
    {
        TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
    }

    public void OnSubmit()
    {
        if (isFeedbackActive)
            return; 

        string userInput = userInputField.text;

        if (userInput == correctAnswer)
        {
            LoadNextScene();
        }
        else
        {
            StartCoroutine(ShowFeedback());
        }
    }

    private IEnumerator ShowFeedback()
    {
        isFeedbackActive = true;
        feedbackImage.SetActive(true);
        yield return new WaitForSeconds(1f);
        feedbackImage.SetActive(false);
        isFeedbackActive = false;
    }

    private void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
