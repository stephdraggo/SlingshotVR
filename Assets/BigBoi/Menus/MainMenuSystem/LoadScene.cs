using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace BigBoi.Menus
{
    /// <summary>
    /// Utilises Core and Editor from BigBoi to display scene in inspector.
    /// Adds the following functionality to a button object:
    /// Load a specific scene.
    /// Limitations: will error out if build settings do not contain the specified scene.
    /// </summary>
    [AddComponentMenu("BigBoi/Menu System/Methods/Load Scene")]
    [RequireComponent(typeof(Button))]
    public class LoadScene : MonoBehaviour
    {
        [SerializeField, SceneField, Tooltip("The scene this button will load.")]
        private string scene;

        void Start()
        {
            GetComponent<Button>().onClick.AddListener(ChangeScene);
        }

        /// <summary>
        /// Load the scene given to this class.
        /// </summary>
        private void ChangeScene()
        {
            SceneManager.LoadScene(scene);
        }
    }
}