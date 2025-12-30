using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RapidClicksLoad : MonoBehaviour, IPointerClickHandler
{
    public int requiredClicks = 5; 
    public float clickInterval = 0.5f; 
    private int clickCount = 0;
    private float lastClickTime = 0f;

    public void OnPointerClick(PointerEventData eventData)
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

        Debug.Log("Click count: " + clickCount);

  
        if (clickCount >= requiredClicks)
        {
            Debug.Log("Required clicks reached! Loading next scene...");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
