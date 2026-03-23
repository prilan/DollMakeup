using UnityEngine;
using UnityEngine.UI;

namespace DollMakeup.UI
{
    public class Cream : MonoBehaviour
    {
        [SerializeField] private Button CreamButton;
        [SerializeField] private Image CreamImage;
        [SerializeField] private GameObject CreamTool;

        private void Start()
        {
            CreamButton.onClick.AddListener(OnCreamClicked);
        }

        private void OnCreamClicked()
        {
            CreamImage.gameObject.SetActive(false);
            CreamTool.SetActive(true);
        }
    }
}
