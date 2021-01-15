using System.Collections.Generic;
using Plugins.Transitions.PremadeTransitions;
using UnityEngine;

namespace Plugins.Transitions.Scripts
{
    [RequireComponent(typeof(TransitionManager))]
    public class Transitions : MonoBehaviour
    {
        [SerializeField] private TransitionBase[] _transitions;
        private Dictionary<string, TransitionBase> _transitionLookup;
        
        private TransitionManager _manager;

        public void FadeToBlack()
        {
            FadeToColor(Color.black);
        }

        public void FadeFromBlack()
        {
            FadeFromColor(Color.black);
        }

        public void FadeToColor(Color color)
        {
            FadeToColor f = TransitionLookup("FadeToColor") as FadeToColor;
            if (f == null)
                Debug.LogWarning("Could not find transition type", this);
            _manager.PerformTransition<Color>(f.Forward, f.Material, f.Duration, color);
        }
        
        public void FadeToColor()
        {
            FadeToColor f = TransitionLookup("FadeToColor") as FadeToColor;
            if (f == null)
                Debug.LogWarning("Could not find transition type", this);
            _manager.PerformTransition<Color>(f.Forward, f.Material, f.Duration, f.Material.color); //Uses the material color
        }

        public void FadeFromColor(Color color)
        {
            FadeToColor f = TransitionLookup("FadeToColor") as FadeToColor;
            if (f == null)
                Debug.LogWarning("Could not find transition type", this);
            _manager.PerformTransition<Color>(f.Reverse, f.Material, f.Duration, color);
        }
        
        public void FadeFromColor()
        {
            FadeToColor f = TransitionLookup("FadeToColor") as FadeToColor;
            if (f == null)
                Debug.LogWarning("Could not find transition type", this);
            _manager.PerformTransition<Color>(f.Reverse, f.Material, f.Duration, f.Material.color); //Uses the material color
        }

        public void Pixelate()
        {
            
        }

        public void DePixelate()
        {
            
        }
        
        //Dissolve

        private TransitionBase TransitionLookup(string name)
        {
            TransitionBase b;
            if (_transitionLookup.TryGetValue(name, out b))
            {
                return b;
            }
            else
            {
                Debug.LogError("Transition not found among transitions", this);
                return null;
            }
        }
        
        private void Awake()
        {
            _manager = GetComponent<TransitionManager>();

            _transitionLookup = new Dictionary<string, TransitionBase>();
            foreach (var transition in _transitions)
            {
                _transitionLookup.Add(transition.Name, transition);
            }
        }
    }
}
