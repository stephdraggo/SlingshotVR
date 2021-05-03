using Serializable = System.SerializableAttribute;
using UnityEngine;
using UnityEngine.Events;

namespace BreadAndButter.VR.Interactions
{
    [Serializable]
    public class InteractionEvent : UnityEvent<Interactions.InteractEventArgs> { };

    [Serializable]
    public class InteractEventArgs
    {
        /// <summary> The controller that initiated the interaction event </summary>
        public VrController controller;
        /// <summary> The rigidbody of the interactable object we are interacting with </summary>
        public Rigidbody rigidbody;
        /// <summary> The collider of the interactable object we are interacting with </summary>
        public Collider collider;

        public InteractEventArgs(VrController _controller, Rigidbody _rigidbody, Collider _collider)
        {
            controller = _controller;
            rigidbody = _rigidbody;
            collider = _collider;
        }
    }
}
