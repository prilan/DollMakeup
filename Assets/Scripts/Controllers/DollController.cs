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
        [SerializeField] public List<SpriteRenderer> BlushSprites;
        [SerializeField] public List<SpriteRenderer> LipstickSprites;

        private const float CREAM_APPLY_DURATION_SEC = 0.3f;
        
        private const float EYE_BRUSH_APPLY_DURATION_SEC = 1.0f;
        private const float EYE_BRUSH_END_DELAY_SEC = 0.4f;
        
        private const float BLUSH_BRUSH_APPLY_DURATION_SEC = 1.0f;
        private const float BLUSH_BRUSH_END_DELAY_SEC = 0.4f;

        public Vector2 FacePosition => FaceSprite.transform.position;

        private bool IsCreamApplied;

        public void OnCreamEndDrag(Vector2 position)
        {
            if (!IsCreamApplied && IsOnFace(position))
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
            if (IsOnFace(position))
            {
                EyeBrushApplied(activeBrushIndex);
            }
            else
            {
                EventEmitter.OnEyeBrushApplyComplete();
            }
        }

        public void OnBlushBrushEndDrag(Vector2 position, int activeBrushIndex)
        {
            if (IsOnFace(position))
            {
                BlushBrushApplied(activeBrushIndex);
            }
            else
            {
                EventEmitter.OnBlushBrushApplyComplete();
            }
        }
        
        public void OnLipstickEndDrag(Vector2 position, int activeBrushIndex)
        {
            if (IsOnFace(position))
            {
                LipstickApplied(activeBrushIndex);
            }
            else
            {
                EventEmitter.OnLipstickApplyComplete();
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
            
            foreach (var brushSprites in EyeBrushList)
            {
                foreach (var brushSprite in brushSprites.BrushSprites)
                {
                    brushSprite.DOFade(0, 0);
                    brushSprite.gameObject.SetActive(false);
                }
            }
            
            foreach (var brushSprite in BlushSprites)
            {
                brushSprite.DOFade(0, 0);
                brushSprite.gameObject.SetActive(false);
            }
            
            foreach (var lipstickSprite in LipstickSprites)
            {
                lipstickSprite.DOFade(0, 0);
                lipstickSprite.gameObject.SetActive(false);
            }
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

            transform.DOMove(transform.position, EYE_BRUSH_APPLY_DURATION_SEC + EYE_BRUSH_END_DELAY_SEC)
                .OnComplete(EventEmitter.OnEyeBrushApplyComplete);
        }
        
        private void BlushBrushApplied(int activeBrushIndex)
        {
            BlushBrushApplyAnimation(activeBrushIndex);
        }

        private void BlushBrushApplyAnimation(int activeBrushIndex)
        {
            for (var i = 0; i < BlushSprites.Count; i++)
            {
                if (i == activeBrushIndex)
                {
                    BlushSprites[i].gameObject.SetActive(true);

                    BlushSprites[i].DOFade(0, 0);
                    BlushSprites[i].DOFade(1, BLUSH_BRUSH_APPLY_DURATION_SEC);
                }
                else
                {
                    if (BlushSprites[i].gameObject.activeSelf)
                    {
                        var index = i;
                        BlushSprites[i].DOFade(0, BLUSH_BRUSH_APPLY_DURATION_SEC).OnComplete(() =>
                        {
                            BlushSprites[index].gameObject.SetActive(false);
                        });
                    }
                }
            }

            transform.DOMove(transform.position, BLUSH_BRUSH_APPLY_DURATION_SEC + BLUSH_BRUSH_END_DELAY_SEC)
                .OnComplete(EventEmitter.OnBlushBrushApplyComplete);
        }
        
        private void LipstickApplied(int activeBrushIndex)
        {
            LipstickApplyAnimation(activeBrushIndex);
        }

        private void LipstickApplyAnimation(int activeBrushIndex)
        {
            for (var i = 0; i < LipstickSprites.Count; i++)
            {
                if (i == activeBrushIndex)
                {
                    LipstickSprites[i].gameObject.SetActive(true);

                    LipstickSprites[i].DOFade(0, 0);
                    LipstickSprites[i].DOFade(1, BLUSH_BRUSH_APPLY_DURATION_SEC);
                }
                else
                {
                    if (LipstickSprites[i].gameObject.activeSelf)
                    {
                        var index = i;
                        LipstickSprites[i].DOFade(0, BLUSH_BRUSH_APPLY_DURATION_SEC).OnComplete(() =>
                        {
                            LipstickSprites[index].gameObject.SetActive(false);
                        });
                    }
                }
            }

            transform.DOMove(transform.position, BLUSH_BRUSH_APPLY_DURATION_SEC + BLUSH_BRUSH_END_DELAY_SEC) // TODO
                .OnComplete(EventEmitter.OnLipstickApplyComplete);
        }
        
        private bool IsOnFace(Vector2 position)
        {
            return position.x > FaceSprite.transform.position.x - FaceSprite.size.x / 4
                   && position.x < FaceSprite.transform.position.x + FaceSprite.size.x / 2
                   && position.y > FaceSprite.transform.position.y - FaceSprite.size.y / 2
                   && position.y < FaceSprite.transform.position.y + FaceSprite.size.y / 2;
        }
    }
}
