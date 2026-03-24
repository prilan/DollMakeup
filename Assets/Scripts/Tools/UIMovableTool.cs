using UnityEngine;

namespace DollMakeup.Tools
{
    public class UIMovableTool : MovableTool
    {
        protected override Vector3 GetMousePosition(Vector3 position)
        {
            return position;
        }
    }
}
