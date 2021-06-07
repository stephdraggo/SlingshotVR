using UnityEngine;
using Valve.VR;
namespace Wakaba.VR
{
    [RequireComponent(typeof(SteamVR_Behaviour_Pose), typeof(VrControllerInput), typeof(Rigidbody))]
    public class VrController : MonoBehaviour
    {
        public Rigidbody Rigidbody { get; private set; }
        public VrControllerInput Input { get; private set; }
        /// <summary>How fast the controller is moving in worldspace.</summary>
        public Vector3 Velocity => pose.GetVelocity();
        /// <summary>How fast the controller is rotating, and in which direction.</summary>
        public Vector3 AngularVelocity => pose.GetAngularVelocity();

        public SteamVR_Input_Sources InputSource => pose.inputSource;
        
        private SteamVR_Behaviour_Pose pose;

        public void Initialise()
        {
            pose = gameObject.GetComponent<SteamVR_Behaviour_Pose>();
            Input = gameObject.GetComponent<VrControllerInput>();
            Rigidbody = gameObject.GetComponent<Rigidbody>();

            Input.Initialise(this);
        }
    }
}