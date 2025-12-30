using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndResize : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    private RectTransform rectTransform;
    private Vector2 initialTouchPosition;
    private Vector2 initialSizeDelta;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (Input.touchCount == 2)
        {
            //two touches
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);

            // Calculate the distance 
            float currentDistance = Vector2.Distance(touch1.position, touch2.position);

            float previousDistance = Vector2.Distance(touch1.position - touch1.deltaPosition, touch2.position - touch2.deltaPosition);

            // Calculate  scale factor
            float scaleFactor = (currentDistance - previousDistance) * 0.01f; // Adjust the factor for desired scaling sensitivity

            // Apply the scale
            rectTransform.localScale += new Vector3(scaleFactor, scaleFactor, 0);

            // Clamp the scale 
            rectTransform.localScale = new Vector3(
                Mathf.Clamp(rectTransform.localScale.x, 0.5f, 35f), // Adjust min and max scale as needed
                Mathf.Clamp(rectTransform.localScale.y, 0.5f, 35f),
                1
            );
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Calculate the initial touch 
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out initialTouchPosition
        );
        initialSizeDelta = rectTransform.sizeDelta;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Update the position
        Vector2 currentTouchPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectTransform.parent as RectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out currentTouchPosition
        );

        rectTransform.anchoredPosition += currentTouchPosition - initialTouchPosition;
    }
}
