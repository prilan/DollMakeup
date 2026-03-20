using System;
using Utility;

namespace DollMakeup
{
    public class EventEmitter  : AbstractSingleton<EventEmitter>
    {
        private event Action<string> clickedOnItem = (item) => { };

        /*****************************************************************************************/
        
        public static void OnClickedOnItem(string item)
        {
            Instance.clickedOnItem(item);
        }
        
        /*****************************************************************************************/
        
        public static event Action<string> ClickedOnItem
        {
            add { Instance.clickedOnItem += value; }
            remove { Instance.clickedOnItem -= value; }
        }
    }
}
