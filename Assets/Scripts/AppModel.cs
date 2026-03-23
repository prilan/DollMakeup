using DollMakeup.Model;
using UnityEngine;
using Utility;

namespace DollMakeup
{
    public class AppModel : AbstractSingleton<AppModel>
    {
        public Vector2 FacePosition => GameModel.Instance.GameController.DollController.FacePosition;
        public Camera Camera => GameModel.Instance.GameController.Camera;

        public void OnCreamEndDrag(Vector2 position)
        {
            GameModel.Instance.GameController.DollController.OnCreamEndDrag(position);
        }

        public void OnSpongeClicked()
        {
            GameModel.Instance.GameController.DollController.OnSpongeClicked();
        }
    }
}
