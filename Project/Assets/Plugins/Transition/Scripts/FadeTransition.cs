using System;
using System.Collections;
using System.Collections.Generic;
using Plugins.Transition.Scripts;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class FadeTransition : TransitionType
{

    protected override void MakeTransition()
    {
        
    }

    /*public IEnumerator FadeOut(float time)
    {
        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += Time.deltaTime / time;
            yield return null;
        }
    }
    
    public IEnumerator FadeIn(float time)
    {
        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime / time;
            yield return null;
        }
    }*/
}
