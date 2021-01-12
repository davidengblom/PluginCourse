using UnityEngine;

namespace Plugins.Transition.Scripts
{
    public abstract class TransitionType
    {
        public float Duration { get; set; }
        
        protected TransitionType()
        {
           MakeTransition();
        }

        protected abstract void MakeTransition();

        public static TransitionType Fade => new FadeTransition();
    }

    public class FadeTransition : TransitionType
    {
        protected override void MakeTransition()
        {
            Debug.Log("Amazing Fade Transition ermagherd");
        }
    }

    public class PixelateTransition : TransitionType
    {
        protected override void MakeTransition()
        {
            Debug.Log("Amazing Pixelation Transition WoW");
        }
    }
    
    
    //public class SpiralTransition : TransitionType { }
}