using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadHomepage : MonoBehaviour
{
    public Button loadButton;

    void Start()
    {
       
        if (loadButton != null)
        {
            loadButton.onClick.AddListener(LoadScene);
        }
        else
        {
            Debug.LogError("Load button not assigned in the Inspector!");
        }
    }

    public void LoadScene()
    {
       
        SceneManager.LoadScene("homepage");
    }
}
