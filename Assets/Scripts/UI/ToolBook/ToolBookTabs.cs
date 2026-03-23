using System.Collections.Generic;
using UnityEngine;

namespace DollMakeup.UI.ToolBook
{
    public class ToolBookTabs : MonoBehaviour
    {

        [SerializeField] private List<ToolBookTab> Tabs;

        private void Start()
        {
            for (var i = 0; i < Tabs.Count; i++)
            {
                int index = i;
                Tabs[i].OnTabActiveChanged += () => OnTabActiveChanged(index);
            }
        }

        private void OnTabActiveChanged(int index)
        {
            for (var i = 0; i < Tabs.Count; i++)
            {
                if (i != index)
                {
                    Tabs[i].SetActive(false);
                }
            }
        }
    }
}
