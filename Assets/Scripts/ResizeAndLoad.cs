using UnityEngine;
using UnityEngine.SceneManagement;

public class ResizeAndLoad : MonoBehaviour
{
    private RectTransform rectTransform;
    private Vector3 initialScale;
    private float maxScaleFactor = 1.5f; 
    private bool hasTriggered = false; 

    void Start()
    {

        rectTransform = GetComponent<RectTransform>();


        initialScale = rectTransform.localScale;
    }

    void Update()
    {
        if (Input.touchCount == 2 && !hasTriggered) 
        {

            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);


            float currentDistance = Vector2.Distance(touch1.position, touch2.position);

  
            float previousDistance = Vector2.Distance(touch1.position - touch1.deltaPosition, touch2.position - touch2.deltaPosition);

            float scaleFactor = (currentDistance - previousDistance) * 0.005f; 

            float newScaleX = Mathf.Clamp(rectTransform.localScale.x + scaleFactor, initialScale.x, initialScale.x * maxScaleFactor);
            float newScaleY = Mathf.Clamp(rectTransform.localScale.y + scaleFactor, initialScale.y, initialScale.y * maxScaleFactor);

  
            rectTransform.localScale = new Vector3(newScaleX, newScaleY, 1);


            if (rectTransform.localScale.x >= initialScale.x * maxScaleFactor &&
                rectTransform.localScale.y >= initialScale.y * maxScaleFactor)
            {
                hasTriggered = true; 
                Debug.Log("Max scale reached! Waiting 2 seconds before loading next scene...");


                Invoke("LoadNextScene", 2f);
            }
        }
    }

    private void LoadNextScene()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
