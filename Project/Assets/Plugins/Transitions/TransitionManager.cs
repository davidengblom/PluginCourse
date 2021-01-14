using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Plugins.Transition.Scripts
{
    public class TransitionManager : MonoBehaviour//, ITransitionManager
    {
        
        // make a transition using a shader effect on the image
        // render screenshot onto a 2d texture
        // apply the rendered texture onto the canvas image
        // make a transition using a shader effect on the image (e.g. pixelate)   
        // refactor / abstract stuff in this class.
        // custom options such as
        // pause game
        // image stays
        // transition to another level
        
        
        private GameObject transitionScreen;
        private Image image;
        private Sprite sprite;
        
        private static readonly int Intensity = Shader.PropertyToID("_intensity");

        [ContextMenu("FadeFromBlack")]
        public void FadeFromBlack()
        {
            CreateFaderObject();
            StartCoroutine(FadeFromBlack(1f));
        }
        
        [ContextMenu("FadeToBlack")]
        public void FadeToBlack()
        {
            CreateFaderObject();
            StartCoroutine(FadeToBlack(1f));
        }

        [ContextMenu("Take Screenshot")]
        public void TakeScreenShot()
        {
            StartCoroutine(ScreenCaptureToTexture());
        }

        [ContextMenu("PixelateIn")]
        public void PixelateIn()
        {
            StartCoroutine(PixelateTransition());
        }
        [ContextMenu("PixelateOut")]
        public void PixelateOut()
        {
            StartCoroutine(PixelateTransition(false));
        }
        
        private void CreateFaderObject()
        {
            transitionScreen = new GameObject();
            var tempCanvas = transitionScreen.AddComponent<Canvas>();
            tempCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
            image = tempCanvas.gameObject.AddComponent<Image>();
        }
        
        private IEnumerator FadeFromBlack(float Duration)
        {
            Color color = Color.black;
            image.material.color = color;
            color.a = 1f;
            while (image.material.color.a > 0)
            {
                color.a -= Time.deltaTime / Duration;
                image.material.color = color;
                yield return null;
            }
            color.a = 0f;
            CleanUp();
        }
        
        private IEnumerator FadeToBlack(float Duration)
        {
            Color color = Color.black;
            color.a = 0f;
            image.material.color = color;
            while (image.material.color.a < 1f)
            {
                color.a += Time.deltaTime / Duration;
                image.material.color = color;
                yield return null;
            }
            color.a = 1f;
            CleanUp();
        }
        
        IEnumerator ScreenCaptureToTexture()
        {
            yield return new WaitForEndOfFrame();
            
            CreateFaderObject();
            var texture = ScreenCapture.CaptureScreenshotAsTexture();
            
            //todo
            
            Debug.Log("Screen captured");
            
            //Destroy(texture);
        }
        private IEnumerator PixelateTransition(bool pixelate = true, float speed = 2)
        {
            CreateFaderObject();
            ApplyTexture(image.material);
            var pixelateValue = this.image.material.GetFloat(Intensity);
            if (pixelate)
            {
                while (pixelateValue < 1)
                {
                    this.image.material.SetFloat(Intensity, pixelateValue += speed * Time.deltaTime);
                    yield return null;
                }
            }
            else
            {
                while (pixelateValue > 0)
                {
                    this.image.material.SetFloat(Intensity, pixelateValue -= speed * Time.deltaTime);
                    yield return null;
                }
            }
        }

        private void ApplyTexture(Material material)
        {
            //todo
            material.SetTexture("_texture", image.material.mainTexture);
        }

        private void CleanUp() => Destroy(transitionScreen);
    }
}