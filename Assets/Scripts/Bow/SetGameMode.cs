using System.Collections;
using System.Collections.Generic;
using Bow;
using UnityEngine;

public class SetGameMode : MonoBehaviour
{
    public void ChangeGameMode(int _mode) => GameControl.gameMode = (GameMode) _mode;

}
