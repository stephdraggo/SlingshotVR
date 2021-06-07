using UnityEngine;
using UnityEngine.UI;

namespace BigBoi.Menus
{
    /// <summary>
    /// Adds the following functionality to a button object:
    /// Enable the first panel of the passed type.
    /// </summary>
    [AddComponentMenu("BigBoi/Menu System/Methods/Enable Panel by Type")]
    [RequireComponent(typeof(Button))]
    public class EnablePanelByType : MonoBehaviour
    {
        [SerializeField, Tooltip("The first panel of this type will be enabled. Other panels not marked as 'Always Visible' will be disabled.")]
        private PanelTypes type;

        void Start()
        {
            GetComponent<Button>().onClick.AddListener(EnablePanel);
        }

        /// <summary>
        /// Enable the first panel of the given type.
        /// </summary>
        private void EnablePanel()
        {
            FindObjectOfType<BaseMenuManager>().EnablePanelByType(type);
        }
    }
}