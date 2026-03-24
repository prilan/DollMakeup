using DG.Tweening;
using UnityEngine;

namespace DollMakeup.Controllers
{
    public class DollController : MonoBehaviour
    {
        [SerializeField] public SpriteRenderer FaceSprite;
        [SerializeField] public SpriteRenderer FaceCleanSprite;

        private const float CREAM_APPLY_DURATION_SEC = 0.3f;

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
    }
}
