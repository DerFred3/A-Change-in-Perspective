using UnityEngine;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour {
    [SerializeField] private InputActionReference _actionInteract;
    [SerializeField] private Transform _interactionAnchor;

    private GameObject _heldObject;

    private void OnEnable() {
        _actionInteract.action.Enable();
    }

    private void OnDisable() {
        _actionInteract.action.Disable();
    }

    private void Update() {
        if (_actionInteract.action.WasPressedThisFrame()) HandleInteraction();
    }

    private void HandleInteraction() {
        if (_heldObject != null) {
            ReleaseHeldObject();
            return;
        }

        Transform camTransform = ReferenceManager.Instance.CameraTransform;
        Ray ray = new Ray(camTransform.position, camTransform.forward);
        RaycastHit hit;
        if (!Physics.Raycast(ray, out hit, float.MaxValue, LayerMask.GetMask("Interactable"))) return;

        _heldObject = hit.collider.gameObject;
        float distanceToObject = (_heldObject.transform.position - camTransform.position).magnitude;
        float distanceToAnchor = (_interactionAnchor.position - camTransform.position).magnitude;
        Vector3 newScale = _heldObject.transform.localScale;
        newScale *= (distanceToAnchor / distanceToObject);
        _heldObject.transform.localScale = newScale;
        _heldObject.GetComponent<Grabbable>().SetAnchor(_interactionAnchor);
    }

    private void ReleaseHeldObject() {
        _heldObject.GetComponent<Grabbable>().SetAnchor(null);

        Transform camTransform = ReferenceManager.Instance.CameraTransform;
        Ray ray = new Ray(camTransform.position, camTransform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, float.MaxValue, LayerMask.GetMask(new string[] { "Ground", "Wall"}), QueryTriggerInteraction.Ignore)) {

            Vector3 theoreticalScale = _heldObject.transform.localScale;
            theoreticalScale *= (hit.point - camTransform.position).magnitude / (_interactionAnchor.position - camTransform.position).magnitude;

            Vector3 newPosition = hit.point + theoreticalScale.z * (camTransform.position - hit.point).normalized;
            _heldObject.transform.position = newPosition;

            Vector3 newScale = _heldObject.transform.localScale;
            newScale *= ((newPosition - _interactionAnchor.position).magnitude / (_interactionAnchor.position - camTransform.position).magnitude);
            newScale.x = Mathf.Max(0.1f, newScale.x);
            newScale.y = Mathf.Max(0.1f, newScale.y);
            newScale.z = Mathf.Max(0.1f, newScale.z);
            _heldObject.transform.localScale = newScale;
        }


        _heldObject = null;
    }
}
