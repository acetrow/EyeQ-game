using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; 

public class HintManager : MonoBehaviour
{
    public static HintManager Instance; 

    public int hintsAvailable = 3; 
    public TMP_Text hintText; 
    public GameObject confirmationWindow; 
    public GameObject skipConfirmationWindow; 
    public GameObject hintWindow; 
    public TMP_Text hintContentText; 
    public string customizableHint = "This is your hint!"; 

    void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    void Start()
    {
        UpdateHintText(); 
    }

    public void OnHintButtonClicked()
    {
        if (hintsAvailable > 0)
        {
            confirmationWindow.SetActive(true); 
        }
        else
        {
            Debug.Log("No hints available!");
        }
    }

    public void ConfirmUseHint()
    {
        hintsAvailable--; 
        UpdateHintText();
        confirmationWindow.SetActive(false); 

        
        hintContentText.text = customizableHint;
        hintWindow.SetActive(true);
    }

    public void CancelUseHint()
    {
        confirmationWindow.SetActive(false); 
    }

    public void CloseHintWindow()
    {
        hintWindow.SetActive(false); 
    }


    public void OnSkipButtonClicked()
    {
        if (hintsAvailable >= 2)
        {
            skipConfirmationWindow.SetActive(true); 
        }
        else
        {
            Debug.Log("Not enough hints to skip the level!");
        }
    }

    public void ConfirmSkip()
    {
        hintsAvailable -= 2; 
        UpdateHintText();
        skipConfirmationWindow.SetActive(false); 
        LoadNextScene(); 
    }

    public void CancelSkip()
    {
        skipConfirmationWindow.SetActive(false); 
    }

    private void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    private void UpdateHintText()
    {
        if (hintText != null)
        {
            hintText.text = "Hints: " + hintsAvailable;
        }
    }

    public void SetHintTextReference(TMP_Text newHintText)
    {
        hintText = newHintText;
        UpdateHintText();
    }
}
