using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

namespace BreadAndButter.VR
{
    [RequireComponent(typeof(SteamVR_Behaviour_Pose))]
    [RequireComponent(typeof(VrControllerInput))]
    [RequireComponent(typeof(Rigidbody))]
    public class VrController : MonoBehaviour
    {
        public VrControllerInput Input => input;
        public Rigidbody Rigidbody => rigidbody;
        public Vector3 Velocity => pose.GetVelocity();
        public Vector3 AngularVelocity => pose.GetAngularVelocity();
        public SteamVR_Input_Sources InputSource => pose.inputSource;

        private new Rigidbody rigidbody;

        private SteamVR_Behaviour_Pose pose;
        private VrControllerInput input;

        public void Initialise()
        {
            pose = GetComponent<SteamVR_Behaviour_Pose>();
            input = GetComponent<VrControllerInput>();
            rigidbody = GetComponent<Rigidbody>();

            input.Initialise(this);
        }
    }
}
