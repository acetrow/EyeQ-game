using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement; // Import Scene Management

public class HintManagerOtherScenes : MonoBehaviour
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

    public GameObject watchAdPrompt;
    public TMP_Text watchAdPromptText;
    public Button watchAdYesButton;
    public Button watchAdNoButton;
    public GameObject videoPanel;
    public VideoPlayer videoPlayer;
    public VideoClip videoClip;
    public RawImage rawImage;

    [TextArea(3, 5)]
    public string sceneSpecificHint;

    void Start()
    {
        if (HintManager1.Instance != null)
        {
            SyncHintManager();
        }
        else
        {
            Debug.LogError("HintManager1 instance not found! Ensure your persistent HintManager1 is active in your first scene.");
        }
    }

    private void SyncHintManager()
    {

        HintManager1.Instance.hintText = hintText;
        HintManager1.Instance.confirmationWindow = confirmationWindow;
        HintManager1.Instance.hintWindow = hintWindow;
        HintManager1.Instance.hintContentText = hintContentText;
        HintManager1.Instance.skipConfirmationWindow = skipConfirmationWindow;
        HintManager1.Instance.customizableHint = sceneSpecificHint;

        // Sync ad-related UI elements
        HintManager1.Instance.watchAdPrompt = watchAdPrompt;
        HintManager1.Instance.watchAdPromptText = watchAdPromptText;
        HintManager1.Instance.videoPanel = videoPanel;
        HintManager1.Instance.videoPlayer = videoPlayer;
        HintManager1.Instance.videoClip = videoClip;
        HintManager1.Instance.rawImage = rawImage;

        // Ensure hint text updates
        HintManager1.Instance.SetHintTextReference(hintText);

        // Setup button listeners
        AssignButtonListener(hintButton, OnHintButtonClicked);
        AssignButtonListener(yesButton, ConfirmUseHint);
        AssignButtonListener(noButton, CancelUseHint);
        AssignButtonListener(closeHintButton, CloseHintWindow);
        AssignButtonListener(skipButton, OnSkipButtonClicked);
        AssignButtonListener(skipYesButton, ConfirmSkip);
        AssignButtonListener(skipNoButton, CancelSkip);
        AssignButtonListener(watchAdYesButton, ConfirmWatchAd);
        AssignButtonListener(watchAdNoButton, CancelWatchAd);

        // Subscribe to video 
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached += OnVideoFinished;
        }
    }

    private void AssignButtonListener(Button button, UnityEngine.Events.UnityAction action)
    {
        if (button != null)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(action);
        }
    }


    public void OnHintButtonClicked()
    {
        if (HintManager1.Instance.hintsAvailable > 0)
        {
            confirmationWindow.SetActive(true);
        }
        else
        {
            ShowWatchAdPrompt();
        }
    }

    public void ConfirmUseHint()
    {
        if (HintManager1.Instance.hintsAvailable > 0)
        {
            HintManager1.Instance.hintsAvailable--;
            UpdateHintText();
            confirmationWindow.SetActive(false);
            hintContentText.text = sceneSpecificHint;
            hintWindow.SetActive(true);
        }
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
        if (HintManager1.Instance.hintsAvailable >= 2)
        {
            skipConfirmationWindow.SetActive(true);
        }
        else
        {
            ShowWatchAdPrompt();
        }
    }

    public void ConfirmSkip()
    {
        if (HintManager1.Instance.hintsAvailable >= 2)
        {
            HintManager1.Instance.hintsAvailable -= 2;
            UpdateHintText();
            skipConfirmationWindow.SetActive(false);
            LoadNextScene(); // Load the next scene
        }
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
            hintText.text = "Hints: " + HintManager1.Instance.hintsAvailable;
        }
    }


    public void ShowWatchAdPrompt()
    {
        if (watchAdPrompt != null)
        {
            watchAdPrompt.SetActive(true);
        }
    }

    public void ConfirmWatchAd()
    {
        watchAdPrompt.SetActive(false);
        PlayAdVideo();
    }

    public void CancelWatchAd()
    {
        watchAdPrompt.SetActive(false);
    }

    private void PlayAdVideo()
    {
        if (videoPanel != null && videoPlayer != null)
        {
            videoPanel.SetActive(true);
            if (rawImage != null)
            {
                rawImage.gameObject.SetActive(true);
            }
            if (videoPlayer.isPrepared)
            {
                if (rawImage != null)
                {
                    rawImage.texture = videoPlayer.texture;
                }
                videoPlayer.Play();
            }
            else
            {
                videoPlayer.Prepare();
                videoPlayer.prepareCompleted += OnVideoPrepared;
            }
        }
    }

    private void OnVideoPrepared(VideoPlayer vp)
    {
        if (rawImage != null)
        {
            rawImage.texture = videoPlayer.texture;
        }
        videoPlayer.Play();
        videoPlayer.prepareCompleted -= OnVideoPrepared;
    }

    private void OnVideoFinished(VideoPlayer vp)
    {
        HintManager1.Instance.hintsAvailable++; 
        UpdateHintText();

   
        videoPanel.SetActive(false);
        if (rawImage != null)
        {
            rawImage.gameObject.SetActive(false);
        }


        videoPlayer.Stop();
        SetVideoSource();
    }


    private void SetVideoSource()
    {
        if (videoClip != null && videoPlayer != null)
        {
            videoPlayer.source = VideoSource.VideoClip;
            videoPlayer.clip = videoClip;
            videoPlayer.renderMode = VideoRenderMode.APIOnly;

          
            videoPlayer.Prepare();
            videoPlayer.prepareCompleted += OnVideoPrepared;
        }
        else
        {
            Debug.LogError("VideoClip or VideoPlayer is not assigned in the Inspector.");
        }
    }
}
