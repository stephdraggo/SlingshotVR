using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace BreadAndButter.VR
{
    public static class VrUtils
    {
        private static List<XRInputSubsystem> subsystems = new List<XRInputSubsystem>();

        public static void SetVREnabled(bool _enabled)
        {
            //Get all connected XR devices
            SubsystemManager.GetInstances<XRInputSubsystem>(subsystems);

            //enable/disable each subsystem
            foreach(XRInputSubsystem subsystem in subsystems)
            {
                if (_enabled) subsystem.Start();
                else subsystem.Stop();
            }
        }

        public static bool IsVREnabled() 
        {
            //Get all connected XR devices
            SubsystemManager.GetInstances<XRInputSubsystem>(subsystems);

            //check if subsystems are active
            foreach (XRInputSubsystem subsystem in subsystems)
            {
                if (subsystem.running) return true;
            }

            return false;
        }
    }
}
