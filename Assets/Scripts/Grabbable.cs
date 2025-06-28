using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class Grabbable : MonoBehaviour {
    private Transform _activeAnchor;
    private Collider _collider;
    private Rigidbody _rb;

    private void Awake() {
        _collider = GetComponent<Collider>();
        _rb = GetComponent<Rigidbody>();
    }

    private void Update() {
        if (_activeAnchor == null) return;

        float offset = transform.localScale.z / 2;
        transform.position = _activeAnchor.position + (offset * _activeAnchor.forward);
        //transform.rotation = _activeAnchor.rotation;
    }

    public void SetAnchor(Transform anchor) {
        _activeAnchor = anchor;

        _collider.isTrigger = _activeAnchor != null;
        _rb.useGravity = _activeAnchor == null;
        _rb.isKinematic = _activeAnchor != null;
    }
}
