using System.Collections.Generic;
using DG.Tweening;
using DollMakeup.UI.ToolBook.Pages;
using UnityEngine;

namespace DollMakeup.Controllers
{
    public class DollController : MonoBehaviour
    {
        [SerializeField] public SpriteRenderer FaceSprite;
        [SerializeField] public SpriteRenderer FaceCleanSprite;

        [SerializeField] public List<EyeBrushSprites> EyeBrushList;

        private const float CREAM_APPLY_DURATION_SEC = 0.3f;
        private const float EYE_BRUSH_APPLY_DURATION_SEC = 0.4f;

        public Vector2 FacePosition => FaceSprite.transform.position;

        private bool IsCreamApplied;

        public void OnCreamEndDrag(Vector2 position)
        {
            Debug.Log("FacePosition = " + FacePosition);
            
            if (!IsCreamApplied
                &&position.x > FaceSprite.transform.position.x - FaceSprite.size.x / 4
                && position.x < FaceSprite.transform.position.x + FaceSprite.size.x / 2
                && position.y > FaceSprite.transform.position.y - FaceSprite.size.y / 2
                && position.y < FaceSprite.transform.position.y + FaceSprite.size.y / 2)
            {
                CreamApplied();
            }
            else
            {
                EventEmitter.OnCreamApplyComplete();
            }
        }

        public void OnSpongeClicked()
        {
            SpongeApplied();
        }

        public void OnEyeBrushEndDrag(Vector2 position, int activeBrushIndex)
        {
            Debug.Log("FacePosition = " + FacePosition);
            Debug.Log("position = " + position + ", activeBrushIndex = " + activeBrushIndex);
            
            if (position.x > FaceSprite.transform.position.x - FaceSprite.size.x / 4
                && position.x < FaceSprite.transform.position.x + FaceSprite.size.x / 2
                && position.y > FaceSprite.transform.position.y - FaceSprite.size.y / 2
                && position.y < FaceSprite.transform.position.y + FaceSprite.size.y / 2)
            {
                EyeBrushApplied(activeBrushIndex);
            }
            else
            {
                EventEmitter.OnEyeBrushApplyComplete();
            }
        }

        private void CreamApplied()
        {
            FaceCleanSprite.gameObject.SetActive(true);

            CreamApplyAnimation();
        }

        private void CreamApplyAnimation()
        {
            FaceCleanSprite.DOFade(0, 0);
            FaceCleanSprite.DOFade(1, CREAM_APPLY_DURATION_SEC).OnComplete(() =>
            {
                EventEmitter.OnCreamApplyComplete();
                IsCreamApplied = true;
            });
        }

        private void SpongeApplied()
        {
            FaceCleanSprite.gameObject.SetActive(false);
            IsCreamApplied = false;
        }
        
        private void EyeBrushApplied(int activeBrushIndex)
        {
            EyeBrushApplyAnimation(activeBrushIndex);
        }

        private void EyeBrushApplyAnimation(int activeBrushIndex)
        {
            for (var i = 0; i < EyeBrushList.Count; i++)
            {
                foreach (var brushSprite in EyeBrushList[i].BrushSprites)
                {
                    if (i == activeBrushIndex)
                    {
                        brushSprite.gameObject.SetActive(true);
                
                        brushSprite.DOFade(0, 0);
                        brushSprite.DOFade(1, EYE_BRUSH_APPLY_DURATION_SEC);
                    }
                    else
                    {
                        if (brushSprite.gameObject.activeSelf)
                        {
                            brushSprite.DOFade(0, EYE_BRUSH_APPLY_DURATION_SEC).OnComplete(() =>
                            {
                                brushSprite.gameObject.SetActive(false);
                            });
                        }
                    }
                }
            }

            transform.DOMove(transform.position, EYE_BRUSH_APPLY_DURATION_SEC)
                .OnComplete(EventEmitter.OnEyeBrushApplyComplete);
        }
    }
}
