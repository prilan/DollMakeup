using System;
using UnityEngine;
using UnityEngine.UI;

namespace DollMakeup.UI.ToolBook
{
    public class ToolBookTab : MonoBehaviour
    {
        [SerializeField] private Button TabButton;
        [SerializeField] private Image ActiveImage;
        [SerializeField] private Image InactiveImage;

        public Action OnTabActiveChanged = () => { };

        private bool IsActive;

        private void Start()
        {
            TabButton.onClick.AddListener(OnTabClicked);
        }

        public void SetActive(bool isActive)
        {
            IsActive = isActive;
            ActiveImage.gameObject.SetActive(isActive);
            InactiveImage.gameObject.SetActive(!isActive);
            
            if (isActive)
                OnTabActiveChanged?.Invoke();
        }
        
        private void OnTabClicked()
        {
            if (IsActive)
                return;
            
            SetActive(!IsActive);
        }
    }
}
