using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://answers.unity.com/questions/1775925/recttransform-position-relative-to-its-parent-scal.html

public class PreservePosition : MonoBehaviour
{
    RectTransform parentRect = null;

    float posPercentX = 0.5f;
    float posPercentY = 0.5f;

    void Start()
    {
        parentRect = transform.parent.GetComponent<RectTransform>();
        
        //UpdatePosition();
    }

    public void UpdatePosition()
    {
        Vector2 halfRectSize = parentRect.rect.size * 0.5f;
        posPercentX = Mathf.InverseLerp(-halfRectSize.x, halfRectSize.x, transform.localPosition.x);
        posPercentY = Mathf.InverseLerp(-halfRectSize.y, halfRectSize.y, transform.localPosition.y);
    }

    void Update()
    {
        Vector2 halfRectSize = parentRect.rect.size * 0.5f;

        float posX = Mathf.Lerp(-halfRectSize.x, halfRectSize.x, posPercentX);
        float posY = Mathf.Lerp(-halfRectSize.y, halfRectSize.y, posPercentY);

        transform.localPosition = new Vector2(posX, posY);
    }
}