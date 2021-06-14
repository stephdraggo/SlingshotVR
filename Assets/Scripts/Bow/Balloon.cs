using UnityEngine;

namespace Bow
{
    public class Balloon : HittableObject
    {
        [SerializeField] private int points;
        public override void OnArrowHit(Arrow _arrow) => Pop();

        private void Pop()
        {
            GameControl.AddScore(points);
            Destroy(gameObject);
        }
    }
}

