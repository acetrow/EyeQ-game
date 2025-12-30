using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class TwoObjectHold : MonoBehaviour
{
    public GameObject image1; // 
    public GameObject image2; // 
    public float holdDuration = 1f; // 

    private float holdTimer1 = 0f; //
    private float holdTimer2 = 0f; // 
    private bool isHolding1 = false; //
    private bool isHolding2 = false; // 

    void Update()
    {
        //
        if (isHolding1)
            holdTimer1 += Time.deltaTime;

        if (isHolding2)
            holdTimer2 += Time.deltaTime;

        //
        if (holdTimer1 >= holdDuration && holdTimer2 >= holdDuration)
        {
            LoadNextScene();
        }

        //
        if (!isHolding1 || !isHolding2)
        {
            holdTimer1 = 0f;
            holdTimer2 = 0f;
        }
    }

    public void OnPointerDownImage1()
    {
        isHolding1 = true;
    }

    public void OnPointerUpImage1()
    {
        isHolding1 = false;
    }

    public void OnPointerDownImage2()
    {
        isHolding2 = true;
    }

    public void OnPointerUpImage2()
    {
        isHolding2 = false;
    }

    private void LoadNextScene()
    {
        //
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
