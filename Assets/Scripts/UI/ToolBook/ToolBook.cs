using UnityEngine;

namespace DollMakeup.UI.ToolBook
{
    public class ToolBook : MonoBehaviour
    {
        [SerializeField] private ToolBookTabs Tabs;
        [SerializeField] private ToolBookPages Pages;

        private void Start()
        {
            Tabs.OnTabActiveChanged += OnTabActiveChanged;
        }

        private void OnTabActiveChanged(int index)
        {
            Pages.SetActivePage(index);
        }
    }
}
