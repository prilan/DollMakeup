using DG.Tweening;
using UnityEngine;

namespace DollMakeup.UI.Tool
{
    public class Brush : Tool
    {
        private const float BRUSH_LENGTH = 220;
        private const float BRUSH_TARGET_SHIFT = 50;
        private const float BRUSH_ACTIVATE_MOVE_DURATION_SEC = 0.4f;
        private const float BRUSH_IN_COLOR_SHIFT = 30;
        
        protected override void StartBrushAnimation(Vector3 colorPosition)
        {
            base.StartBrushAnimation(colorPosition);
            

            brushAnimation = DOTween.Sequence();
            brushAnimation.OnKill(() => brushAnimation = null);

            var targetPosition = colorPosition + new Vector3(BRUSH_TARGET_SHIFT, -BRUSH_LENGTH, 0);

            Vector3 facePosition = WorldToCanvasPosition(AppModel.Instance.FacePosition, AppModel.Instance.Canvas);
            var centerPosition = (facePosition + colorPosition) / 2;

            brushAnimation.Append(BrushTool.transform.DOMove(targetPosition, BRUSH_ACTIVATE_MOVE_DURATION_SEC));
            brushAnimation.Join(BrushTool.transform.DORotate(new Vector3(0, 0, 15), BRUSH_ACTIVATE_MOVE_DURATION_SEC));

            brushAnimation.Append(BrushTool.transform.DOMove(targetPosition + new Vector3(BRUSH_IN_COLOR_SHIFT, 0, 0), 0.15f).SetEase(Ease.OutSine));
            brushAnimation.Append(BrushTool.transform.DOMove(targetPosition + new Vector3(-BRUSH_IN_COLOR_SHIFT, 0, 0), 0.2f).SetEase(Ease.OutSine));
            brushAnimation.Append(BrushTool.transform.DOMove(targetPosition + new Vector3(BRUSH_IN_COLOR_SHIFT, 0, 0), 0.2f).SetEase(Ease.OutSine));

            brushAnimation.Append(BrushTool.transform.DOMove(centerPosition, 0.5f).SetEase(Ease.InOutSine));
            brushAnimation.Join(BrushTool.transform.DORotate(new Vector3(0, 0, 0), 0.5f));

            brushAnimation.OnComplete(() =>
            {
                IsMovable = true;
            });
        }
    }
}
