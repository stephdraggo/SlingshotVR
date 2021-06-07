using UnityEngine;
using Wakaba.VR;

namespace Bow
{
    public class GameControl : MonoBehaviour
    {
        private static GameControl instance;
        [SerializeField] private bool useVR;

        //serialized for testing
        [SerializeField] private int score;
        private void Awake()
        {
            VrUtils.SetVREnabled(useVR);
            if (!instance) instance = this;
            else Destroy(this);
        }

        private void Start()
        {
            score = 0;
        }

        public static void AddScore(int _amount)
        {
            instance.score += _amount;
        }
    }
}
