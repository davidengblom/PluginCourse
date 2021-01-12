using System;
using UnityEngine;

namespace Plugins.Transition.Scripts
{
    public class TransitionTester : MonoBehaviour, ITransitionManager
    {
        private void Start()
        {
            TransitionIn(new FadeTransition(), 2f, 
                () => TransitionIn(new PixelateTransition(), 3f, ExampleMethod));
        }

        public void TransitionIn(TransitionType transitionType, float time, Action onComplete = null)
        {
            //perform the transition (eg.Fade In) over the duration (time)
            Debug.Log($"Transitioning In : {transitionType} over {time} seconds. OnComplete perform method {onComplete?.Method}");
            onComplete?.Invoke();
        }
        
        public void TransitionOut(float time, Action onComplete)
        {
            throw new NotImplementedException();
        }
        
        private void ExampleMethod()
        {
            Debug.Log("Example Method called");
        }
    }
}