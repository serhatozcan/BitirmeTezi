using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDropPanel : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas;

    public void DragHandler(BaseEventData data)
    {
        PointerEventData pointerEventData = (PointerEventData) data;
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)canvas.transform,
            pointerEventData.position,
            canvas.worldCamera,
            out position);
            
        transform.position = canvas.transform.TransformPoint(position);
    }

    //private bool _dragging;

    //public void OnPointerDown(PointerEventData data)
    //{
    //    _dragging = true;
    //}

    //public void OnPointerUp(PointerEventData data)
    //{
    //    _dragging = false;
    //}

    //private void Update()
    //{
    //    if (_dragging == true)
    //        //transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
    //        transform.position = Input.GetTouch(0).position;
    //}
}