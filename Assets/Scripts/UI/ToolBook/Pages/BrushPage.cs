using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DollMakeup.UI.ToolBook.Pages
{
    public class BrushPage : MonoBehaviour
    {
        [SerializeField] private List<Button> ColorButtons;
        [SerializeField] private List<ColorPick> ColorPicks;
        [SerializeField] private EyeBrush Brush;

        private void Start()
        {
            for (var i = 0; i < ColorPicks.Count; i++)
            {
                var index = i;
                ColorPicks[i].OnColorPick += () => OnColorPick(index);
            }
        }

        private void OnColorPick(int index)
        {
            var colorPosition = ColorButtons[index].transform.position;
            Brush.BrushActivate(colorPosition);
        }
    }
}
