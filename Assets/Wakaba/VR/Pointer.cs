using UnityEngine;
namespace Wakaba.VR
{
    public class Pointer : MonoBehaviour
    {
        private const float TracerWidth = 0.025f;

        public Vector3 EndPoint { get; private set; } = Vector3.zero;
        public bool Active { get;  set; } = false;

        public VrController controller;

        [SerializeField] private float cursorScaleFactor = 0.1f;
        [SerializeField] private Color invalid = Color.red;
        [SerializeField] private Color valid = Color.green;

        private Transform tracer;
        private Transform cursor;

        private Renderer tracerRenderer;
        private Renderer cursorRenderer;

        private void Start()
        {
            controller.Input.OnPointerPressed.AddListener(_args => { Active = true; tracer.gameObject.SetActive(true); cursor.gameObject.SetActive(true); });
            controller.Input.OnPointerReleased.AddListener(_args => { Active = false; tracer.gameObject.SetActive(false); cursor.gameObject.SetActive(false); });

            CreatePointer();
            tracer.gameObject.SetActive(false);
            cursor.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (!Active) return;

            bool didHit = Physics.Raycast(controller.transform.position, controller.transform.forward, out RaycastHit hit);
            EndPoint = didHit ? hit.point : Vector3.zero;
            UpdateScalePos(hit, didHit);
            SetValid(didHit);
        }

        public void SetValid(bool _valid)
        {
            tracerRenderer.material.color = _valid ? valid : invalid;
            cursorRenderer.material.color = _valid ? valid : invalid;
        }

        private void UpdateScalePos(RaycastHit _hit, bool _didHit)
        {
            if (_didHit)
            {
                CalculateDistanceAndDirection(controller.transform.position, _hit.point, out float dis, out Vector3 dir);

                // Set the tracer position to the midpoint of the parent pos and the endpoint.
                Vector3 midPoint = Vector3.Lerp(controller.transform.position, _hit.point, 0.5f);
                tracer.position = midPoint;

                // Scale the tracer to between the endpoint and this.
                tracer.localScale = new Vector3(TracerWidth, TracerWidth, dis);

                // Set the cursor to the endpoint and scale it.
                cursor.position = _hit.point;
                cursor.localScale = Vector3.one * cursorScaleFactor;
            }
            else
            {
                // Set the tracer and cursor position/scale values based on an arbitrary endpoint.
                CalculateDistanceAndDirection(controller.transform.position, controller.transform.forward + controller.transform.forward * 100, out float dis, out Vector3 dir);
                
                Vector3 midPoint = Vector3.Lerp(controller.transform.position, controller.transform.position + dir * dis, 0.5f);
                tracer.position = midPoint;
                
                tracer.localScale = new Vector3(TracerWidth, TracerWidth, dis);

                cursor.position = controller.transform.position + controller.transform.forward * 100;
                cursor.localScale = Vector3.one * cursorScaleFactor;
            }
        }

        private void CalculateDistanceAndDirection(Vector3 _startPoint, Vector3 _endPoint, out float _distance, out Vector3 _direction)
        {
            Vector3 heading = _endPoint - _startPoint;
            _distance = heading.magnitude;
            _direction = heading / _distance;
        }

        private void CreatePointer()
        {
            GameObject tracerObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
            GameObject cursorObj = GameObject.CreatePrimitive(PrimitiveType.Sphere);

            tracerObj.layer = LayerMask.NameToLayer("Ignore Raycast");
            cursorObj.layer = LayerMask.NameToLayer("Ignore Raycast");

            tracerObj.GetComponent<BoxCollider>().enabled = false;
            cursorObj.GetComponent<SphereCollider>().enabled = false;

            tracer = tracerObj.transform;
            cursor = cursorObj.transform;

            tracer.parent = controller.transform;
            cursor.parent = controller.transform;

            tracerRenderer = tracer.GetComponent<Renderer>();
            cursorRenderer = cursor.GetComponent<Renderer>();

            SetValid(false);
        }
    }
}