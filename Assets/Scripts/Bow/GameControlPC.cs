using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Wakaba.VR;
using Bow;
public class GameControlPC : MonoBehaviour
{
    public static GameControlPC instance;
    //GameControl gameControl;
    [SerializeField] private bool useVR;
    [SerializeField] private float timer;
    private float timerCount;

    public static GameMode gameMode = GameMode.Free;

    public static int score;
    [SerializeField] private TMP_Text scoreText, timerText;
    [SerializeField] private Text endScoreText;
    [SerializeField] private GameObject endPanel;

    private bool limitedArrows = false;
    [SerializeField] private int maxArrows = 20;
    private int arrowCount;

    [SerializeField] private MouseLook mouseLook;

    private void Awake()
    {
        if (useVR) VrUtils.SetVREnabled(true);
        if (!instance) instance = this;
        else Destroy(this);
    }

    private void Start()
    {
        mouseLook.SetMouseLookEnabled(false);
        score = 0;
        if (scoreText) scoreText.text = "Score: 0";
        if (gameMode == GameMode.Frenzy) timerCount = timer;
        else if (gameMode == GameMode.Free) timerText.gameObject.transform.parent.gameObject.SetActive(false);
        else
        {
            limitedArrows = true;
            arrowCount = maxArrows;
            SetArrowText("Arrows: " + arrowCount);
        }
    }

    private void Update()
    {
        if (gameMode != GameMode.Frenzy) return;
        if (timerCount > 0)
        {
            timerText.text = "Time: " + (int)timerCount;
            timerCount -= Time.deltaTime;
        }
        else EndGame();
    }

    public void SetArrowText(string _text)
    {
        timerText.text = _text;
    }

    public void EndGame()
    {
        endPanel.gameObject.SetActive(true);
        endScoreText.text = "Score: " + score.ToString();
    }

    public static void AddScore(int _amount)
    {
        score += _amount;
        if (instance.scoreText) instance.scoreText.text = "Score: " + score;
    }


    public void GamePause(bool _isPaused)
    {
        mouseLook.SetMouseLookEnabled(_isPaused);
        mouseLook.SetMouseVisible(_isPaused);
    }
}
