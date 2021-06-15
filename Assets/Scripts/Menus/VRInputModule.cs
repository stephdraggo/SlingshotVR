using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SlingShotVR.Menus
{
    /// <summary>
    /// This class allows canvas elements to be interacted with in VR
    /// </summary>
    public class VRInputModule : BaseInputModule
    {
        public Vector3 ControllerPosition { get; set; }
        public bool ControllerButtonDown { get; set; }
        public bool ControllerButtonUp { get; set; }

        private GameObject currentObject = null;
        private PointerEventData data = null;
        [SerializeField] private new Camera camera;

        protected override void Awake()
        {
            base.Awake();

            data = new PointerEventData(eventSystem);
        }

        protected override void Start()
        {
            base.Start();

            //camera = VrRig.instance.Headset.GetComponent<Camera>();
            if (!camera) camera = FindObjectOfType<Camera>();
            //camera = Camera.main;
        }
        

        //update loop for input modules
        public override void Process()
        {
            data.Reset();
            data.position = camera.WorldToScreenPoint(ControllerPosition);

            eventSystem.RaycastAll(data, m_RaycastResultCache);
            data.pointerCurrentRaycast = FindFirstRaycast(m_RaycastResultCache);
            currentObject = data.pointerCurrentRaycast.gameObject;

            m_RaycastResultCache.Clear();

            //hover
            HandlePointerExitAndEnter(data, currentObject);
            
            
            
            if (ControllerButtonDown) ProcessPress();
            if (ControllerButtonUp) ProcessRelease();
            
        }

        private void ProcessPress()
        {
            data.pointerPressRaycast = data.pointerCurrentRaycast;

            GameObject newPointerPress =
                ExecuteEvents.ExecuteHierarchy(currentObject, data, ExecuteEvents.pointerDownHandler);

            if (newPointerPress == null)
                newPointerPress = ExecuteEvents.GetEventHandler<IPointerClickHandler>(currentObject);

            data.pressPosition = data.position;
            data.pointerPress = newPointerPress;
            data.rawPointerPress = currentObject;

            ControllerButtonDown = false;
        }

        private void ProcessRelease()
        {
            ExecuteEvents.Execute(data.pointerPress, data, ExecuteEvents.pointerUpHandler);

            GameObject pointerUpHandler = ExecuteEvents.GetEventHandler<IPointerClickHandler>(currentObject);

            if (data.pointerPress == pointerUpHandler)
                ExecuteEvents.Execute(data.pointerPress, data, ExecuteEvents.pointerClickHandler);

            eventSystem.SetSelectedGameObject(null);
            data.pressPosition = Vector2.zero;
            data.pointerPress = null;
            data.rawPointerPress = null;

            ControllerButtonUp = false;
        }
    }
}