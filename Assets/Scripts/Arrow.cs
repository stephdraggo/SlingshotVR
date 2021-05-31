using UnityEngine;
using Wakaba.VR;
using Wakaba.VR.Interaction;

public class Arrow : InteractableObject
{
    public bool InBow { get; set; }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Bow bow))
        {
            if (!transform.GetComponentInParent<VrController>()) return;
            if (!bow.GetComponentInParent<VrController>()) return;
            if (bow.CurrentArrow != null) return;

            VrController controller = transform.parent.GetComponent<VrController>();

            controller.GetComponent<InteractGrab>().ForceRelease();

            transform.SetParent(bow.transform);
            transform.position = bow.arrowPosition.position;
            transform.rotation = bow.arrowPosition.rotation;

            Rigidbody.isKinematic = true;

            bow.CurrentArrow = this;
            InBow = true;
            SetGrabbable(false);

        }
    }

    public void ReleaseFromBow()
    {
        InBow = false;
        transform.SetParent(null);
        Rigidbody.isKinematic = false;
        SetGrabbable(true);
    }
}
