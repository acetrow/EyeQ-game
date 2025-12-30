using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadLevel11 : MonoBehaviour
{
    public Button loadButton; 

    void Start()
    {
        if (loadButton != null)
        {
            loadButton.onClick.AddListener(LoadLevel11Scene);
        }
        else
        {
            Debug.LogError("Load button not assigned in the Inspector!");
        }
    }

    public void LoadLevel11Scene()
    {
        
        SceneManager.LoadScene("Level11");
    }
}
