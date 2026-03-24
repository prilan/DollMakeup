using System;
using DollMakeup.Tools;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DollMakeup.UI
{
    public class EyeBrush : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private Image BrushImage;
        [SerializeField] private GameObject BrushTool;
        
        private Vector2 BrushImagePosition;
        private MovableTool BrushMove;

        private void Start()
        {
            BrushImagePosition = BrushImage.transform.position;
            BrushMove = GetComponent<MovableTool>();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnBrushClicked();

            StartDrag(eventData);
        }
        
        private void OnBrushClicked()
        {
            BrushImage.enabled = false;
            
            BrushTool.SetActive(true);
            BrushTool.transform.position = BrushImagePosition;
            BrushTool.transform.eulerAngles = Vector3.zero;
        }
        
        private void StartDrag(PointerEventData eventData)
        {
            BrushMove.StartDrag(BrushTool, true);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            
        }
    }
}
