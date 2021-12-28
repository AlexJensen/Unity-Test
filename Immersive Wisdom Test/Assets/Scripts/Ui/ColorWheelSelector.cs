using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ColorWheelSelector : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    RectTransform handle;

    [SerializeField]
    ColorWheel wheel;

    RectTransform wheelDistanceCheck;
    PreservePosition preservePos;

    Image wheelImage;

    private void Awake()
    {
        wheelImage = GetComponent<Image>();
        preservePos = handle.GetComponent<PreservePosition>();
        wheelDistanceCheck = (RectTransform)handle.parent;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //
    }

    public void OnDrag(PointerEventData eventData)
    {
        if ((eventData.position - (Vector2)wheelDistanceCheck.position).magnitude <
            wheelDistanceCheck.rect.width / 2)
        {
            handle.position = eventData.position;
            preservePos.UpdatePosition();


            Vector2 normalizedPos = new Vector2(
                (eventData.position.x - (wheelDistanceCheck.position.x + wheelDistanceCheck.rect.xMin)) /
                ((wheelDistanceCheck.position.x + wheelDistanceCheck.rect.xMax) - (wheelDistanceCheck.position.x + wheelDistanceCheck.rect.xMin)),
                (eventData.position.y - (wheelDistanceCheck.position.y + wheelDistanceCheck.rect.yMin)) /
                ((wheelDistanceCheck.position.y + wheelDistanceCheck.rect.yMax) - (wheelDistanceCheck.position.y + wheelDistanceCheck.rect.yMin)));


            Color color = wheelImage.sprite.texture.GetPixelBilinear(normalizedPos.x, normalizedPos.y);
            wheel.UpdateBackgroundColor(color);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //
    }

    public void UpdateWheelColor(Color color)
    {
        wheelImage.color = color;
    }


}
