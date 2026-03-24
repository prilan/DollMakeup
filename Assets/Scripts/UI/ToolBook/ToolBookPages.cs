using System.Collections.Generic;
using UnityEngine;

namespace DollMakeup.UI.ToolBook
{
    public class ToolBookPages : MonoBehaviour
    {
        [SerializeField] private List<GameObject> Pages;

        public void SetActivePage(int index)
        {
            if (Pages != null && index < Pages.Count)
            {
                for (var i = 0; i < Pages.Count; i++)
                {
                    Pages[i].SetActive(i == index);
                }
            }
        }
    }
}
