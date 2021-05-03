using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BreadAndButter.VR
{
    public class VrRig : MonoBehaviour
    {
        public static VrRig instance = null;
        public Transform LeftController => leftController;
        public Transform RightController => rightController;
        public Transform Headset => headset;
        public Transform PlayArea => playArea;

        [SerializeField] private Transform leftController;
        [SerializeField] private Transform rightController;
        [SerializeField] private Transform headset;
        [SerializeField] private Transform playArea;

        private VrController left;
        private VrController right;

        private void OnValidate()
        {
            //Check if the set object is a VrController, if it isn't unset it and warn the user
            if (leftController != null && leftController.GetComponent<VrController>() == null)
            {
                leftController = null;
                Debug.LogWarning("The object you are trying to set to leftController does not have VrController component on it!");
            }
            if (rightController != null && rightController.GetComponent<VrController>() == null)
            {
                rightController = null;
                Debug.LogWarning("The object you are trying to set to rightController does not have VrController component on it!");
            }
        }


        private void Start()
        {
            //Validate all the transform components
            ValidateComponent(leftController);
            ValidateComponent(rightController);
            ValidateComponent(headset);
            ValidateComponent(playArea);

            //get the VrController components from the relevant controller
            left = leftController.GetComponent<VrController>();
            right = rightController.GetComponent<VrController>();

            //initialise the vr controllers
            left.Initialise();
            right.Initialise();
        }

        private void Awake()
        {
            if (instance == null) instance = this;
            else if (instance != this)
            {
                Destroy(gameObject);
                return;
            }
        }

        private void ValidateComponent<T>(T _component) where T : Component
        {
            if (_component == null)
            {
                Debug.LogError($"Component {nameof(_component)} is not set!");

#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
            }
        }
    }
}

