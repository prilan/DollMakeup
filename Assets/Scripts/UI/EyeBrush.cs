using DG.Tweening;
using DollMakeup.Model;
using DollMakeup.Tools;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DollMakeup.UI
{
    public class EyeBrush : MonoBehaviour, IPointerDownHandler, IPointerUpHandler//, IDragHandler
    {
        [SerializeField] private Image BrushImage;
        [SerializeField] private GameObject BrushTool;
        
        private const float BRUSH_LENGTH = 220;
        private const float BRUSH_TARGET_SHIFT = 50;
        private const float BRUSH_ACTIVATE_MOVE_DURATION_SEC = 0.4f;
        private const float BRUSH_IN_COLOR_SHIFT = 30;

        private Vector2 BrushImagePosition;
        private UIMovableTool BrushMove;
        private int ActiveBrushIndex;
        
        private Sequence brushAnimation;
        private bool IsMovable;

        private void Start()
        {
            BrushImagePosition = BrushImage.transform.position;
            BrushMove = GetComponent<UIMovableTool>();
        }

        public void BrushActivate(Vector3 colorPosition, int index)
        {
            ActiveBrushIndex = index;
            
            OnBrushClicked();

            StartBrushAnimation(colorPosition);
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {

        }
        
        private void OnBrushClicked()
        {
            BrushImage.enabled = false;
            
            BrushTool.SetActive(true);
            BrushTool.transform.position = BrushImagePosition;
            BrushTool.transform.eulerAngles = Vector3.zero;
        }
        
        private void StartBrushAnimation(Vector3 colorPosition)
        {
            Debug.Log("colorPosition = " + colorPosition);

            brushAnimation = DOTween.Sequence();
            brushAnimation.OnKill(() => brushAnimation = null);

            var targetPosition = colorPosition + new Vector3(BRUSH_TARGET_SHIFT, -BRUSH_LENGTH, 0);

            Vector3 facePosition = WorldToCanvasPosition(AppModel.Instance.FacePosition, AppModel.Instance.Canvas);
            Debug.Log("facePosition = " + facePosition);
            var centerPosition = (facePosition + colorPosition) / 2;
            Debug.Log("centerPosition = " + centerPosition);

            brushAnimation.Append(BrushTool.transform.DOMove(targetPosition, BRUSH_ACTIVATE_MOVE_DURATION_SEC));
            brushAnimation.Join(BrushTool.transform.DORotate(new Vector3(0, 0, 15), BRUSH_ACTIVATE_MOVE_DURATION_SEC));

            //brushAnimation.AppendInterval(0.05f);

            brushAnimation.Append(BrushTool.transform.DOMove(targetPosition + new Vector3(BRUSH_IN_COLOR_SHIFT, 0, 0), 0.15f).SetEase(Ease.OutSine));
            brushAnimation.Append(BrushTool.transform.DOMove(targetPosition + new Vector3(-BRUSH_IN_COLOR_SHIFT, 0, 0), 0.2f).SetEase(Ease.OutSine));
            brushAnimation.Append(BrushTool.transform.DOMove(targetPosition + new Vector3(BRUSH_IN_COLOR_SHIFT, 0, 0), 0.2f).SetEase(Ease.OutSine));

            brushAnimation.Append(BrushTool.transform.DOMove(centerPosition, 0.5f).SetEase(Ease.InOutSine));

            brushAnimation.OnComplete(() =>
            {
                IsMovable = true;
            });
        }

        private Vector2 WorldToCanvasPosition(Vector2 position, Canvas canvas)
        {
            Vector2 ViewPos = AppModel.Instance.Camera.WorldToViewportPoint(position);
            var CanvasSize = canvas.gameObject.GetComponent<RectTransform>().sizeDelta;
            return new Vector2(ViewPos.x * CanvasSize.x, ViewPos.y * CanvasSize.y);
        }

        public void OnDrag(PointerEventData eventData)
        {
            //Debug.Log("EyeBrush OnDrag, IsMovable = " + IsMovable);

            if (!IsMovable)
                return;
            
            StartDrag(eventData);
        }
        
        private void StartDrag(PointerEventData eventData)
        {
            BrushMove.StartDrag(BrushTool, true);
            BrushMove.OnDrag(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            BrushMove.EndDrag();
            var position = (Vector2) AppModel.Instance.Camera.ScreenToWorldPoint(eventData.position);
            Debug.Log("OnPointerUp position = " + position);
            AppModel.Instance.OnEyeBrushEndDrag(position, ActiveBrushIndex);
        }
    }
}
