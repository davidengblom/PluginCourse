using System;

namespace Plugins.Transition.Scripts
{
    public interface ITransitionManager
    {
        public void TransitionIn(TransitionType transitionType, float time, Action onComplete);
        public void TransitionOut(float time, Action onComplete);
    }
}