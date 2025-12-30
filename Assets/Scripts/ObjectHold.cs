using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems; 

public class ObjectHold : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public float holdDuration = 2f; 
    private float holdTimer = 0f;
    private bool isHolding = false;

    void Update()
    {

        if (isHolding)
        {
            holdTimer += Time.deltaTime;


            if (holdTimer >= holdDuration)
            {
                LoadNextScene();
                isHolding = false; 
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {

        isHolding = true;
        holdTimer = 0f; 
    }

    public void OnPointerUp(PointerEventData eventData)
    {

        isHolding = false;
        holdTimer = 0f; 
    }

    private void LoadNextScene()
    {

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
