using System;
using System.Collections;
using UnityEngine;

namespace Plugins.Transitions.Scripts
{
    public abstract class TransitionBase : ScriptableObject
    {
        [SerializeField] private Material _material;
        [SerializeField] private float _duration;
        [SerializeField] private string _name;

        public Material Material => _material;
        public float Duration => _duration;
        public string Name => _name;

        public virtual IEnumerator Forward(Material material, float duration)
        {
            float progress = 1f;
            while (progress > 0f)
            {
                progress += Time.deltaTime / duration;
                material.SetFloat("_Progress", progress);
                yield return null;
            }
            material.SetFloat("_Progress", 0f);
        }

        public virtual IEnumerator Reverse(Material material, float duration)
        {
            float progress = 1f;
            while (progress > 0f)
            {
                progress += Time.deltaTime / duration;
                material.SetFloat("_Progress", progress);
                yield return null;
            }
            material.SetFloat("_Progress", 0f);
        }
    }
}

