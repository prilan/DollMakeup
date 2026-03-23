using DollMakeup.Controllers;
using Utility;

namespace DollMakeup.Model
{
    public class GameModel : MonoSingleton<GameModel>
    {
        public GameController GameController;
    }
}
