using System;
using System.Collections;
using Plugins.Transitions.Rendering;
using UnityEngine;

namespace Plugins.Transitions.Scripts
{
    public class TransitionManager : MonoBehaviour, ITransitionManager
    {
        public Material fadeMaterial;
        public Material pixelMaterial;
        
        public TransitionRenderPassFeature _transitionRender;

        public void PerformTransition<T>(Func<Material, float, T, IEnumerator> transition, Material material, float duration, T t)
        {
            Debug.Log("Generic transition started");
            _transitionRender.ChangeMaterial(material);
            StartCoroutine(transition(material, duration, t));
            Debug.Log("Generic transition ended");
        }
        
        public void PerformTransition(Func<Material, float, IEnumerator> transition, Material m, float duration)
        {
            _transitionRender.ChangeMaterial(pixelMaterial);
            StartCoroutine(transition(m, duration));
        }
        
        public void PixelateButton()
        {
            //_transitionRender.ChangeMaterial(pixelMaterial);
            PerformTransition(Pixelate, pixelMaterial, 3f);
            //StartCoroutine(Pixelate(pixelMaterial,3f));
        }
        
        public void DepixelateButton()
        {
            _transitionRender.ChangeMaterial(pixelMaterial);
            StartCoroutine(DePixelate(pixelMaterial,3f));
        }

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
        
        private IEnumerator DePixelate(Material material, float duration)
        {
            float pixelAmount = 1f;
            while (pixelAmount > 0f)
            {
                pixelAmount -= Time.deltaTime / duration;
                material.SetFloat("_PixelAmount", pixelAmount);
                yield return null;
            }
            material.SetFloat("_PixelAmount", 0f);
        }
        
        private IEnumerator Pixelate(Material material, float duration)
        {
            float pixelAmount = 0f;
            while (pixelAmount < 1f)
            {
                pixelAmount += Time.deltaTime / duration;
                material.SetFloat("_PixelAmount", pixelAmount);
                yield return null;
            }
            material.SetFloat("_PixelAmount", 1f);
        }
        
    }
}
