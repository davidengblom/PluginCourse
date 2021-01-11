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

        public abstract void MakeTransition();

    }

    public class Fade : TransitionType
    {
        public override void MakeTransition()
        {
            Debug.Log("Amazing Fade Transition ermagherd");
        }
    }

    public class Pixelate : TransitionType
    {
        public override void MakeTransition()
        {
            Debug.Log("Amazing Pixelation Transition WoW");
        }
    }

    //public class Spiral : TransitionType { }
}