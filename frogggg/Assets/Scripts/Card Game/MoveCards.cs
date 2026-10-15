using UnityEngine;
using UnityEngine.EventSystems;

public class MoveCards : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    //this script is placed Canvas -> Card Holders -> Player's Cards -> Player Card Slot 1 & 2
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Canvas canvas;

    Vector2 originalCardPos;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("mouse click");
        originalCardPos = rectTransform.anchoredPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("begin draggin");
        canvasGroup.alpha = 0.3f; // Make item slightly transparent
        canvasGroup.blocksRaycasts = false; // Allow drop zones to detect events
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End Drag");
        canvasGroup.alpha = 1f; // Reset transparency
        canvasGroup.blocksRaycasts = true; // Restore raycast blocking
        //put card back to original pos
        rectTransform.anchoredPosition = originalCardPos;
    }
}
