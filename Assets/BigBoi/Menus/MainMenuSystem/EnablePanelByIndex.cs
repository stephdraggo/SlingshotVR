using UnityEngine;
using UnityEngine.UI;

namespace BigBoi.Menus
{
    /// <summary>
    /// Adds the following functionality to a button object:
    /// Enable a specific panel by index.
    /// </summary>
    [AddComponentMenu("BigBoi/Menu System/Methods/Enable Panel by Index")]
    [RequireComponent(typeof(Button))]
    public class EnablePanelByIndex : MonoBehaviour
    {
        [SerializeField, Tooltip("The panel with this index in the menu manager will be enabled. Other panels not marked as 'Always Visible' will be disabled.")]
        private int index;

        void Start()
        {
            GetComponent<Button>().onClick.AddListener(EnablePanel);
        }

        /// <summary>
        /// Enable the panel by index given to this class.
        /// </summary>
        private void EnablePanel()
        {
            FindObjectOfType<BaseMenuManager>().EnablePanelByIndex(index);
        }
    }
}
