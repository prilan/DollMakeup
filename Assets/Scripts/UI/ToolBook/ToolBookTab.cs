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

        private bool IsActive;

        private void Start()
        {
            Debug.Log("ToolBookTab Start");
            TabButton.onClick.AddListener(OnTabClicked);
        }

        private void OnDestroy()
        {
            
        }

        public void SetActive(bool isActive)
        {
            IsActive = isActive;
            ActiveImage.gameObject.SetActive(isActive);
            InactiveImage.gameObject.SetActive(!isActive);
        }
        
        private void OnTabClicked()
        {
            Debug.Log("ToolBookTab OnTabClicked");
            SetActive(!IsActive);
        }
    }
}
