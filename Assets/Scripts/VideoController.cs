using UnityEngine;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject videoPanel; // 
    void Start()
    {
        videoPanel.SetActive(false); // 
    }

    public void PlayVideo()
    {
        videoPanel.SetActive(true); 
        videoPlayer.Play(); // 
    }

    void Update()
    {

        if (videoPlayer.isPrepared && !videoPlayer.isPlaying)
        {
            videoPanel.SetActive(false);
        }
    }
}
