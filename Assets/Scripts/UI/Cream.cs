using DollMakeup.Tools;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DollMakeup.UI
{
    public class Cream : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private Button CreamButton;
        [SerializeField] private Image CreamImage;
        [SerializeField] private TextMeshProUGUI Text;
        [SerializeField] private GameObject CreamTool;

        private Vector2 StartPosition;
        private MovableTool CreamMove;

        private void Start()
        {
            StartPosition = ((Vector2) AppModel.Instance.Camera.ScreenToWorldPoint(CreamImage.transform.position) +
                             AppModel.Instance.FacePosition) / 2;
            CreamMove = GetComponent<MovableTool>();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnCreamClicked();
            
            CreamMove.StartDrag(CreamTool);
        }
        
        private void OnCreamClicked()
        {
            CreamImage.enabled = false;
            Text.enabled = false;
            
            CreamTool.SetActive(true);
            CreamTool.transform.position = StartPosition;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            CreamMove.EndDrag();
            
            AppModel.Instance.OnCreamEndDrag(eventData.position);
            
            CreamImage.enabled = true;
            Text.enabled = true;
            
            CreamTool.SetActive(false);
        }
    }
}
