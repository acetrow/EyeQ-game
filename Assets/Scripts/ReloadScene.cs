using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReloadScene : MonoBehaviour
{
    public Button reloadButton;

    void Start()
    {

        if (reloadButton != null)
        {
            reloadButton.onClick.AddListener(ReloadCurrentScene);
        }
        else
        {
            Debug.LogError("Reload button not assigned in the Inspector!");
        }
    }

    public void ReloadCurrentScene()
    {
        Debug.Log("Reloading scene...");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reload current scene
    }
}
