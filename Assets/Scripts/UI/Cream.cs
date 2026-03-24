using DG.Tweening;
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

        private const float CREAM_APPEAR_TIME_SEC = 0.2f;
        
        private Vector2 CreamImageWorldPosition;
        private Vector2 StartPosition;
        private Vector2 PositionDelta;
        private MovableTool CreamMove;

        private Sequence creamAnimation;

        private void Start()
        {
            CreamImageWorldPosition = AppModel.Instance.Camera.ScreenToWorldPoint(CreamImage.transform.position);
            StartPosition = CreamImageWorldPosition + (AppModel.Instance.FacePosition - CreamImageWorldPosition) / 3;
            PositionDelta = StartPosition - CreamImageWorldPosition; 
            CreamMove = GetComponent<MovableTool>();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnCreamClicked();

            StartCreamAnimation(eventData);
        }
        
        private void OnCreamClicked()
        {
            CreamImage.enabled = false;
            Text.enabled = false;
            
            CreamTool.SetActive(true);
            CreamTool.transform.position = CreamImageWorldPosition;
            CreamTool.transform.eulerAngles = Vector3.zero;
        }

        private void StartCreamAnimation(PointerEventData eventData)
        {
            creamAnimation = DOTween.Sequence();
            creamAnimation.OnKill(() => creamAnimation = null);

            creamAnimation.Append(CreamTool.transform.DOMove(StartPosition, CREAM_APPEAR_TIME_SEC));
            creamAnimation.Join(CreamTool.transform.DORotate(new Vector3(0, 0, -20), CREAM_APPEAR_TIME_SEC));
            creamAnimation.OnComplete(() => StartDrag(eventData));
        }

        private void StartDrag(PointerEventData eventData)
        {
            var clickDelta = (Vector2) AppModel.Instance.Camera.ScreenToWorldPoint(eventData.position) -
                             CreamImageWorldPosition;
            Debug.Log("OnPointerDown clickDelta = " + clickDelta);
            CreamMove.StartDrag(CreamTool, PositionDelta - clickDelta);
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
