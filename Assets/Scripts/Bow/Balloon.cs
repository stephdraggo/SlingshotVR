using UnityEngine;

namespace Bow
{
    public class Balloon : HittableObject
    {
        [SerializeField] private int points;
        public override void OnArrowHit(Arrow _arrow) => Pop();

        private void Pop()
        {
            if (Wakaba.VR.VrUtils.IsVREnabled() == true) GameControl.AddScore(points);
            else GameControlPC.AddScore(points);
            Destroy(gameObject);
        }
    }
}

