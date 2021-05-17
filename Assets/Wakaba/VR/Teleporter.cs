using UnityEngine;
namespace Wakaba.VR
{
    [RequireComponent(typeof(Pointer))]
    public class Teleporter : MonoBehaviour
    {
        [SerializeField, HideInInspector] private Pointer pointer;

        private void OnValidate() => pointer = gameObject.GetComponent<Pointer>();

        private void Start()
        {
            if (pointer == null) pointer = gameObject.GetComponent<Pointer>();

            pointer.controller.Input.OnTeleportPressed.AddListener(_args => { if (pointer.EndPoint != Vector3.zero) VrRig.Instance.PlayArea.position = pointer.EndPoint; });
        }
    }
}