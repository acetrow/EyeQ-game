using UnityEngine;
using UnityEngine.SceneManagement; 
using UnityEngine.UI; 
using System.Collections; 


public class RapidClicks : MonoBehaviour
{
    public GameObject firstImage; 
    public GameObject secondImage; 
    public float clickInterval = 0.5f; 
    public int requiredClicks = 5; 

    private int clickCount = 0;
    private float lastClickTime = 0f;

    void Update()
    {
      
        if (Time.time - lastClickTime > clickInterval)
        {
            clickCount = 0;
        }
    }

    public void OnImageClick()
    {

        if (Time.time - lastClickTime <= clickInterval)
        {
            clickCount++;
        }
        else
        {
            clickCount = 1; 
        }

        lastClickTime = Time.time;


        if (clickCount >= requiredClicks)
        {
            StartCoroutine(SwapImagesAndLoadScene());
        }
    }

    private IEnumerator SwapImagesAndLoadScene()
    {

        firstImage.SetActive(false);


        secondImage.SetActive(true);


        yield return new WaitForSeconds(2f);


        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
