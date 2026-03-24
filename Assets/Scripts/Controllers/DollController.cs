using UnityEngine;

namespace DollMakeup.Controllers
{
    public class DollController : MonoBehaviour
    {
        [SerializeField] public SpriteRenderer FaceSprite;
        [SerializeField] public SpriteRenderer FaceCleanSprite;

        public Vector2 FacePosition => FaceSprite.transform.position;

        public void OnCreamEndDrag(Vector2 position)
        {
            Debug.Log("FacePosition = " + FacePosition);
            
            if (position.x > FaceSprite.transform.position.x - FaceSprite.size.x / 4
                && position.x < FaceSprite.transform.position.x + FaceSprite.size.x / 2
                && position.y > FaceSprite.transform.position.y - FaceSprite.size.y / 2
                && position.y < FaceSprite.transform.position.y + FaceSprite.size.y / 2)
            {
                CreamApplied();
            }
        }

        public void OnSpongeClicked()
        {
            SpongeApplied();
        }

        private void CreamApplied()
        {
            FaceCleanSprite.gameObject.SetActive(true);
        }

        private void SpongeApplied()
        {
            FaceCleanSprite.gameObject.SetActive(false);
        }
    }
}
