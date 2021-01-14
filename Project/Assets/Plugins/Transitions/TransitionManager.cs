﻿﻿using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Plugins.Transition.Scripts
{
    public class TransitionManager : MonoBehaviour//, ITransitionManager
    {
        // refactor / abstract stuff in this class.
        // custom options such as
        // pause game
        // image stays
        // transition to another level
        
        private GameObject transitionScreen;
        private Image image;

        public Shader transition;
        private Material mat;
        private Texture2D texture;
        
        private static readonly int Intensity = Shader.PropertyToID("_intensity");
        private static readonly int Texture = Shader.PropertyToID("_texture");

        [ContextMenu("Start Transition")]
        public void StartTransition()
        {
            StartCoroutine(ScreenCaptureToTexture());
        }

        private void CreateFaderObject()
        {
            transitionScreen = new GameObject();
            var tempCanvas = transitionScreen.AddComponent<Canvas>();
            tempCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
            image = tempCanvas.gameObject.AddComponent<Image>();
        }
        
        IEnumerator ScreenCaptureToTexture()
        {
            CreateFaderObject();
            this.image.material = this.mat;
            this.image.material.shader = this.transition;
            this.image.material.SetFloat(Intensity, 0); 

            yield return new WaitForEndOfFrame();
            texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
            texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0, false);
            texture.Apply();
            this.image.material.SetTexture(Texture, texture);
            
            StartCoroutine(DoTransition(true, 0.5f));
        }
        
        private IEnumerator DoTransition(bool direction = true, float speed = 2)
        {
            var value = this.image.material.GetFloat(Intensity);
            if (direction)
            {
                while (value < 1)
                {
                    this.image.material.SetFloat(Intensity, value += speed * Time.deltaTime);
                    yield return null;
                }
            }
            else
            {
                while (value > 0)
                {
                    this.image.material.SetFloat(Intensity, value -= speed * Time.deltaTime);
                    yield return null;
                }
            }

            Cleanup();
        }

        private void Cleanup()
        {
            Destroy(transitionScreen);
            Destroy(texture);
        }

        // [ContextMenu("FadeFromBlack")]
        // public void FadeFromBlack()
        // {
        //     CreateFaderObject();
        //     StartCoroutine(FadeFromBlack(1f));
        // }
        //
        // [ContextMenu("FadeToBlack")]
        // public void FadeToBlack()
        // {
        //     CreateFaderObject();
        //     StartCoroutine(FadeToBlack(1f));
        // }
        //
        //
        // [ContextMenu("PixelateIn")]
        // public void PixelateIn()
        // {
        //     StartCoroutine(PixelateTransition());
        // }
        // [ContextMenu("PixelateOut")]
        // public void PixelateOut()
        // {
        //     StartCoroutine(PixelateTransition(false));
        // }

        // private void ApplyTexture(Material material)
        // {
        //     //todo
        //     material.SetTexture("_texture", image.material.mainTexture);
        // }

        // private void CleanUp() => Destroy(transitionScreen);
        
        // private IEnumerator FadeToBlack(float Duration)
        // {
        //     Color color = Color.black;
        //     color.a = 0f;
        //     image.material.color = color;
        //     while (image.material.color.a < 1f)
        //     {
        //         color.a += Time.deltaTime / Duration;
        //         image.material.color = color;
        //         yield return null;
        //     }
        //     color.a = 1f;
        //     CleanUp();
        // }
        
        // private IEnumerator FadeFromBlack(float Duration)
        // {
        //     Color color = Color.black;
        //     image.material.color = color;
        //     color.a = 1f;
        //     while (image.material.color.a > 0)
        //     {
        //         color.a -= Time.deltaTime / Duration;
        //         image.material.color = color;
        //         yield return null;
        //     }
        //     color.a = 0f;
        //     CleanUp();
        // }
    }
}