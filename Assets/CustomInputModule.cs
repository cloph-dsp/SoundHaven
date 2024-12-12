using UnityEngine;
using UnityEngine.EventSystems;

public class CustomInputModule : StandaloneInputModule
{
    private Touch touch;
    private PointerEventData lastPointerEventData;

    public override void Process()
    {
        lastPointerEventData = GetLastPointerEventData(-1);

        touch = Input.GetTouch(0);

        if (EventSystem.current.IsPointerOverGameObject(touch.fingerId) && lastPointerEventData.pointerCurrentRaycast.gameObject.CompareTag("UI"))
        {
            // Avoid processing if touch is over a UI with the tag "UI"
            return;
        }

        base.Process();
    }
}