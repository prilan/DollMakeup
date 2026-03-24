using UnityEngine;
using UnityEngine.EventSystems;

namespace DollMakeup.Tools
{
    public class MovableTool : MonoBehaviour, IDragHandler
    {
        private bool IsOn;
        private bool IsUI;
        private GameObject SpriteToMove;
        private Vector2 PositionDelta;

        public void StartDrag(GameObject spriteToMove, bool isUI, Vector2 positionDelta = new Vector2())
        {
            Debug.Log("StartDrag positionDelta = " + positionDelta);

            IsUI = isUI;
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
                Vector3 mousePos;
                if (IsUI)
                {
                    mousePos = Input.mousePosition;
                }
                else
                {
                    mousePos = AppModel.Instance.Camera.ScreenToWorldPoint(Input.mousePosition);
                    mousePos.z = 0;
                }

                SpriteToMove.transform.position = mousePos + (Vector3) PositionDelta;
            }
        }
    }
}
