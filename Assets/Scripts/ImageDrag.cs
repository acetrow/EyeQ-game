using UnityEngine;
using UnityEngine.EventSystems;

public class ImageDrag : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    private Vector2 offset;

    public void OnPointerDown(PointerEventData eventData)
    {
        // Calculate offset 
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            GetComponent<RectTransform>(), 
            eventData.position, 
            eventData.pressEventCamera, 
            out offset);
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Update position
        Vector2 pointerPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            transform.parent.GetComponent<RectTransform>(), 
            eventData.position, 
            eventData.pressEventCamera, 
            out pointerPosition);

        // Apply the offset
        transform.localPosition = pointerPosition - offset;
    }
}
