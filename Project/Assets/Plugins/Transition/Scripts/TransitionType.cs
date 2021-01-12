using UnityEngine;
using UnityEngine.UI;

namespace Plugins.Transition.Scripts
{
    public abstract class TransitionType 
    {
        public float Duration { get; set; }
        public Image image { get; set; }
        
        protected TransitionType()
        {
           MakeTransition();
        }

        protected abstract void MakeTransition();

        public static TransitionType Fade => new FadeTransition();
        public static TransitionType DeLog => new DebugLogTransition();
    }

    public class DebugLogTransition : TransitionType
    {
        protected override void MakeTransition()
        {
            
            Debug.Log("Amazing Pixelation Transition WoW");
        }
    }
    
    
    //public class SpiralTransition : TransitionType { }
}