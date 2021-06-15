using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;

public class VRLoadScene : MonoBehaviour
{
 
    public static void LoadScene(string _sceneName)
    {
        SteamVR_LoadLevel.Begin(_sceneName);
    }
}
