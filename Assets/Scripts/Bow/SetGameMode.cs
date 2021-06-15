using System.Collections;
using System.Collections.Generic;
using Bow;
using UnityEngine;

public class SetGameMode : MonoBehaviour
{
    public void ChangeGameMode(int _mode)
    {
        if (Wakaba.VR.VrUtils.IsVREnabled() == true) GameControl.gameMode = (GameMode)_mode;
        else GameControlPC.gameMode = (GameMode)_mode;
    }

}
