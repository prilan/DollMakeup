using UnityEngine;

namespace DollMakeup.Controllers
{
    public class DollController : MonoBehaviour
    {
        [SerializeField] public SpriteRenderer FaceSprite;
        [SerializeField] public SpriteRenderer FaceCleanSprite;

        public void OnCreamEndDrag(Vector2 position)
        {
            if (Camera.main != null)
            {
                var mousePos = Camera.main.ScreenToWorldPoint(position);
                
                if (mousePos.x > FaceSprite.transform.position.x - FaceSprite.size.x / 2
                    && mousePos.x < FaceSprite.transform.position.x + FaceSprite.size.x / 2
                    && mousePos.y > FaceSprite.transform.position.y - FaceSprite.size.y / 2
                    && mousePos.y < FaceSprite.transform.position.y + FaceSprite.size.y / 2)
                {
                    Debug.Log("good position");
                    CreamApplied();
                }
            }
        }

        private void CreamApplied()
        {
            FaceCleanSprite.gameObject.SetActive(true);
        }
    }
}
