using UnityEngine;

namespace DollMakeup.UI.Tool
{
    public class BlushBrush : Brush
    {
        private const float FACE_BLUSH_ADD_LENGTH = 300;
        private const float BLUSH_BRUSH_APPLY_SHIFT = 80;

        protected override void Initialize()
        {
            base.Initialize();

            FaceBrushTargetAddLength = FACE_BLUSH_ADD_LENGTH;
            BrushApplyShift = BLUSH_BRUSH_APPLY_SHIFT;
        }
        
        protected override void AddListeners()
        {
            base.AddListeners();
            
            EventEmitter.BlushBrushApplyComplete += OnBrushApplyComplete;
        }

        protected override void RemoveListeners()
        {
            base.RemoveListeners();
            
            EventEmitter.BlushBrushApplyComplete -= OnBrushApplyComplete;
        }
        
        protected override void OnBrushEndDrag(Vector2 position, int activeBrushIndex)
        {
            base.OnBrushEndDrag(position, activeBrushIndex);

            AppModel.Instance.OnBlushBrushEndDrag(position, activeBrushIndex);
        }
    }
}
