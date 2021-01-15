using System;
using System.Collections;
using UnityEngine;

namespace Plugins.Transitions.Scripts
{
    public interface ITransitionManager
    {
        void PerformTransition(Func<Material, float, IEnumerator> transition, Material m, float duration);
    }
}
