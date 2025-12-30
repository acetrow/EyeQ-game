using UnityEngine;
using UnityEngine.EventSystems;

public class DraggedObjectDisappears : MonoBehaviour, IPointerDownHandler, IDragHandler, IEndDragHandler
{
    private Vector2 originalPosition; // Stores the original 
    public RectTransform targetObject; // Assign the second object 

    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        originalPosition = rectTransform.anchoredPosition; 
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnDrag(PointerEventData eventData)
    {
        
        Vector2 pointerPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            transform.parent.GetComponent<RectTransform>(),
            eventData.position,
            eventData.pressEventCamera,
            out pointerPosition);

 
        rectTransform.anchoredPosition = pointerPosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        if (IsOverlappingTarget())
        {
            Debug.Log("Object collided with target! Hiding draggable object...");
            gameObject.SetActive(false); 
        }
        else
        {
            Debug.Log("Object not on target, returning to original position.");
            rectTransform.anchoredPosition = originalPosition; 
        }
    }

    private bool IsOverlappingTarget()
    {

        if (targetObject == null) return false;


        return RectTransformUtility.RectangleContainsScreenPoint(
            targetObject,
            rectTransform.position,
            null 
        );
    }
}
