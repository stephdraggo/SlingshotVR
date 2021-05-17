using System.Collections.Generic;
using UnityEngine;
using XRInputSubsystem = UnityEngine.XR.XRInputSubsystem;
namespace Wakaba.VR
{
    public static class VrUtils
    {
        private static List<XRInputSubsystem> subsystems = new List<XRInputSubsystem>();

        public static void SetVREnabled(bool _enabled)
        {
            // Get all the connected XR devices.
            SubsystemManager.GetInstances(subsystems);

            // Loop through all XR devices.
            foreach (XRInputSubsystem subsystem in subsystems)
            {
                // If we want to enable it, start it, otherwise stop it.
                if (_enabled) subsystem.Start();
                else subsystem.Stop();
            }
        }

        public static bool IsVREnabled()
        {
            // Get all the connected XR devices.
            SubsystemManager.GetInstances(subsystems);

            // Loop through all XR devices.
            foreach (XRInputSubsystem subsystem in subsystems) if (subsystem.running) return true;

            // No active XR devices/
            return false;
        }
    }
}