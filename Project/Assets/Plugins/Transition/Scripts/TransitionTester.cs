using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Plugins.Transition.Scripts
{
    public class TransitionTester : MonoBehaviour, ITransitionManager
    {
        Camera mainCamera;
        private void Start()
        {
            mainCamera = Camera.main;
            TransitionIn(TransitionType.Fade, 2f);
            // TransitionIn(new FadeTransition(), 2f);
        }

        public void TransitionIn(TransitionType transitionType, float time, Action onComplete = null)
        {
            transitionType.Duration = time;
            // transitionType.image = gameObject.AddComponent<Image>();
            transitionType.image.material.color = Color.black;

            var canvas = mainCamera.gameObject.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            var image = canvas.gameObject.AddComponent<Image>();
            image = transitionType.image;
            
            Delay();
            
            IEnumerator Delay()
            {
                yield return new WaitForSeconds(transitionType.Duration);
            }

            /*//perform the transition (eg.Fade In) over the duration (time)
            Debug.Log($"Transitioning In : {transitionType} over {time} seconds. OnComplete perform method {onComplete?.Method}");
            onComplete?.Invoke();*/
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