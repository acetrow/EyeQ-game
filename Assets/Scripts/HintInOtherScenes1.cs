using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HintInOtherScenes1 : MonoBehaviour
{
    public TMP_Text hintText; 
    public GameObject confirmationWindow; 
    public GameObject skipConfirmationWindow; 
    public GameObject hintWindow; 
    public TMP_Text hintContentText; 

    public Button hintButton; 
    public Button yesButton; 
    public Button noButton; 
    public Button closeHintButton; 

    public Button skipButton; 
    public Button skipYesButton; 
    public Button skipNoButton; 

    public Button watchAdButton; 

    [TextArea(3, 5)] 
    public string sceneSpecificHint; 

    void Start()
    {
        
        if (HintManager1.Instance != null)
        {
            HintManager1.Instance.SetHintTextReference(hintText);

            HintManager1.Instance.hintText = hintText;
            HintManager1.Instance.confirmationWindow = confirmationWindow;
            HintManager1.Instance.hintWindow = hintWindow;
            HintManager1.Instance.hintContentText = hintContentText;
            HintManager1.Instance.skipConfirmationWindow = skipConfirmationWindow;

            HintManager1.Instance.customizableHint = sceneSpecificHint;

            hintButton.onClick.RemoveAllListeners();
            hintButton.onClick.AddListener(HintManager1.Instance.OnHintButtonClicked);

            yesButton.onClick.RemoveAllListeners();
            yesButton.onClick.AddListener(HintManager1.Instance.ConfirmUseHint);

            noButton.onClick.RemoveAllListeners();
            noButton.onClick.AddListener(HintManager1.Instance.CancelUseHint);

            closeHintButton.onClick.RemoveAllListeners();
            closeHintButton.onClick.AddListener(HintManager1.Instance.CloseHintWindow);

            skipButton.onClick.RemoveAllListeners();
            skipButton.onClick.AddListener(HintManager1.Instance.OnSkipButtonClicked);

            skipYesButton.onClick.RemoveAllListeners();
            skipYesButton.onClick.AddListener(HintManager1.Instance.ConfirmSkip);

            skipNoButton.onClick.RemoveAllListeners();
            skipNoButton.onClick.AddListener(HintManager1.Instance.CancelSkip);

            
            watchAdButton.onClick.RemoveAllListeners();
            watchAdButton.onClick.AddListener(HintManager1.Instance.ShowWatchAdPrompt);
        }
        else
        {
            Debug.LogError("HintManager1 instance not found!");
        }
    }
}
