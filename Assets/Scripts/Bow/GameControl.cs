using UnityEngine;

namespace Bow
{
    public class GameControl : MonoBehaviour
    {
        private static GameControl instance;

        //serialized for testing
        [SerializeField] private int score;
        private void Awake()
        {
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
