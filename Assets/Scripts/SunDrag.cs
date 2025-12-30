using UnityEngine;
using UnityEngine.SceneManagement;

public class SunDrag : MonoBehaviour
{
    private Vector3 offset;
    private bool isDragging = false;

    private float screenLeft, screenRight, screenTop, screenBottom;

    void Start()
    {

        Camera cam = Camera.main;
        screenLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane)).x;
        screenRight = cam.ViewportToWorldPoint(new Vector3(1, 0, cam.nearClipPlane)).x;
        screenTop = cam.ViewportToWorldPoint(new Vector3(0, 1, cam.nearClipPlane)).y;
        screenBottom = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane)).y;
    }

    void OnMouseDown()
    {
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        isDragging = true;
    }

    void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
            transform.position = newPosition;
        }
    }

    void OnMouseUp()
    {
        isDragging = false;

        if (transform.position.x < screenLeft || transform.position.x > screenRight ||
            transform.position.y < screenBottom || transform.position.y > screenTop)
        {
            gameObject.SetActive(false); 

        }
    }
}
