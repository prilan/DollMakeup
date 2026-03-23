using UnityEngine;
using UnityEngine.EventSystems;

namespace DollMakeup.Tools
{
    public class MovableTool : MonoBehaviour, IDragHandler
    {
        private bool IsOn;
        private GameObject SpriteToMove;
        private Vector2 PositionDelta;

        public void StartDrag(GameObject spriteToMove, Vector2 positionDelta = new Vector2())
        {
            Debug.Log("StartDrag positionDelta = " + positionDelta);
            
            IsOn = true;
            SpriteToMove = spriteToMove;
            PositionDelta = positionDelta;
        }

        public void EndDrag()
        {
            Debug.Log("EndDrag");
            
            IsOn = false;
            SpriteToMove = null;
        }
        
        public void OnDrag(PointerEventData eventData)
        {
            if (IsOn)
            {
                    var mousePos = AppModel.Instance.Camera.ScreenToWorldPoint(Input.mousePosition);
                    mousePos.z = 0;

                    SpriteToMove.transform.position = mousePos + (Vector3)PositionDelta;
            }
        }
    }
}
