using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DollMakeup.UI.Tool
{
    public class Lipstick : Tool, IPointerDownHandler, IDragHandler
    {
        private const float FACE_LIPSTICK_ADD_LENGTH = 255;
        private const float LIPSTICK_APPLY_SHIFT = 30;
        
        public Action OnColorPick = () => { };

        private int LipstickIndex;
        
        public void InitIndex(int index)
        {
            LipstickIndex = index;
        }

        protected override void Initialize()
        {
            base.Initialize();

            ActiveBrushIndex = -1;
            FaceBrushTargetAddLength = FACE_LIPSTICK_ADD_LENGTH;
            BrushApplyShift = LIPSTICK_APPLY_SHIFT;
        }
        
        protected override void AddListeners()
        {
            base.AddListeners();

            EventEmitter.LipstickApplyComplete += OnBrushApplyComplete;
        }
        
        protected override void RemoveListeners()
        {
            base.RemoveListeners();

            EventEmitter.LipstickApplyComplete -= OnBrushApplyComplete;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnColorPick?.Invoke();
        }
        
        protected override void StartBrushAnimation(Vector3 colorPosition)
        {
            base.StartBrushAnimation(colorPosition);
            
            Debug.Log("colorPosition = " + colorPosition);

            brushAnimation = DOTween.Sequence();
            brushAnimation.OnKill(() => brushAnimation = null);

            Vector3 facePosition = WorldToCanvasPosition(AppModel.Instance.FacePosition, AppModel.Instance.Canvas);
            Debug.Log("facePosition = " + facePosition);
            var centerPosition = colorPosition + (facePosition - colorPosition) / 4;
            Debug.Log("centerPosition = " + centerPosition);

            brushAnimation.Append(BrushTool.transform.DOMove(centerPosition, 0.5f).SetEase(Ease.InOutSine));
            brushAnimation.Join(BrushTool.transform.DORotate(new Vector3(0, 0, 0), 0.5f));

            brushAnimation.OnComplete(() =>
            {
                IsMovable = true;
            });
        }

        protected override void OnBrushEndDrag(Vector2 position, int activeBrushIndex)
        {
            base.OnBrushEndDrag(position, activeBrushIndex);

            AppModel.Instance.OnLipstickEndDrag(position, activeBrushIndex);
        }

        protected override bool IsNeedBreakInBrushApplyComplete()
        {
            return LipstickIndex != ActiveBrushIndex;
        }

        protected override void OnBrushApplyCompleteFinalActions()
        {
            base.OnBrushApplyCompleteFinalActions();
            
            ActiveBrushIndex = -1;
        }
    }
}
