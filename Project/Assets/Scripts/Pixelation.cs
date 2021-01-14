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

    // private void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.E))
    //     {
    //         StartCoroutine(PixelateTransition(true, 0.5f));
    //     }
    //
    //     if (Input.GetKeyDown(KeyCode.R))
    //     {
    //         StartCoroutine(PixelateTransition(false, 0.5f));
    //     }
    // }

}
