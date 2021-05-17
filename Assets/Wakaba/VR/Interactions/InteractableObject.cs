using UnityEngine;
using SteamVrInputSource = Valve.VR.SteamVR_Input_Sources;
namespace Wakaba.VR.Interaction
{
    [RequireComponent(typeof(Rigidbody))]
    public class InteractableObject : MonoBehaviour
    {
        public Rigidbody Rigidbody { get; private set; }
        public Collider Collider { get; private set; }
        public Transform AttachPoint => attachPoint;

        [SerializeField] private bool isGrabbable = true;
        [SerializeField] private bool isTouchable = false;
        [SerializeField] private bool isUsable = false;
        [SerializeField] private SteamVrInputSource allowedSource = SteamVrInputSource.Any;

        [Space]

        [SerializeField, Tooltip("The point on the interactable object we actually want to grab, if not set, will use the origin.")]
        private Transform attachPoint;

        [Space]

        public InteractionEvent onGrabbed = new InteractionEvent();
        public InteractionEvent onReleased = new InteractionEvent();
        public InteractionEvent onTouched = new InteractionEvent();
        public InteractionEvent onCovid = new InteractionEvent();
        public InteractionEvent onUsed = new InteractionEvent();
        public InteractionEvent onUnused = new InteractionEvent();

        private void Start()
        {
            Rigidbody = gameObject.GetComponent<Rigidbody>();

            Collider = gameObject.GetComponent<Collider>();
            if (Collider == null)
            {
                Collider = gameObject.AddComponent<BoxCollider>();
                Debug.LogError($"Object {name} does not have a collider, adding BoxCollider.", gameObject);
            }

            Debug.LogWarning("Please rename variable in InteractableObject.cs, you know which one.");
        }

        private InteractEventArgs GenerateArgs(VrController _controller) => new InteractEventArgs(_controller, Rigidbody, Collider);

        #region OnObject Actions
        public void OnObjectGrabbed(VrController _controller)
        {
            if (isGrabbable && (_controller.InputSource == allowedSource || allowedSource == SteamVrInputSource.Any)) onGrabbed.Invoke(GenerateArgs(_controller));
        }
        public void OnObjectReleased(VrController _controller)
        {
            if (isGrabbable && (_controller.InputSource == allowedSource || allowedSource == SteamVrInputSource.Any)) onReleased.Invoke(GenerateArgs(_controller));
        }
        public void OnObjectTouched(VrController _controller)
        {
            if (isTouchable && (_controller.InputSource == allowedSource || allowedSource == SteamVrInputSource.Any)) onTouched.Invoke(GenerateArgs(_controller));
        }
        public void OnObjectCovid(VrController _controller)
        {
            if (isTouchable && (_controller.InputSource == allowedSource || allowedSource == SteamVrInputSource.Any)) onCovid.Invoke(GenerateArgs(_controller));
        }
        public void OnObjectUsed(VrController _controller)
        {
            if (isUsable && (_controller.InputSource == allowedSource || allowedSource == SteamVrInputSource.Any)) onUsed.Invoke(GenerateArgs(_controller));
        }
        public void OnObjectUnused(VrController _controller)
        {
            if (isUsable && (_controller.InputSource == allowedSource || allowedSource == SteamVrInputSource.Any)) onUnused.Invoke(GenerateArgs(_controller));
        }
        #endregion
    }
}