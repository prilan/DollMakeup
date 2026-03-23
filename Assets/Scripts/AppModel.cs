using DollMakeup.Model;
using UnityEngine;
using Utility;

namespace DollMakeup
{
    public class AppModel : AbstractSingleton<AppModel>
    {
        public void OnCreamEndDrag(Vector2 position)
        {
            GameModel.Instance.GameController.DollController.OnCreamEndDrag(position);
        }
    }
}
