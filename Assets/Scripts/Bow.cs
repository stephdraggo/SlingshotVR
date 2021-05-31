using UnityEngine;
using Wakaba.VR;
using Wakaba.VR.Interaction;


public class Bow : InteractableObject
{
    public Transform arrowPosition;

    public Arrow CurrentArrow { get; set; }

    public override void OnObjectReleased(VrController _controller)
    {
        base.OnObjectReleased(_controller);
        if (CurrentArrow)
        {
            CurrentArrow.ReleaseFromBow();
            CurrentArrow = null;
        }
    }




}
