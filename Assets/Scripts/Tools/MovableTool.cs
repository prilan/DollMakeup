using UnityEngine;
using UnityEngine.EventSystems;

namespace DollMakeup.Tools
{
    public class MovableTool : MonoBehaviour, IDragHandler
    {
        private bool IsOn;
        private GameObject SpriteToMove;

        public void StartDrag(GameObject spriteToMove)
        {
            Debug.Log("StartDrag");
            
            IsOn = true;
            SpriteToMove = spriteToMove;
        }

        public void EndDrag()
        {
            Debug.Log("EndDrag");
            
            IsOn = false;
            SpriteToMove = null;
        }
        
        public void OnDrag(PointerEventData eventData)
        {
            //Debug.Log("Drag position = " + eventData.position);
            
            if (IsOn)
            {
                if (Camera.main != null)
                {
                    var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    mousePos.z = 0;
                    //Debug.Log("mousePos = " + mousePos);

                    SpriteToMove.transform.position = mousePos;
                }
            }
        }
    }
}
