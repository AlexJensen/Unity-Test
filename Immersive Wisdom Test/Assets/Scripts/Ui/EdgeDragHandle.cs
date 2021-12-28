using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Ui
{
    public class EdgeDragHandle : DragHandle
    {
        public enum Edge
        {
            LEFT, RIGHT, TOP, BOTTOM, TOPLEFT, TOPRIGHT, BOTTOMLEFT, BOTTOMRIGHT
        }

        public Edge edge;

        protected Vector2 StartTransformScale { get; set; }

        private Dictionary<Edge, Vector2> Pivots = new Dictionary<Edge, Vector2>()
        {
            { Edge.LEFT, new Vector2(1f, .5f) },
            { Edge.RIGHT, new Vector2(0f, .5f) },
            { Edge.TOP, new Vector2(0.5f, 0f) },
            { Edge.BOTTOM, new Vector2(0.5f, 1f) },
            { Edge.TOPLEFT, new Vector2(1f, 0f) },
            { Edge.TOPRIGHT, new Vector2(0f, 0f) },
            { Edge.BOTTOMLEFT, new Vector2(1f, 1f) },
            { Edge.BOTTOMRIGHT, new Vector2(0f, 1f) },
        };


        public override void OnBeginDrag(PointerEventData eventData)
        {
            SetPivot(RectTransform, Pivots[edge]);
            StartTransformScale = RectTransform.sizeDelta;
            base.OnBeginDrag(eventData);
        }

        public override void OnDrag(PointerEventData eventData)
        {
            Vector2 mousePosition = ClampMousePositionInParent(eventData.position);
            Vector2 offset = TranslateOffsetEdge(mousePosition - StartMousePosition);
            RectTransform.sizeDelta = StartTransformScale + offset;
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            SetPivot(RectTransform, new Vector2(0.5f, 0.5f));
            base.OnEndDrag(eventData);
        }

        private Vector2 ClampMousePositionInParent(Vector2 position)
        {
            RectTransform parent = (RectTransform)RectTransform.parent;
            Rect parentRect = parent.rect;
            for (int i = 0; i < 2; i++)
            {
                position[i] = Mathf.Clamp(position[i], parent.position[i] + parentRect.min[i], parent.position[i] + parentRect.max[i]);
            }
            return position;
        }

        private Vector2 TranslateOffsetEdge(Vector2 offset)
        {
            switch (edge)
            {
                case Edge.LEFT:
                case Edge.BOTTOMLEFT:
                case Edge.TOPLEFT:
                    offset *= new Vector2(-1f, 1f);
                    break;
            }
            switch (edge)
            {
                case Edge.BOTTOM:
                case Edge.BOTTOMLEFT:
                case Edge.BOTTOMRIGHT:
                    offset *= new Vector2(1f, -1f);
                    break;
            }
            switch (edge)
            {
                case Edge.TOP:
                case Edge.BOTTOM:
                    offset *= new Vector2(0f, 1f);
                    break;
                case Edge.RIGHT:
                case Edge.LEFT:
                    offset *= new Vector2(1f, 0f);
                    break;
            }
            return offset;
        }

        // https://answers.unity.com/questions/976201/set-a-recttranforms-pivot-without-changing-its-pos.html
        public void SetPivot(RectTransform rectTransform, Vector2 pivot)
        {
            Vector2 size = rectTransform.sizeDelta;
            Vector2 deltaPivot = rectTransform.pivot - pivot;
            Vector3 deltaPosition = new Vector3(deltaPivot.x * size.x, deltaPivot.y * size.y);
            rectTransform.pivot = pivot;
            rectTransform.localPosition -= deltaPosition;
        }
    }
}
