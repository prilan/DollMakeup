using System;
using Utility;

namespace DollMakeup
{
    public class EventEmitter  : AbstractSingleton<EventEmitter>
    {
        private event Action creamApplyComplete = () => { };
        private event Action eyeBrushApplyComplete = () => { };
        private event Action blushBrushApplyComplete = () => { };

        /*****************************************************************************************/
        
        public static void OnCreamApplyComplete()
        {
            Instance.creamApplyComplete();
        }
        
        public static void OnEyeBrushApplyComplete()
        {
            Instance.eyeBrushApplyComplete();
        }
        
        public static void OnBlushBrushApplyComplete()
        {
            Instance.blushBrushApplyComplete();
        }
        
        /*****************************************************************************************/
        
        public static event Action CreamApplyComplete
        {
            add => Instance.creamApplyComplete += value;
            remove => Instance.creamApplyComplete -= value;
        }
        
        public static event Action EyeBrushApplyComplete
        {
            add => Instance.eyeBrushApplyComplete += value;
            remove => Instance.eyeBrushApplyComplete -= value;
        }
        
        public static event Action BlushBrushApplyComplete
        {
            add => Instance.blushBrushApplyComplete += value;
            remove => Instance.blushBrushApplyComplete -= value;
        }
    }
}
