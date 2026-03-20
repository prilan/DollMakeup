using UnityEngine;

namespace DollMakeup.Controllers
{
    public class InputController : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                
                
                Debug.Log("Clicked on " + Input.mousePosition);
                //EventEmitter.OnClickedOnItem(sprite.sprite.name);
            }

            /*if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
            }*/
        }
    }
}
