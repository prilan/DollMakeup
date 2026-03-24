using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DollMakeup.UI.ToolBook
{
    public class ColorPick : MonoBehaviour, IPointerDownHandler, IDragHandler
    {
        [SerializeField] private EyeBrush Brush;
        
        public Action OnColorPick = () => { };

        public void OnPointerDown(PointerEventData eventData)
        {
            OnColorPick?.Invoke();
        }

        public void OnDrag(PointerEventData eventData)
        {
            Debug.Log("ColorPick OnDrag");
            
            Brush.OnDrag(eventData);
        }
    }
}
