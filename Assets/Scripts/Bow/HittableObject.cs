using UnityEngine;

namespace Bow
{
    public enum ArrowEffect
    {
        None,
        StickArrow,
        DestroyArrow
    }
    public class HittableObject : MonoBehaviour
    {
        [Tooltip("What should happen to the arrow when hitting this object")]

        [SerializeField] private ArrowEffect arrowEffect;
        public ArrowEffect ArrowEffect => arrowEffect;
        public virtual void OnArrowHit() {}
    }
}

