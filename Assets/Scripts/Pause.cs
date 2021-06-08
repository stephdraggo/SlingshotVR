using UnityEngine;
using UnityEngine.Events;
using Valve.VR;
using Wakaba.VR;

public class Pause : MonoBehaviour
{
    private SteamVR_Action_Boolean pauseAction;

    [SerializeField] private UnityEvent pauseEvent;
    [SerializeField] private UnityEvent resumeEvent;
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
        if (pauseAction.stateDown)
        {
            if (paused) ResumeGame();
            else PauseGame(); 
        }
    }

    public void PauseGame()
    {
        pauseEvent.Invoke();
        leftPointer.Hide = false;
        rightPointer.Hide = false;
        paused = true;
    }

    public void ResumeGame()
    {
        resumeEvent.Invoke();
        leftPointer.Hide = true;
        rightPointer.Hide = true;
        paused = false;
    }
}
