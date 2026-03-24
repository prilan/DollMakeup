using System;
using Utility;

namespace DollMakeup
{
    public class EventEmitter  : AbstractSingleton<EventEmitter>
    {
        private event Action creamApplyComplete = () => { };

        /*****************************************************************************************/
        
        public static void OnCreamApplyComplete()
        {
            Instance.creamApplyComplete();
        }
        
        /*****************************************************************************************/
        
        public static event Action CreamApplyComplete
        {
            add => Instance.creamApplyComplete += value;
            remove => Instance.creamApplyComplete -= value;
        }
    }
}
