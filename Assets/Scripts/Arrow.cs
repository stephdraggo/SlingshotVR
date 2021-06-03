using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Wakaba.VR;
using Wakaba.VR.Interaction;

public class Arrow : InteractableObject
{
    [Header("Arrow settings")]
    [Tooltip("If not set to 0, will change the y value of the box collider when grabbed." +
             "This is for preventing the arrow from colliding with the bow until it is closer" +
             "If collider is not a box collider, this will be ignored.")]
    [SerializeField] float newColliderHeight = 0;
    private float baseColliderHeight;
    private BoxCollider boxCollider;

    [SerializeField] private Transform tipPosition;
    [SerializeField] private float tipHitRadius = 0.075f;
    
    //If true, the arrow will stick in things when it hits them
    bool fired;
    
    //bow holding the arrow
    public Bow currentBow { get; set; }
    
    //controller holding the arrow while in the bow
    private InteractGrab holdingController = null;

    protected override void Awake()
    {
        base.Awake();

        if (newColliderHeight == 0) return;
        if (TryGetComponent<BoxCollider>(out boxCollider)) baseColliderHeight = ((BoxCollider) Collider).size.y;

    }

    private void OnCollisionEnter(Collision _collision)
    {
        //collision with bow
        if (_collision.gameObject.TryGetComponent(out Bow bow))
        {
            //if the bow or arrow are not being held, return
            if (!transform.GetComponentInParent<VrController>()) return;
            if (!bow.GetComponentInParent<VrController>()) return;

            //if the bow already has an arrow, return
            if (bow.CurrentArrow != null) return;

            //attach arrow to bow
            AttachToBow(bow);
        }
        else
        {
            //collision with object after firing
            if (fired)
            {
                //Default (trees, ground etc.)
                LayerMask mask = LayerMask.GetMask("Default");
                //If collision with default near tip, set rigidbody to kinematic so the arrow sticks
                if (Physics.OverlapSphere(tipPosition.position, tipHitRadius, mask).Length > 0) Rigidbody.isKinematic = true;
                //Tip didn't hit, don't stick
                else fired = false;
            }
        }
        
        
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
        holdingController = null;
        currentBow = null;

        //unparent from bow
        transform.SetParent(null);

        //set rigidbody back to dynamic
        Rigidbody.isKinematic = false;

        //release the arrow
        Rigidbody.AddForce(fireForce);
        
        //Set fired so that the arrow sticks in objects it hits
        if (fireForce.magnitude > 0) fired = true;
        
        //Arrow can be thrown again when not in bow
        ThrowOnRelease = true;

        //Enable grabbing
        SetGrabbable(true);
    }
    
    
    public override void OnObjectGrabbed(VrController _controller)
    {
        //reduce the collider size so that the arrow needs to be closer to the bow to collide
        if (boxCollider) boxCollider.size = new Vector3(boxCollider.size.x, newColliderHeight, boxCollider.size.z);

        //When pulling arrow out of something, unstick it
        fired = false;
        Rigidbody.isKinematic = false;
        
        base.OnObjectGrabbed(_controller);
        //change layer so the arrow can collide with the bow
        gameObject.layer = 0;
    }

    public override void OnObjectReleased(VrController _controller)
    {
        //set the collider size back to the base size
        if (boxCollider) boxCollider.size = new Vector3(boxCollider.size.x, baseColliderHeight, boxCollider.size.z);
        
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
