using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Wakaba.VR;
using Wakaba.VR.Interaction;

public class Arrow : InteractableObject
{
    //bow holding the arrow
    public Bow currentBow { get; set; }
    
    //controller holding the arrow while in the bow
    private InteractGrab holdingController = null;

    private void OnCollisionEnter(Collision _collision)
    {
        //check collision is with bow
        if (!_collision.gameObject.TryGetComponent(out Bow bow)) return;
        
        //if the bow or arrow are not being held, return
        if (!transform.GetComponentInParent<VrController>()) return;
        if (!bow.GetComponentInParent<VrController>()) return;

        //if the bow already has an arrow, return
        if (bow.CurrentArrow != null) return;

        //attach arrow to bow
        AttachToBow(bow);
    }

    /// <summary>
    /// Places the arrow inside a bow, removing free control from the hand holding it
    /// </summary>
    private void AttachToBow(Bow _bow)
    {
        //Get parent controller
        holdingController = transform.parent.GetComponent<VrController>().GetComponent<InteractGrab>();

        //Set parent to bow and go to position
        transform.SetParent(_bow.transform);
        transform.position = _bow.arrowPosition.position;
        transform.rotation = _bow.arrowPosition.rotation;

        //Set kinematic so no longer effected by gravity
        Rigidbody.isKinematic = true;

        //When letting go, do not use controller velocity
        ThrowOnRelease = false;

        //Set in bow
        _bow.EquipArrow(this, holdingController);
        currentBow = _bow;

        //Disable grabbing
        SetGrabbable(false);
    }
    

    /// <summary>
    /// Removes the arrow from the bow it is in, firing it if the bow is pulled back
    /// </summary>
    public void ReleaseFromBow()
    {
        if (!currentBow) return;

        //get the force at which the arrow should be released
        Vector3 fireForce = currentBow.GetFireForce();

        //Detach from bow
        currentBow.CurrentArrow = null;
        currentBow = null;

        //unparent from bow
        transform.SetParent(null);

        //set rigidbody back to dynamic
        Rigidbody.isKinematic = false;

        //release the arrow
        Rigidbody.AddForce(fireForce);
        
        //Arrow can be thrown again when not in bow
        ThrowOnRelease = true;

        //Enable grabbing
        SetGrabbable(true);
    }
    
    
    public override void OnObjectGrabbed(VrController _controller)
    {
        base.OnObjectGrabbed(_controller);
        //change layer so the arrow can collide with the bow
        gameObject.layer = 0;
    }

    public override void OnObjectReleased(VrController _controller)
    {
        base.OnObjectReleased(_controller);
        //set layer back to worldArrow to stop collision with bow
        gameObject.layer = 6;
        
        //Release the arrow from the bow it is in
        ReleaseFromBow();
    }

    /// <summary>
    /// Releases the arrow from the controller holding it, call when dropping the bow holding the arrow
    /// </summary>
    public void ForceReleaseFromController()
    {
        if (!holdingController) return;
        
        holdingController.ForceRelease();
    }
    
}
