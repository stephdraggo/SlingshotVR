using System;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using Wakaba.VR;
using TMPro;
using UnityEngine.UI;

namespace Bow
{
    public enum GameMode
    {
        Free,
        Frenzy,
        Strategic
    }
    public class GameControl : MonoBehaviour
    {
        public static GameControl instance;
        [SerializeField] private bool useVR;
        [SerializeField] private float timer;
        private float timerCount;
        
        public static GameMode gameMode = GameMode.Free;

        public static int score;
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private TMP_Text timerText;
        [SerializeField] private Text endScoreText;
        [SerializeField] private GameObject endPanel;
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
            if (gameMode == GameMode.Frenzy)
            {
                timerCount = timer;
            }
            else if (gameMode == GameMode.Free) timerText.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (gameMode != GameMode.Frenzy) return;
            if (timerCount > 0)
            {
                timerText.text = "Time: " + (int)timerCount;
                timerCount -= Time.deltaTime;
            }
            else
            {
               EndGame();
            }
        }

        public void SetArrowText(string _text)
        {
            timerText.text = _text;
        }

        [SerializeField] private Pause pause;

        public void EndGame()
        {
            pause.PauseGame(true);
            endScoreText.text = "Score: " + score;
        }

        public static void AddScore(int _amount)
        {
            score += _amount;
            if (instance.scoreText) instance.scoreText.text = "Score: " + score;
        }
    }
}
