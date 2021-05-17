using UnityEngine;
using UnityEngine.Events;
using Valve.VR;
using Serializable = System.SerializableAttribute;
namespace Wakaba.VR
{
    [Serializable] public class VrInputEvent : UnityEvent<InputEventArgs> { }

    [Serializable]
    public class InputEventArgs : MonoBehaviour
    {
        /// <summary>The controller firing the event.</summary>
        public VrController controller;

        /// <summary>The input source the event is firing from.</summary>
        public SteamVR_Input_Sources source;

        /// <summary>The position the player is touching the touchpad on.</summary>
        public Vector2 touchpadAxis;

        public InputEventArgs(VrController _controller, SteamVR_Input_Sources _source, Vector2 _touchpadAxis)
        {
            controller = _controller;
            source = _source;
            touchpadAxis = _touchpadAxis;
        }
    }
}