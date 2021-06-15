using UnityEngine;
using UnityEngine.Events;
using Valve.VR;
using Wakaba.VR;

public class Pause : MonoBehaviour
{
    private SteamVR_Action_Boolean pauseAction;

    [SerializeField] private UnityEvent pauseEvent;
    [SerializeField] private UnityEvent resumeEvent;
    [SerializeField] private UnityEvent endEvent;
    private bool paused;

    [SerializeField] private Pointer leftPointer;
    [SerializeField] private Pointer rightPointer;
    
    private void Start()
    {
        pauseAction = SteamVR_Actions.default_Menu;
        leftPointer.Hide = true;
        rightPointer.Hide = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (end) return;
        if (pauseAction.stateDown)
        {
            if (paused) ResumeGame();
            else PauseGame(false); 
        }
    }

    private bool end;
    public void PauseGame(bool _end)
    {
        
        leftPointer.Hide = false;
        rightPointer.Hide = false;
        paused = true;

        if (_end) endEvent.Invoke();
        else pauseEvent.Invoke();

        _end = end;
    }

    public void ResumeGame()
    {
        resumeEvent.Invoke();
        leftPointer.Hide = true;
        rightPointer.Hide = true;
        paused = false;
    }
}
