using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class Level1 : MonoBehaviour
{
    public int numberValue;  

    
    public void OnNumberClicked()
    {
        if (numberValue == 3)  
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
        else
        {
           
        }
    }
}
