using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndReturnImage : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private Vector2 originalPosition; //original position
    private RectTransform rectTransform;
    private Vector2 offset;

    void Start()
    {

        rectTransform = GetComponent<RectTransform>();
        originalPosition = rectTransform.anchoredPosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Calculate the offset 
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out offset);
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Update the position 
        Vector2 pointerPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectTransform.parent as RectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out pointerPosition);

        // Apply the offset 
        rectTransform.anchoredPosition = pointerPosition - offset;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // Return the object 
        rectTransform.anchoredPosition = originalPosition;
    }
}
