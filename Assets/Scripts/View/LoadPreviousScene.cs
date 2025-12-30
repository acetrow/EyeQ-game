using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadPreviousScene : MonoBehaviour
{
    public void LoadPrevious()
    {
        // Get the current scene index
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Check if there is a previous scene
        if (currentSceneIndex > 0)
        {
            // Load the previous scene
            SceneManager.LoadScene(currentSceneIndex - 1);
        }
        else
        {
            Debug.Log("No previous scene to load.");
        }
    }
}
