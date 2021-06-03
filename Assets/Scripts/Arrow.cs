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

    private void AttachToBow(Bow _bow)
    {
        //Get parent controller
        holdingController = transform.parent.GetComponent<VrController>().GetComponent<InteractGrab>();
        //controller.GetComponent<InteractGrab>().ForceRelease();

        //Set parent to bow and go to position
        transform.SetParent(_bow.transform);
        transform.position = _bow.arrowPosition.position;
        transform.rotation = _bow.arrowPosition.rotation;

        //Set kinematic so no longer effected by gravity
        Rigidbody.isKinematic = true;

        //Set in bow
        _bow.CurrentArrow = this;
        currentBow = _bow;

        //Disable grabbing
        SetGrabbable(false);
    }

    public void ReleaseFromBow()
    {
        if (!currentBow) return;

        //Detach from bow
        currentBow.CurrentArrow = null;
        currentBow = null;

        //unparent from bow
        transform.SetParent(null);

        //set rigidbody back to dynamic
        Rigidbody.isKinematic = false;

        //Enable grabbing
        SetGrabbable(true);
    }

    /// <summary>
    /// Releases the arrow from the controller holding it, call when dropping the bow with an arrow in it
    /// </summary>
    public void ForceReleaseFromController()
    {
        if (!holdingController) return;
        
        holdingController.ForceRelease();
    }
    
}
