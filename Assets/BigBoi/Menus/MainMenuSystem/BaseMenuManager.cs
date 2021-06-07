using System;
using UnityEngine;

/// <summary>
/// The Menu System for switching between panels and loading scenes.
/// </summary>
namespace BigBoi.Menus
{
    /// <summary>
    /// Base menu manager, not to be used alone.
    /// Game scene and non game scene menu managers derive from this class.
    /// Basic information about each panel in the scene is stored in an array with
    /// elements being accessible by index or 'Panel Type'.
    /// </summary>
    public abstract class BaseMenuManager : MonoBehaviour
    {
        [SerializeField, Tooltip("Fill in the details of every panel in this scene that will be affected by this menu manager.")]
        protected PanelDetails[] panels;

        /// <summary>
        /// Information about one menu or display panel in the scene.
        /// Any panels that will be affected by other objects should be listed and filled out here.
        /// </summary>
        [Serializable]
        public class PanelDetails
        {
            [Tooltip("The panel object which can be enabled or disabled.")]
            public GameObject panelObject;

            [Tooltip("Should this panel start active in the hierarchy? Example: press any key screen.")]
            public bool activeOnStart;

            [Tooltip("Should this panel always be visible? Example: HUD.\nIf this is selected, activeOnStart should also be selected but the script will fix this if missed.")]
            public bool alwaysVisible;

            [Tooltip("Should this panel pause and/or disable gameplay while active?\nHas no effect in non game scenes.")]
            public bool disableGame;

            [Tooltip("Which predefined type is this panel? To add or remove types, change the 'PanelTypes' enum in BaseMenuManager.cs.")]
            public PanelTypes type;
        }

        protected virtual void Start()
        {
            //enable any panels that are set to active on start
            foreach (PanelDetails _panel in panels)
            {
                if (_panel.alwaysVisible && !_panel.activeOnStart) //if a panel is set to always visible but not active on start this will fix that
                {
                    _panel.activeOnStart = true;
                }
                if (_panel.activeOnStart)
                {
                    _panel.panelObject.SetActive(true);
                }
                else
                {
                    _panel.panelObject.SetActive(false);
                }
            }
        }

        /// <summary>
        /// Disable all panels not marked as alwaysVisible and enable a specific panel based on its index position in the array of panels.
        /// If the index is out of bounds, the panels will be left unchanged.
        /// </summary>
        public void EnablePanelByIndex(int _index)
        {
            if (_index < panels.Length) //if index within bounds of array
            {
                DisablePanels();

                panels[_index].panelObject.SetActive(true);

                ExtraEnableFunctionality(panels[_index]);
            }
            else
            {
                Debug.LogError("The passed index was out of bounds of the panels array, leaving panels unchanged.");
            }
        }

        /// <summary>
        /// Disable all panels not marked as alwaysVisible.
        /// </summary>
        public void DisablePanels()
        {
            foreach (PanelDetails _panel in panels)
            {
                if (!_panel.alwaysVisible) //if not set to always visible, disable
                {
                    _panel.panelObject.SetActive(false);
                }
            }

            ExtraDisableFunctionality();
        }

        /// <summary>
        /// Disable all panels not marked as alwaysVisible and enable the first panel of the specified type.
        /// If no panel of the type is found, the panels will be left unchanged.
        /// </summary>
        /// <param name="_type"></param>
        public void EnablePanelByType(PanelTypes _type)
        {
            foreach (PanelDetails _panel in panels)
            {
                if (_panel.type == _type)
                {
                    DisablePanels();

                    _panel.panelObject.SetActive(true);

                    ExtraEnableFunctionality(_panel);

                    return; //if more than one of this type, only enable first one
                }
            }
        }

        protected virtual void ExtraEnableFunctionality(PanelDetails _panel) { }
        protected virtual void ExtraDisableFunctionality() { }

    }

    
    
    public enum PanelTypes
    {
        //press any key panel has separate script and does not get a type here
        HUD,
        Main,
        Options,
        Pause,
        Other,
    }
}