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
        
        private static readonly int Intensity = Shader.PropertyToID("_intensity");
        private static readonly int Texture = Shader.PropertyToID("_texture");

        public int width = 1920, height = 1080;

        [ContextMenu("Start Transition")]
        public void StartTransition()
        {
            StartCoroutine(ScreenCaptureToTexture());
        }

        private void Start()
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

            this.image.material.SetTexture(Texture, ScreenshotToTexture2D());

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
            Destroy(this.transitionScreen);
        }

        public Texture2D ScreenshotToTexture2D()
        {
            Camera cam = GetComponent<Camera>();

            // Create Render Texture with width and height.
            RenderTexture rt = new RenderTexture(width, height, 0, RenderTextureFormat.ARGB32);
        
            // Assign Render Texture to camera.
            cam.targetTexture = rt;

            // save current background settings of the camera
            CameraClearFlags clearFlags = cam.clearFlags;
            Color backgroundColor = cam.backgroundColor;

            // Render the camera's view to the Target Texture.
            cam.Render();

            // Save the currently active Render Texture so we can override it.
            RenderTexture currentRT = RenderTexture.active;

            // ReadPixels reads from the active Render Texture.
            RenderTexture.active = cam.targetTexture;

            // Make a new texture and read the active Render Texture into it.
            Texture2D screenshot = new Texture2D(width, height, TextureFormat.ARGB32, false);
            screenshot.ReadPixels(new Rect(0, 0, width, height), 0, 0, false);

            // PNGs should be sRGB so convert to sRGB color space when rendering in linear.
            if(QualitySettings.activeColorSpace == ColorSpace.Linear) {
                Color[] pixels = screenshot.GetPixels();
                for(int p = 0; p < pixels.Length; p++) {
                    pixels[p] = pixels[p].gamma;
                }
                screenshot.SetPixels(pixels);
            }

            // Apply the changes to the screenshot texture.
            screenshot.Apply(false);
            return screenshot;
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