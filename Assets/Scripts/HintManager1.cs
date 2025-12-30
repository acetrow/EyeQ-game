using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video; 

public class HintManager1 : MonoBehaviour
{
    public static HintManager1 Instance; 

    public int hintsAvailable = 3; 
    public TMP_Text hintText; 
    public GameObject confirmationWindow; 
    public GameObject skipConfirmationWindow; 
    public GameObject hintWindow; 
    public TMP_Text hintContentText; 
    public string customizableHint = "This is your hint!"; 

    public GameObject watchAdPrompt;
    public TMP_Text watchAdPromptText; 
    public GameObject videoPanel; 
    public VideoPlayer videoPlayer; 


    public VideoClip videoClip;

    public RawImage rawImage;

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


        if (videoPanel != null)
        {
            videoPanel.SetActive(false);
        }
        if (rawImage != null)
        {
            rawImage.gameObject.SetActive(false);
        }

        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached += OnVideoFinished;
        }

        SetVideoSource();
    }

    public void OnHintButtonClicked()
    {
        if (hintsAvailable > 0)
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
        hintsAvailable--; 
        UpdateHintText();
        confirmationWindow.SetActive(false);


        hintContentText.text = "Hints: " + hintsAvailable;
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
            ShowWatchAdPrompt(); 
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



    public void ShowWatchAdPrompt()
    {
        if (watchAdPrompt != null)
        {
            watchAdPrompt.SetActive(true); 
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
        hintsAvailable++; 
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
