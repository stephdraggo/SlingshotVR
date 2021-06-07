using UnityEngine;
namespace Wakaba.VR
{
    public class VrRig : MonoSingleton<VrRig>
    {
        public Transform LeftController => leftController;
        [SerializeField] private Transform leftController;
        public Transform RightController => rightController;
        [SerializeField] private Transform rightController;
        public Transform Headset => headset;
        [SerializeField] private Transform headset;
        public Transform PlayArea => playArea;
        [SerializeField] private Transform playArea;

        private VrController left;
        private VrController right;

        private void OnValidate()
        {
            // Check if the set object isn't a VrController, if it isn't, unset it and warn the user.
            if (leftController != null && leftController.GetComponent<VrController>() == null)
            {
                // The object set to this variable is not of type VrController.
                leftController = null;
                Debug.LogWarning("The object you are trying to set to the leftController does not have VrController component on it!");
            }
            if (rightController != null && rightController.GetComponent<VrController>() == null)
            {
                // The object set to this variable is not of type VrController.
                rightController = null;
                Debug.LogWarning("The object you are trying to set to the rightController does not have VrController component on it!");
            }
        }

        private void Awake()
        {
            // Valitate all the transform components, aka Small-Brain-Proofing.
            ValidateComponent(leftController);
            ValidateComponent(rightController);
            ValidateComponent(headset);
            ValidateComponent(playArea);

            // Get the VrController components from the relevant controllers.
            left = leftController.GetComponent<VrController>();
            right = rightController.GetComponent<VrController>();

            // Initialise the two controllers;
            left.Initialise();
            right.Initialise();
        }

        private void ValidateComponent<T>(T _component) where T : Component
        {
            // If the component is null then log out the name of the component in a error.
            if (_component == null)
            {
                Debug.LogError($"Component {nameof(_component)} is null! This has to be set!");
#if UNITY_EDITOR
                // The component was null and we are in the editor so stop the editor from playing.
                UnityEditor.EditorApplication.isPlaying = false;
#endif
            }
        }
    }
}