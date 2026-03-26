using System;
using DollMakeup.UI.Tool;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DollMakeup.UI.ToolBook
{
    public class ColorPick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        [SerializeField] private Brush Brush;
        
        public Action OnColorPick = () => { };

        public void OnPointerDown(PointerEventData eventData)
        {
            OnColorPick?.Invoke();
        }

        public void OnDrag(PointerEventData eventData)
        {
            Brush.OnDrag(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Brush.OnPointerUp(eventData);
        }
    }
}
