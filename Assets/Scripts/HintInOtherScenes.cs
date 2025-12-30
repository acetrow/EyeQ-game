using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HintInOtherScenes : MonoBehaviour
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

    [TextArea(3, 5)]
    public string sceneSpecificHint; 

    void Start()
    {
      
        if (HintManager.Instance != null)
        {
          
            HintManager.Instance.SetHintTextReference(hintText);

         
            HintManager.Instance.hintText = hintText;
            HintManager.Instance.confirmationWindow = confirmationWindow;
            HintManager.Instance.hintWindow = hintWindow;
            HintManager.Instance.hintContentText = hintContentText;
            HintManager.Instance.skipConfirmationWindow = skipConfirmationWindow;

      
            HintManager.Instance.customizableHint = sceneSpecificHint;

         
            hintButton.onClick.RemoveAllListeners();
            hintButton.onClick.AddListener(HintManager.Instance.OnHintButtonClicked);

     
            yesButton.onClick.RemoveAllListeners();
            yesButton.onClick.AddListener(HintManager.Instance.ConfirmUseHint);

    
            noButton.onClick.RemoveAllListeners();
            noButton.onClick.AddListener(HintManager.Instance.CancelUseHint);

   
            closeHintButton.onClick.RemoveAllListeners();
            closeHintButton.onClick.AddListener(HintManager.Instance.CloseHintWindow);


            skipButton.onClick.RemoveAllListeners();
            skipButton.onClick.AddListener(HintManager.Instance.OnSkipButtonClicked);


            skipYesButton.onClick.RemoveAllListeners();
            skipYesButton.onClick.AddListener(HintManager.Instance.ConfirmSkip);


            skipNoButton.onClick.RemoveAllListeners();
            skipNoButton.onClick.AddListener(HintManager.Instance.CancelSkip);
        }
        else
        {
            Debug.LogError("HintManager instance not found!");
        }
    }
}
