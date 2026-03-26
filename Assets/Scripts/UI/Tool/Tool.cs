using DG.Tweening;
using DollMakeup.Tools;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DollMakeup.UI.Tool
{
    public class Tool : MonoBehaviour, IPointerUpHandler
    {
        [SerializeField] protected GameObject BrushTool;
        private Image BrushImage => gameObject.GetComponent<Image>();
        
        private const float BRUSH_APPLY_START_MOVE_DURATION_SEC = 0.15f;
        private const float BRUSH_APPLY_MOVE_ONE_SIDE_DURATION_SEC = 0.2f;
        private const float BRUSH_RETURN_DURATION_SEC = 0.2f;

        protected float FaceBrushTargetAddLength;
        protected float BrushApplyShift;

        private Vector2 BrushImagePosition;
        private UIMovableTool BrushMove;
        protected int ActiveBrushIndex;
        
        protected Sequence brushAnimation;
        protected bool IsMovable;

        protected virtual void Initialize()
        {
        }
        
        protected virtual void AddListeners()
        {
        }
        
        protected virtual void RemoveListeners()
        {
        }
        
        private void Start()
        {
            BrushImagePosition = BrushImage.transform.position;
            BrushMove = GetComponent<UIMovableTool>();

            Initialize();
            AddListeners();
        }

        private void OnDestroy()
        {
            RemoveListeners();
        }

        public void BrushActivate(Vector3 colorPosition, int index)
        {
            ActiveBrushIndex = index;
            
            OnBrushClicked();

            StartBrushAnimation(colorPosition);
        }
        
        private void OnBrushClicked()
        {
            BrushImage.enabled = false;
            
            BrushTool.SetActive(true);
            BrushTool.transform.position = BrushImagePosition;
            BrushTool.transform.eulerAngles = Vector3.zero;
        }
        
        protected virtual void StartBrushAnimation(Vector3 colorPosition)
        {
        }

        protected Vector2 WorldToCanvasPosition(Vector2 position, Canvas canvas)
        {
            Vector2 ViewPos = AppModel.Instance.Camera.WorldToViewportPoint(position);
            var CanvasSize = canvas.gameObject.GetComponent<RectTransform>().sizeDelta;
            return new Vector2(ViewPos.x * CanvasSize.x, ViewPos.y * CanvasSize.y);
        }

        public void OnDrag(PointerEventData eventData)
        {
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

            OnBrushEndDrag(position, ActiveBrushIndex);

            BrushApplyAnimation();
        }

        protected virtual void OnBrushEndDrag(Vector2 position, int activeBrushIndex)
        {
        }

        private void BrushApplyAnimation()
        {
            brushAnimation = DOTween.Sequence();
            brushAnimation.OnKill(() => brushAnimation = null);

            Vector3 facePosition = WorldToCanvasPosition(AppModel.Instance.FacePosition, AppModel.Instance.Canvas);
            Debug.Log("facePosition = " + facePosition);
            
            var targetPosition = facePosition - new Vector3(0, FaceBrushTargetAddLength, 0);

            brushAnimation.Append(BrushTool.transform.DOMove(targetPosition, BRUSH_APPLY_START_MOVE_DURATION_SEC));

            brushAnimation.Append(BrushTool.transform.DOMove(targetPosition + new Vector3(BrushApplyShift, 0, 0),  BRUSH_APPLY_MOVE_ONE_SIDE_DURATION_SEC).SetEase(Ease.OutSine));
            brushAnimation.Append(BrushTool.transform.DOMove(targetPosition + new Vector3(-BrushApplyShift, 0, 0), BRUSH_APPLY_MOVE_ONE_SIDE_DURATION_SEC).SetEase(Ease.OutSine));
            brushAnimation.Append(BrushTool.transform.DOMove(targetPosition + new Vector3(BrushApplyShift, 0, 0),  BRUSH_APPLY_MOVE_ONE_SIDE_DURATION_SEC).SetEase(Ease.OutSine));
            brushAnimation.Append(BrushTool.transform.DOMove(targetPosition + new Vector3(-BrushApplyShift, 0, 0), BRUSH_APPLY_MOVE_ONE_SIDE_DURATION_SEC).SetEase(Ease.OutSine));
            brushAnimation.Append(BrushTool.transform.DOMove(targetPosition + new Vector3(BrushApplyShift, 0, 0),  BRUSH_APPLY_MOVE_ONE_SIDE_DURATION_SEC).SetEase(Ease.OutSine));

            brushAnimation.Append(BrushTool.transform.DOMove(targetPosition, BRUSH_APPLY_MOVE_ONE_SIDE_DURATION_SEC).SetEase(Ease.InOutSine));

            brushAnimation.OnComplete(() =>
            {
                IsMovable = true;
            });
        }
        
        protected void OnBrushApplyComplete()
        {
            Debug.Log("OnBrushApplyComplete");
            
            if (IsNeedBreakInBrushApplyComplete())
                return;
            
            brushAnimation.Complete();
            brushAnimation.Kill();
            
            brushAnimation = DOTween.Sequence();
            brushAnimation.OnKill(() => brushAnimation = null);
            
            brushAnimation.Append(BrushTool.transform.DOMove(BrushImagePosition, BRUSH_RETURN_DURATION_SEC));
            brushAnimation.Join(BrushTool.transform.DORotate(new Vector3(0, 0, 0), BRUSH_RETURN_DURATION_SEC));
            brushAnimation.OnComplete(() =>
            {
                BrushImage.enabled = true;

                BrushTool.SetActive(false);

                OnBrushApplyCompleteFinalActions();
            });
        }

        protected virtual bool IsNeedBreakInBrushApplyComplete()
        {
            return false;
        }

        protected virtual void OnBrushApplyCompleteFinalActions()
        {
        }
    }
}
