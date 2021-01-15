using System.Collections;
using UnityEngine;

namespace Plugins.Transitions.Scripts
{
    public interface ITransition
    {
        IEnumerator Forward(Material material, float duration);
        IEnumerator Reverse(Material material, float duration);
    }
}
