using UnityEngine;
using Wakaba.VR;
using Wakaba.VR.Interaction;

public class Bow : InteractableObject
{
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
    
    //When bow is dropped the arrow should also be dropped
    public override void OnObjectReleased(VrController _controller)
    {
        base.OnObjectReleased(_controller);

        if (!CurrentArrow) return;
        
        //release the arrow from its controller, which will also release it from this bow
        CurrentArrow.ForceReleaseFromController();
        CurrentArrow = null;
    }
    
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

    private void Update()
    {
        if (!CurrentArrow) return;
        
        //Get the local position of the arrow
        Vector3 arrowPos = CurrentArrow.transform.localPosition;
        
        //default position of the arrow
        float defaultZPos = arrowPosition.localPosition.z;
        
        //move the arrow off the default position based on distance from hand to the nock position
        arrowPos.z = defaultZPos - CalculateHandToNockPosDistance();

        //Clamp the arrow so it can't go forwards or too far backwards
        arrowPos.z = Mathf.Clamp(arrowPos.z, defaultZPos - arrowMaxPullDistance, defaultZPos);
        
        //Set the local position of the arrow
        CurrentArrow.transform.localPosition = arrowPos;
    }

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
}
