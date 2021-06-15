using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Wakaba.VR;

namespace SlingShotVR.Menus
{
    /// <summary>
    /// Put these on each "hand" and uncomment the pointer stuff
    /// </summary>
    public class VRUGUIPointer : MonoBehaviour
    {
        [SerializeField] private SteamVR_Action_Boolean clickAction;

        [SerializeField] private LayerMask uiMask;

        [SerializeField] private Pointer pointer;
        private VRInputModule inputModule;

        void Start()
        {
            inputModule = FindObjectOfType<VRInputModule>();
        }

        void Update()
        {
            inputModule.ControllerButtonDown = clickAction.stateDown;
            inputModule.ControllerButtonUp = clickAction.stateUp;

            Vector3 position = Vector3.zero;
            bool hitUi = false;
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, uiMask))
            {
                position = hit.point;
                hitUi = true;
            }

            inputModule.ControllerPosition = position;
            if (pointer != null) pointer.Active = hitUi;
        }
    }
}