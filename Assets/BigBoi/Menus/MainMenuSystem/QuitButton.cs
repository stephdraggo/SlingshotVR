using UnityEngine;
using UnityEngine.UI;

namespace BigBoi.Menus
{
    /// <summary>
    /// Adds the following functionality to a button object:
    /// In Unity Editor: closes play mode
    /// In build: closes application
    /// </summary>
    [AddComponentMenu("BigBoi/Menu System/Methods/Quit")]
    [RequireComponent(typeof(Button))]
    public class QuitButton : MonoBehaviour
    {
        void Start()
        {
            GetComponent<Button>().onClick.AddListener(Quit);
        }

        /// <summary>
        /// Close play mode or application (unity editor and build function)
        /// </summary>
        public void Quit()
        {
            Application.Quit();

#if UNITY_EDITOR
            UnityEditor.EditorApplication.ExitPlaymode();
#endif
        }
    }
}