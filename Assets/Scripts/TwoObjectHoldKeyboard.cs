using UnityEngine;
using UnityEngine.SceneManagement;

public class TwoObjectHoldKeyboard : MonoBehaviour
{
    public float holdDuration = 1f; // Time 
    private float holdTimer1 = 0f; // Timer 
    private float holdTimer2 = 0f; // Timer 
    private bool isHolding1 = false;
    private bool isHolding2 = false;

    void Update()
    {
        // 
        isHolding1 = Input.GetKey(KeyCode.A); // Simulate
        isHolding2 = Input.GetKey(KeyCode.B); // Simulate
        // Incre
        if (isHolding1)
            holdTimer1 += Time.deltaTime;
        else
            holdTimer1 = 0f; //

        if (isHolding2)
            holdTimer2 += Time.deltaTime;
        else
            holdTimer2 = 0f; //

        // 
        if (holdTimer1 >= holdDuration && holdTimer2 >= holdDuration)
        {
            LoadNextScene();
        }
    }

    private void LoadNextScene()
    {
        //
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
