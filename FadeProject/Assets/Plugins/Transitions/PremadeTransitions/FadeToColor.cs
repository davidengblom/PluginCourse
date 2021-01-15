using System.Collections;
using Plugins.Transitions.Scripts;
using UnityEngine;

namespace Plugins.Transitions.PremadeTransitions
{
    [CreateAssetMenu(fileName = "FadeToColor", menuName = "ScriptableObjects/Transitions/FadeToColor")]
    public class FadeToColor: TransitionBase
    {
        private const string FloatRef = "_Progress";
        
        //IEnumerator ForwardSimple => ()=> this.Forward(this.Material)
        public virtual IEnumerator Forward(Material material, float duration, Color color)
        {
            float progress = 0f;
            while (progress < 1f)
            {
                progress += Time.deltaTime / duration;
                material.SetFloat(FloatRef, progress);
                yield return null;
            }
            material.SetFloat(FloatRef, 1f);
        }

        public virtual IEnumerator Reverse(Material material, float duration, Color color)
        {
            float progress = 1f;
            while (progress > 0f)
            {
                progress -= Time.deltaTime / duration;
                material.SetFloat(FloatRef, progress);
                yield return null;
            }
            material.SetFloat(FloatRef, 0f);
        }
    }
}
