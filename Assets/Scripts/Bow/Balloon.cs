using UnityEngine;

namespace Bow
{
    public class Balloon : HittableObject
    {
        [SerializeField] private int points;
        public override void OnArrowHit() => Pop();

        private void Pop()
        {
            GameControl.AddScore(points);
            Destroy(gameObject);
        }
    }
}

