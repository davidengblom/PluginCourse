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

        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }

        // MVP
        // Make example second scene
        // Make transition to next scene method
        // Make readme
        // Example script
        // get rid of debug logs
        
        // export plugin
        // TEST!!! plugin in a clean project.
        // make presentation
        

        // EXTRA
            // Allow user to change fade type by method call.
            // make readme
            // refactor / abstract stuff in this class.
            // custom options such as
            // pause game
            // image stays
            // transition to another level
            // make transition by providing material and duration

        public void LoadSceneButton(string path)
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
            // PerformTransition(Pixelate, pixelMaterial, 3f);
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
            Debug.Log("End fade in");
        }

        IEnumerator LoadAsynchronously(string path)
        {
            AsyncOperation load = EditorSceneManager.LoadSceneAsyncInPlayMode(path, new LoadSceneParameters(LoadSceneMode.Single, LocalPhysicsMode.Physics3D)); // LocalPhysicsMode comes in more flavors.
            while (!load.isDone)
            {
                float progress = Mathf.Clamp01(load.progress / .9f);
                Debug.Log(progress);
                yield return null;
            }
        }
    }
}
