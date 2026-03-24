using System;
using System.Collections.Generic;
using UnityEngine;

namespace DollMakeup.UI.ToolBook
{
    public class ToolBookTabs : MonoBehaviour
    {
        [SerializeField] private List<ToolBookTab> Tabs;
        
        public Action<int> OnTabActiveChanged = (index) => { };

        private void Start()
        {
            for (var i = 0; i < Tabs.Count; i++)
            {
                var index = i;
                Tabs[i].OnTabActiveChanged += () => TabActiveChanged(index);
            }
        }

        private void TabActiveChanged(int index)
        {
            for (var i = 0; i < Tabs.Count; i++)
            {
                if (i != index)
                {
                    Tabs[i].SetActive(false);
                }
            }
            
            OnTabActiveChanged?.Invoke(index);
        }
    }
}
