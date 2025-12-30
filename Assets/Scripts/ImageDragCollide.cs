using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Collections;

public class ImageDragCollide : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    private Vector2 offset;
    public RectTransform targetObject; // The object to disappear
    public RectTransform newObject;   // The object to appear

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

        // Check for overlap 
        if (IsOverlappingTarget())
        {
            Debug.Log("Object is overlapping with the target! Making target disappear...");
            HideTargetObjectAndShowNewObject();
        }
    }

    private bool IsOverlappingTarget()
    {
        // Get the RectTransform 
        RectTransform draggableRect = GetComponent<RectTransform>();


        return RectTransformUtility.RectangleContainsScreenPoint(
            targetObject,
            draggableRect.position,
            null 
        );
    }

    private void HideTargetObjectAndShowNewObject()
    {
        // Deactivate the target 
        targetObject.gameObject.SetActive(false);

        // Activate 
        if (newObject != null)
        {
            newObject.gameObject.SetActive(true);

            // Start coroutine 
            StartCoroutine(LoadNextSceneAfterDelay(1f));
        }
    }

    private IEnumerator LoadNextSceneAfterDelay(float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Load the next scene in the build order
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
