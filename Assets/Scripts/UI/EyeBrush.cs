using UnityEngine;

namespace DollMakeup.UI
{
    public class EyeBrush : Brush
    {
        private const float FACE_EYE_ADD_LENGTH = 220;
        private const float EYE_BRUSH_APPLY_SHIFT = 120;

        protected override void Initialize()
        {
            base.Initialize();

            FaceBrushTargetAddLength = FACE_EYE_ADD_LENGTH;
            BrushApplyShift = EYE_BRUSH_APPLY_SHIFT;
        }

        protected override void AddListeners()
        {
            base.AddListeners();
            
            EventEmitter.EyeBrushApplyComplete += OnBrushApplyComplete;
        }

        protected override void RemoveListeners()
        {
            base.RemoveListeners();
            
            EventEmitter.EyeBrushApplyComplete -= OnBrushApplyComplete;
        }

        protected override void OnBrushEndDrag(Vector2 position, int activeBrushIndex)
        {
            base.OnBrushEndDrag(position, activeBrushIndex);
            
            AppModel.Instance.OnEyeBrushEndDrag(position, activeBrushIndex);
        }
    }
}
