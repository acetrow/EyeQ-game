using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ImageDragCollide2 : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    private Vector2 offset;
    public RectTransform targetObject;
    public RectTransform newObject;  

    public void OnPointerDown(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            GetComponent<RectTransform>(),
            eventData.position,
            eventData.pressEventCamera,
            out offset);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pointerPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            transform.parent.GetComponent<RectTransform>(),
            eventData.position,
            eventData.pressEventCamera,
            out pointerPosition);

        transform.localPosition = pointerPosition - offset;

        if (IsOverlappingTarget())
        {
            Debug.Log("Objects overlapped! Both will disappear.");
            HideBothObjectsAndLoadNextScene();
        }
    }

    private bool IsOverlappingTarget()
    {

        if (targetObject == null) return false;


        RectTransform draggableRect = GetComponent<RectTransform>();


        return RectTransformUtility.RectangleContainsScreenPoint(
            targetObject,
            draggableRect.position,
            null 
        );
    }

    private void HideBothObjectsAndLoadNextScene()
    {
        Debug.Log("Hiding objects, starting scene load...");


        if (targetObject != null)
        {

        }

        gameObject.SetActive(false);


        if (newObject != null)
        {
            newObject.gameObject.SetActive(true);
        }


        Invoke("LoadNextScene", 1f);
    }

    private void LoadNextScene()
    {
        Debug.Log("Loading next scene...");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
