using UnityEngine;
using Wakaba.VR;
using Wakaba.VR.Interaction;


public class Bow : InteractableObject
{
    public Transform arrowPosition;

    public Arrow CurrentArrow { get; set; }

    //When bow is dropped also drop nocked arrow
    public override void OnObjectReleased(VrController _controller)
    {
        base.OnObjectReleased(_controller);

        if (!CurrentArrow) return;
        //release the arrow from its controller, which will also release it from this bow
        CurrentArrow.ForceReleaseFromController();
        
        CurrentArrow = null;
    }




}
