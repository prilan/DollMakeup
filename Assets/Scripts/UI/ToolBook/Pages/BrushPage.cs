using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DollMakeup.UI.ToolBook.Pages
{
    public class BrushPage : MonoBehaviour
    {
        [SerializeField] private List<Button> ColorButtons;
        [SerializeField] private EyeBrush Brush;

        private void Start()
        {
            for (var i = 0; i < ColorButtons.Count; i++)
            {
                var index = i;
                ColorButtons[i].onClick.AddListener(() => OnColorButtonClick(index));
            }
        }

        private void OnColorButtonClick(int index)
        {
            Debug.Log("Color clicked: " + index);

            var colorPosition = ColorButtons[index].transform.position;
            Brush.BrushActivate(colorPosition);
        }
    }
}
