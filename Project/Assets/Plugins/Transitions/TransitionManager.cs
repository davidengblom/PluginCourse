using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Plugins.Transition.Scripts
{
    public class TransitionManager : MonoBehaviour//, ITransitionManager
    {
        private GameObject transitionScreen;
        private Image image;
        private Sprite sprite;

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
            StartCoroutine(ScreenCaptureToSprite());
        }
        
        IEnumerator ScreenCaptureToSprite()
        {
            yield return new WaitForEndOfFrame();
            var texture = ScreenCapture.CaptureScreenshotAsTexture();
                
            //sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
            transitionScreen = new GameObject();
            var tempCanvas = transitionScreen.AddComponent<Canvas>();
            tempCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
            var image = tempCanvas.gameObject.AddComponent<RawImage>();
            //texture.Apply();
            if (texture == null)
                Debug.Log("texture null");
            image.texture = texture;
            //image.sprite = sprite;
            Debug.Log("Screen captured");
            
            //Destroy(texture);
        }
        
        // make a transition using a shader effect on the image
        // render screenshot onto a 2d texture
            // apply the rendered texture onto the canvas image
                // make a transition using a shader effect on the image (e.g. pixelate)   
        // refactor / abstract stuff in this class.
        // custom options such as
            // pause game
            // image stays
            // transition to another level
            
        
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

        private void CleanUp()
        {
            Destroy(transitionScreen);
        }
    }
}