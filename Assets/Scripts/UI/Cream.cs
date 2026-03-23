using DollMakeup.Tools;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DollMakeup.UI
{
    public class Cream : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private Button CreamButton;
        [SerializeField] private Image CreamImage;
        [SerializeField] private TextMeshProUGUI Text;
        [SerializeField] private GameObject CreamTool;

        private Vector2 CreamImageWorldPosition;
        private Vector2 StartPosition;
        private Vector2 PositionDelta;
        private MovableTool CreamMove;

        private void Start()
        {
            CreamImageWorldPosition = AppModel.Instance.Camera.ScreenToWorldPoint(CreamImage.transform.position);
            StartPosition = (CreamImageWorldPosition + AppModel.Instance.FacePosition) / 2;
            PositionDelta = StartPosition - CreamImageWorldPosition; 
            CreamMove = GetComponent<MovableTool>();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnCreamClicked();

            var clickDelta = (Vector2) AppModel.Instance.Camera.ScreenToWorldPoint(eventData.position) -
                             CreamImageWorldPosition;
            Debug.Log("OnPointerDown clickDelta = " + clickDelta);
            CreamMove.StartDrag(CreamTool, PositionDelta - clickDelta);
        }
        
        private void OnCreamClicked()
        {
            CreamImage.enabled = false;
            Text.enabled = false;
            
            CreamTool.SetActive(true);
            CreamTool.transform.position = StartPosition;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            CreamMove.EndDrag();
            var position = (Vector2) AppModel.Instance.Camera.ScreenToWorldPoint(eventData.position) + PositionDelta;
            Debug.Log("OnPointerUp position = " + position);
            AppModel.Instance.OnCreamEndDrag(position);
            
            CreamImage.enabled = true;
            Text.enabled = true;
            
            CreamTool.SetActive(false);
        }
    }
}
