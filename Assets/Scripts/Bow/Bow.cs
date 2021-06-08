using UnityEngine;
using UnityEngine.Serialization;
using Wakaba.VR;
using Wakaba.VR.Interaction;

namespace Bow
{
    public class Bow : InteractableObject
    {
        #region variables
        [Header("Bow settings")]
        [Tooltip("Default location for arrow to go to when put in the bow")]
        public Transform arrowPosition;
        
        //Set to the location of the controller when the arrow is knocked, pulling back is relative to this transform
        private Transform arrowNockedPosition;

        [Tooltip("How far back the arrow can be pulled")]
        [SerializeField] private float arrowMaxPullDistance = 0.6f;

        [Tooltip("How much force to release arrows with, multiplied by pull distance")]
        [SerializeField] private float fireForce = 20f;

        //multiplied by fire force when releasing an arrow
        private const float FireForceMultiplier = 100f;
        
        /// <summary>
        /// The current arrow equipped in this bow, null means no arrow equipped
        /// </summary>
        public Arrow CurrentArrow { get; set; }
        
        //Controller holding the arrow equipped to this bow
        private InteractGrab arrowController;
        
        [Header("Bowstring line renderer positions")]
        [SerializeField] private Transform bowStringTopPos;
        [SerializeField] private Transform bowStringMiddlePos;
        [SerializeField] private Transform bowStringBottomPos;
        private Transform bowStringBaseMiddlePos;

        private float stringPullDistance;
        private LineRenderer stringRenderer;
        private bool useString;

        [Header("Return to player")] 
        [SerializeField] private Transform returnPoint;

        [SerializeField] private float returnDistance = 3f;
        
        #endregion

        #region equip/fire arrow

        /// <summary>
        /// Equips an arrow to this bow
        /// </summary>
        /// <param name="_arrow">Arrow to put in the bow</param>
        /// <param name="_heldController">Controller holding the arrow, will be used for pulling back string</param>
        public void EquipArrow(Arrow _arrow, InteractGrab _heldController)
        {
            //Set arrow and controller
            CurrentArrow = _arrow;
            arrowController = _heldController;
            
            //create a transform for the arrow nocked position, parent it so it moves with the bow
            if (!arrowNockedPosition)
            {
                arrowNockedPosition = new GameObject().transform;
                arrowNockedPosition.SetParent(transform);
            }
            
            //Set the position of the nocked position to where the controller is
            arrowNockedPosition.SetPositionAndRotation(_heldController.transform.position, transform.rotation);
        }

        /// <summary>
        /// Returns the force an arrow should be fired with when released from this bow, based on how far back it is pulled
        /// </summary>
        public Vector3 GetFireForce()
        {
            if (!CurrentArrow) return Vector3.zero;
            
            //Get how far back the arrow is pulled
            float pullDistance = CalculateHandToNockPosDistance();
            pullDistance = Mathf.Clamp(pullDistance, 0f, arrowMaxPullDistance);

            //multiply by the firing force of the bow
            float outfireForce = pullDistance * fireForce * FireForceMultiplier;

            //multiply by the direction the bow is facing
            Vector3 fireForceVector = new Vector3(transform.forward.x * outfireForce, transform.forward.y * outfireForce,
                transform.forward.z * outfireForce);
            
            return fireForceVector;
        }
        
        //When bow is dropped the arrow should also be dropped
        public override void OnObjectReleased(VrController _controller)
        {
            base.OnObjectReleased(_controller);

            if (!CurrentArrow) return;
            
            //release the arrow from its controller, which will also release it from this bow
            CurrentArrow.ForceReleaseFromController();
            CurrentArrow = null;
        }
        
        #endregion

        #region update
        private void Update()
        {
            if (CurrentArrow)
            {
                //Get the local position of the arrow
                Vector3 arrowPos = CurrentArrow.transform.localPosition;

                //default position of the arrow
                float defaultZPos = arrowPosition.localPosition.z;

                //get the distance from the controller to the nock position
                float pullDistance = CalculateHandToNockPosDistance();

                //Clamp the position so the arrow can't go forwards or too far backwards
                pullDistance = Mathf.Clamp(pullDistance, 0, arrowMaxPullDistance);

                //set the string pull distance for the line renderer
                stringPullDistance = pullDistance;

                //offset the arrow position from the default position by the pull distance
                arrowPos.z = defaultZPos - pullDistance;

                //Set the local position of the arrow
                CurrentArrow.transform.localPosition = arrowPos;
            }
            //no arrow equipped, string should straight
            else stringPullDistance = 0;
            
            //Display the line renderer for the string
            UpdateStringRenderer();
            
            //If the return point is set, go to it if far enough away
            if (!returnPoint) return;
            if (!Grabbed && Vector3.Distance(transform.position, returnPoint.position) > returnDistance)
            {
                transform.position = returnPoint.position;
                Rigidbody.velocity = Vector3.zero;
            }
               
        }
        #endregion
        
        #region Get pull distance

        /// <summary>
        /// Returns the how far back the controller is from where the arrow was nocked
        /// </summary>
        private float CalculateHandToNockPosDistance()
        {
            //Get distance between nock position and hand
            Vector3 handBowDistance = arrowNockedPosition.InverseTransformDirection(arrowNockedPosition.position -
                                                                                arrowController.transform.position);
            //return z distance only (how far back hand is)
            return handBowDistance.z;
        }

        #endregion
        
        #region bowstring
        
        protected override void Awake()
        {
            base.Awake();
            SetupBowString();
        }

        void SetupBowString()
        {
            //Check that string can be used
            if (!GetComponent<LineRenderer>())
            {
                Debug.Log("No Line renderer attached, bowstring will not show");
                return;
            }
            if (!(bowStringTopPos && bowStringMiddlePos && bowStringBottomPos))
            {
                Debug.Log("Bow string positions not set, bowstring will not show.");
                return;
            }
            
            //Setup the line renderer for the string
            stringRenderer = gameObject.GetComponent<LineRenderer>();
            stringRenderer.positionCount = 3;
            useString = true;

            //create a default middle position to use when pulling string back
            bowStringBaseMiddlePos = new GameObject().transform;
            bowStringBaseMiddlePos.SetParent(transform);
            bowStringBaseMiddlePos.SetPositionAndRotation(bowStringMiddlePos.position, bowStringMiddlePos.rotation);
            
            UpdateStringRenderer();
        }
        /// <summary>
        /// Sets the positions of the line renderer for the string to their correct values
        /// </summary>
        void UpdateStringRenderer()
        {
            if (!useString) return;
            
            //Change the middle position based on the pull distance
            Vector3 bowStringMiddlePosition = bowStringBaseMiddlePos.localPosition;
            bowStringMiddlePosition.z -= stringPullDistance;
            bowStringMiddlePos.localPosition = bowStringMiddlePosition;
            
            //Set the line renderer points
            stringRenderer.SetPosition(0, bowStringTopPos.position);
            stringRenderer.SetPosition(1, bowStringMiddlePos.position);
            stringRenderer.SetPosition(2, bowStringBottomPos.position);
        }
        
        #endregion
    }
}
