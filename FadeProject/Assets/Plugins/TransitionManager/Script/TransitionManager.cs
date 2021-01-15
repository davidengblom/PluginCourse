using System;
using System.Collections;
using Plugins.Transitions.Rendering;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Plugins.TransitionManager.Script
{
    public class TransitionManager : MonoBehaviour
    {
        public TransitionRenderPassFeature _transitionRender;
        public Material fadeMaterial;
        public Material pixelMaterial;

        [Header("Play transition on start")]
        [Tooltip("A transition is performed on start")]public bool playTransitionOnStart = false;
        public Material startTransitionMaterial;
        public float startTransitionDuration = 1f;
        [Tooltip("Toggles the type of transition. Set to true to transition in")] public bool startTransitionIn = true;

        // EXTRA TODO
            // Allow user to change fade type by method call.
            // refactor / abstract stuff in this class.
            // custom options such as
            // pause game
            // optional if image stays
            // transition to another level
            // make transition by providing material and duration

            private void Start()
        {
            if (playTransitionOnStart)
            {
                if (startTransitionMaterial == null)
                {
                    Debug.LogWarning("Must provide startmaterial for start fade to function. Using fadeMaterial by default.");
                    startTransitionMaterial = fadeMaterial;
                }
                PlayDefaultTransition(startTransitionMaterial, startTransitionDuration, startTransitionIn);
            }
        }
        
        public void PlayDefaultTransition(Material material, float duration = 1f, bool transitionIn = false)
        {
            if (transitionIn)
            {
                PlayCustomTransition(TransitionIn, material, duration);
            }
            else
            {
                PlayCustomTransition(TransitionOut, material, duration);
            }
        }

        public void PlayCustomTransition(Func<Material, float, IEnumerator> transition, Material material, float duration)
        {
            if (material == null)
            {
                Debug.LogError("No material found, not playing transition. Please provide start material");
                return;
            }
            _transitionRender.ChangeMaterial(material);
            StartCoroutine(transition(material, duration));
        }

        private IEnumerator TransitionOutAndSwitchLevel(string path, float duration = 3f)
        {
            if (fadeMaterial == null)
            {
                Debug.LogError("Material not found");
                yield return null;
            }
            yield return StartCoroutine(TransitionOut(fadeMaterial, duration));
            StartCoroutine(LoadAsynchronously(path));
        }

        public void LoadSceneWithTransition(string path)
        {
            StartCoroutine(TransitionOutAndSwitchLevel(path));
        }

        public void LoadSceneWithoutTransition(string path)
        {
            StartCoroutine(LoadAsynchronously(path));
        }

        public void FadeIn(float duration = 1f)
        {
            _transitionRender.ChangeMaterial(fadeMaterial);
            StartCoroutine(TransitionIn(fadeMaterial, duration));
        }
        
        public void FadeOut(float duration = 1f)
        {
            _transitionRender.ChangeMaterial(fadeMaterial);
            StartCoroutine(TransitionOut(fadeMaterial,duration));
        }

        public void PixelateIn(float duration = 1f)
        {
            _transitionRender.ChangeMaterial(pixelMaterial);
            StartCoroutine(TransitionIn(pixelMaterial,duration));
        }
        
        public void PixelateOut(float duration = 1f)
        {
            _transitionRender.ChangeMaterial(pixelMaterial);
            StartCoroutine(TransitionOut(pixelMaterial,duration));
        }
        
        private IEnumerator TransitionOut(Material material, float duration)
        {
            float intensity = 0f;
            while (intensity < 1f)
            {
                intensity += Time.deltaTime / duration;
                material.SetFloat("_Intensity", intensity);
                yield return null;
            }
            material.SetFloat("_Intensity", 1f);
        }
        
        private IEnumerator TransitionIn(Material material, float duration)
        {
            Debug.Log("Start fade in");
            float intensity = 1f;
            while (intensity > 0f)
            {
                intensity -= Time.deltaTime / duration;
                material.SetFloat("_Intensity", intensity);
                yield return null;
            }
            material.SetFloat("_Intensity", 0f);
        }

        IEnumerator LoadAsynchronously(string path)
        {
            AsyncOperation load = EditorSceneManager.LoadSceneAsyncInPlayMode(path, new LoadSceneParameters(LoadSceneMode.Single, LocalPhysicsMode.Physics3D)); // LocalPhysicsMode comes in more flavors.
            while (!load.isDone)
            {
                float progress = Mathf.Clamp01(load.progress / .9f);
                yield return null;
            }
        }
    }
}
