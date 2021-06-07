using UnityEngine;
using Valve.VR;
namespace Wakaba.VR
{
    public class VrControllerInput : MonoBehaviour
    {
        public VrController Controller { get; private set; }

        #region SteamVR Actions
        [Header("Steam Actions")]
        [SerializeField] private SteamVR_Action_Boolean pointer;
        [SerializeField] private SteamVR_Action_Boolean teleport;
        [SerializeField] private SteamVR_Action_Boolean interact;
        [SerializeField] private SteamVR_Action_Boolean grab;
        [SerializeField] private SteamVR_Action_Vector2 touchpadAxis;
        #endregion
        
        #region VrInputEvent Variables
        [Header("Unity Events")]
        [SerializeField] private VrInputEvent onPointerPressed = new VrInputEvent();
        [SerializeField] private VrInputEvent onPointerReleased = new VrInputEvent();
        [SerializeField] private VrInputEvent onTeleportPressed = new VrInputEvent();
        [SerializeField] private VrInputEvent onTeleportReleased = new VrInputEvent();
        [SerializeField] private VrInputEvent onInteractPressed = new VrInputEvent();
        [SerializeField] private VrInputEvent onInteractReleased = new VrInputEvent();
        [SerializeField] private VrInputEvent onGrabPressed = new VrInputEvent();
        [SerializeField] private VrInputEvent onGrabReleased = new VrInputEvent();
        [SerializeField] private VrInputEvent onTouchpadAxisChanged = new VrInputEvent();
        #endregion
        
        #region VrInputEvent Properties
        public VrInputEvent OnPointerPressed => onPointerPressed;
        public VrInputEvent OnPointerReleased => onPointerReleased;
        public VrInputEvent OnTeleportPressed => onTeleportPressed;
        public VrInputEvent OnTeleportReleased => onTeleportReleased;
        public VrInputEvent OnInteractPressed => onInteractPressed;
        public VrInputEvent OnInteractReleased => onInteractReleased;
        public VrInputEvent OnGrabPressed => onGrabPressed;
        public VrInputEvent OnGrabReleased => onGrabReleased;
        public VrInputEvent OnTouchpadAxisChanged => onTouchpadAxisChanged;
        #endregion
        
        #region SteamVR Input Callbacks
        private void OnPointerDown(SteamVR_Action_Boolean _action, SteamVR_Input_Sources _source) => onPointerPressed.Invoke(GenerateArgs());
        private void OnPointerUp(SteamVR_Action_Boolean _action, SteamVR_Input_Sources _source) => onPointerReleased.Invoke(GenerateArgs());
        private void OnTeleportDown(SteamVR_Action_Boolean _action, SteamVR_Input_Sources _source) => onTeleportPressed.Invoke(GenerateArgs());
        private void OnTeleportUp(SteamVR_Action_Boolean _action, SteamVR_Input_Sources _source) => onTeleportReleased.Invoke(GenerateArgs());
        private void OnInteractDown(SteamVR_Action_Boolean _action, SteamVR_Input_Sources _source) => onInteractPressed.Invoke(GenerateArgs());
        private void OnInteractUp(SteamVR_Action_Boolean _action, SteamVR_Input_Sources _source) => onInteractReleased.Invoke(GenerateArgs());
        private void OnGrabDown(SteamVR_Action_Boolean _action, SteamVR_Input_Sources _source) => onGrabPressed.Invoke(GenerateArgs());
        private void OnGrabUp(SteamVR_Action_Boolean _action, SteamVR_Input_Sources _source) => onGrabReleased.Invoke(GenerateArgs());
        private void OnTouchpadChanged(SteamVR_Action_Vector2 _action, SteamVR_Input_Sources _source, Vector2 _axis, Vector2 _delta) => onTouchpadAxisChanged.Invoke(GenerateArgs());
        #endregion
        
        public void Initialise(VrController _controller)// => controller = _controller;
        {
            Controller = _controller;

            pointer.AddOnStateDownListener(OnPointerDown, Controller.InputSource);
            pointer.AddOnStateUpListener(OnPointerUp, Controller.InputSource);
            teleport.AddOnStateDownListener(OnTeleportDown, Controller.InputSource);
            teleport.AddOnStateUpListener(OnTeleportUp, Controller.InputSource);
            interact.AddOnStateDownListener(OnInteractDown, Controller.InputSource);
            interact.AddOnStateUpListener(OnInteractUp, Controller.InputSource);
            grab.AddOnStateDownListener(OnGrabDown, Controller.InputSource);
            grab.AddOnStateUpListener(OnGrabUp, Controller.InputSource);
            touchpadAxis.AddOnChangeListener(OnTouchpadChanged, Controller.InputSource);
        }

        /// <summary>Sets up an instance of InputEventArgs based on the controller and touchpad values.</summary>
        private InputEventArgs GenerateArgs() => new InputEventArgs(Controller, Controller.InputSource, touchpadAxis.axis);
    }
}