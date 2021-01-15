using System.Collections;
using Plugins.Transitions.Rendering;
using UnityEngine;

namespace Plugins.NewTransitionManager
{
    public class NewTransitionManager : MonoBehaviour
    {
        public Material fadeMaterial;
        public Material pixelMaterial;

        public TransitionRenderPassFeature _transitionRender;
    
        public void FadeOut()
        {
            _transitionRender.ChangeMaterial(fadeMaterial);
            StartCoroutine(FadeOut(fadeMaterial,3f));
        }
        
        public void FadeIn()
        {
            _transitionRender.ChangeMaterial(fadeMaterial);
            StartCoroutine(FadeIn(fadeMaterial, 3f));
        }
    
        private IEnumerator FadeIn(Material material, float duration)
        {
            Debug.Log("Start fade in");
            float fadeAmount = 1f;
            while (fadeAmount > 0f)
            {
                fadeAmount -= Time.deltaTime / duration;
                material.SetFloat("_Progress", fadeAmount);
                yield return null;
            }
            material.SetFloat("_Progress", 0f);
            Debug.Log("End fade in");
        }
        
        private IEnumerator FadeOut(Material material, float duration)
        {
            Debug.Log("Start fade out");
            float fadeAmount = 0f;
            while (fadeAmount < 1f)
            {
                fadeAmount += Time.deltaTime / duration;
                material.SetFloat("_Progress", fadeAmount);
                yield return null;
            }
            material.SetFloat("_Progress", 1f);
            Debug.Log("End fade out");
        }
    }
}
