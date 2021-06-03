using UnityEngine;
namespace Wakaba.VR.Interaction
{
    [RequireComponent(typeof(VrControllerInput))]
    public class InteractGrab : MonoBehaviour
    {
        public InteractionEvent grabbed = new InteractionEvent();
        public InteractionEvent released = new InteractionEvent();

        private VrControllerInput input;

        private InteractableObject collidingObject;
        private InteractableObject heldObject;

        // The held object's original parent before it got reparented to this controller.
        private Transform heldOriginalParent;
        

        public void ForceRelease()
        {
            ReleaseObject();
        }

        private void Start()
        {
            input = gameObject.GetComponent<VrControllerInput>();

            input.OnGrabPressed.AddListener((_args) => { if (collidingObject != null) GrabObject(); });
            input.OnGrabReleased.AddListener((_args) => { if (heldObject != null) ReleaseObject(); });
        }

        private void SetCollidingObject(Collider _other)
        {
            InteractableObject interactable = _other.GetComponent<InteractableObject>();
            if (collidingObject != null || interactable == null) return;
            collidingObject = interactable;
        }

        private void OnTriggerEnter(Collider _other) => SetCollidingObject(_other);

        private void OnTriggerExit(Collider _other)
        {
            if (collidingObject == _other.GetComponent<InteractableObject>()) collidingObject = null;
        }

        private void GrabObject()
        {
            heldObject = collidingObject;
            
            //For switching hands, release the object in the other hand first
            if(heldObject.Grabbed != null) heldObject.Grabbed.ForceRelease();

            heldObject = collidingObject;

            collidingObject = null;
            if (heldObject.transform.parent != null) heldOriginalParent = heldObject.transform.parent;

            //Stop the rigidbody without becoming kinematic so collisions can still be detected while held
            heldObject.Freeze();
            SnapObject(heldObject.transform, heldObject.AttachPoint);

            heldObject.OnObjectGrabbed(input.Controller);
            grabbed.Invoke(new InteractEventArgs(input.Controller, heldObject.Rigidbody, heldObject.Collider));
        }

        private void ReleaseObject()
        {
            heldObject.UnFreeze();
            if (heldOriginalParent == null) heldObject.transform.SetParent(null);
            else heldObject.transform.SetParent(heldOriginalParent);

            if (heldObject.ThrowOnRelease)
            {
                heldObject.transform.SetPositionAndRotation(transform.position, transform.rotation);
                heldObject.Rigidbody.velocity = input.Controller.Velocity;
                heldObject.Rigidbody.angularVelocity = input.Controller.AngularVelocity;
            }

            heldObject.OnObjectReleased(input.Controller);
            released.Invoke(new InteractEventArgs(input.Controller, heldObject.Rigidbody, heldObject.Collider));
            heldObject = null;
        }

        private void SnapObject(Transform _object, Transform _attachPoint)
        {
            Rigidbody attachPoint = input.Controller.Rigidbody;
            _object.transform.SetParent(transform);

            if (_attachPoint == null)
            {
                _object.localPosition = Vector3.zero;
                _object.localRotation = Quaternion.identity;
            }
            else
            {
                _object.position = attachPoint.transform.position - (_attachPoint.position - _object.position);
                _object.rotation = attachPoint.transform.rotation * Quaternion.Euler(_attachPoint.localEulerAngles);
            }
        }
    }
}