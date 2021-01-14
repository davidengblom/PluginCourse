using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Pixelation : MonoBehaviour
{
    private Image _image;
    private static readonly int Intensity = Shader.PropertyToID("_intensity");

    private void Start()
    {
        this._image = GetComponent<Image>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(PixelateTransition(true, 0.5f));
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(PixelateTransition(false, 0.5f));
        }
    }

    private IEnumerator PixelateTransition(bool pixelate = true, float speed = 2)
    {
        var pixelateValue = this._image.material.GetFloat(Intensity);
        if (pixelate)
        {
            while (pixelateValue < 1)
            {
                this._image.material.SetFloat(Intensity, pixelateValue += speed * Time.deltaTime);
                yield return null;
            }
        }
        else
        {
            while (pixelateValue > 0)
            {
                this._image.material.SetFloat(Intensity, pixelateValue -= speed * Time.deltaTime);
                yield return null;
            }
        }
    }
}
