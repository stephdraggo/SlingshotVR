using UnityEngine;

namespace BigBoi.Menus
{
    /// <summary>
    /// Simple class that deactivates its object if any key is pressed.
    /// </summary>
    [AddComponentMenu("BigBoi/Menu System/Methods/Press Any Key")]
    public class PressAnyKey : MonoBehaviour
    {
        void Update()
        {
            if (Input.anyKeyDown)
            {
                gameObject.SetActive(false);
            }
        }
    }
}