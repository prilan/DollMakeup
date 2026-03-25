using System.Collections.Generic;
using UnityEngine;

namespace DollMakeup.UI.ToolBook.Pages
{
    public class LipstickPage : MonoBehaviour
    {
        [SerializeField] private List<Lipstick> Lipsticks;

        private void Start()
        {
            for (var i = 0; i < Lipsticks.Count; i++)
            {
                var index = i;
                Lipsticks[i].InitIndex(index);
                Lipsticks[i].OnColorPick += () => OnColorPick(index);
            }
        }

        private void OnColorPick(int index)
        {
            var colorPosition = Lipsticks[index].transform.position;
            Lipsticks[index].BrushActivate(colorPosition, index);
        }
    }
}
