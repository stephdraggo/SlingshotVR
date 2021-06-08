using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using Wakaba.VR;
using TMPro;

namespace Bow
{
    public class GameControl : MonoBehaviour
    {
        private static GameControl instance;
        [SerializeField] private bool useVR;

        private int score;
        [SerializeField] private TMP_Text scoreText;
        private void Awake()
        {
            if (useVR) VrUtils.SetVREnabled(true);
            if (!instance) instance = this;
            else Destroy(this);
        }

        private void Start()
        {
            score = 0;
            if (scoreText) scoreText.text = "Score: 0";
        }

        public static void AddScore(int _amount)
        {
            instance.score += _amount;
            if (instance.scoreText) instance.scoreText.text = "Score: " + instance.score;
        }
    }
}
