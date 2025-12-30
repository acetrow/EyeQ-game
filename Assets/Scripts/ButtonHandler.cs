using UnityEngine;
using UnityEngine.SceneManagement; 

public class ButtonHandler : MonoBehaviour
{
    public void OnButtonClickSuccess()
    {
        Debug.Log("You are correct!");
        SceneManager.LoadScene("Level3");
    }

    public void OnButtonClickFail()
    {
        Debug.Log("Wrong answer");
    }
}
