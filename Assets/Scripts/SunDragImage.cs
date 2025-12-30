using UnityEngine;
using UnityEngine.UI; //
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Collections;

public class SunDragImage : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    private Vector2 offset;
    private RectTransform rectTransform;

    //
    private const float BORDER_LEFT = -550f;
    private const float BORDER_RIGHT = 550f;
    private const float BORDER_BOTTOM = -1250f;
    private const float BORDER_TOP = 1150f;

    void Start()
    {
        //
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out offset);
    }

    public void OnDrag(PointerEventData eventData)
    {
        //
        Vector2 pointerPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectTransform.parent as RectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out pointerPosition);

        //
        rectTransform.anchoredPosition = pointerPosition - offset;

        // 
        if (IsOutsideDefinedBorders())
        {
            //
            GetComponent<Image>().enabled = false;

            //
            StartCoroutine(LoadNextSceneAfterDelay(1f));
        }
    }

    private bool IsOutsideDefinedBorders()
    {
        //
        Vector2 anchoredPosition = rectTransform.anchoredPosition;

        //
        return anchoredPosition.x <= BORDER_LEFT || anchoredPosition.x >= BORDER_RIGHT ||
               anchoredPosition.y <= BORDER_BOTTOM || anchoredPosition.y >= BORDER_TOP;
    }

    private IEnumerator LoadNextSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
