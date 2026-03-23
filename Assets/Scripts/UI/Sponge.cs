using UnityEngine;
using UnityEngine.UI;

namespace DollMakeup.UI
{
    public class Sponge : MonoBehaviour
    {
        [SerializeField] private Button SpongeButton;

        private void Start()
        {
            SpongeButton.onClick.AddListener(OnSpongeClicked);
        }

        private void OnSpongeClicked()
        {
            AppModel.Instance.OnSpongeClicked();
        }
    }
}
