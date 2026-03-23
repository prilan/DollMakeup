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
            var mousePos = AppModel.Instance.Camera.ScreenToWorldPoint(position);
            
            if (mousePos.x > FaceSprite.transform.position.x - FaceSprite.size.x / 2
                && mousePos.x < FaceSprite.transform.position.x + FaceSprite.size.x / 2
                && mousePos.y > FaceSprite.transform.position.y - FaceSprite.size.y / 2
                && mousePos.y < FaceSprite.transform.position.y + FaceSprite.size.y / 2)
            {
                Debug.Log("good position");
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
