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
                var mousePos = GetMousePosition(Input.mousePosition);

                SpriteToMove.transform.position = mousePos + (Vector3) PositionDelta;
            }
        }

        protected virtual Vector3 GetMousePosition(Vector3 position)
        {
            var mousePos = AppModel.Instance.Camera.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            return mousePos;
        }
    }
}
