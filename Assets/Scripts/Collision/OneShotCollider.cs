using UnityEngine;

[RequireComponent(typeof(Collider))]
public class OneShotCollider : MonoBehaviour {
    [SerializeField] private bool _onEnter = true;
    
    private Collider _col;

    private void Awake() {
        _col = GetComponent<Collider>();
    }

    public void ActivateCollider() {
        _col.enabled = true;
    }

    private void DisableCollider() {
        _col.enabled = false;
    }

    private void OnCollisionEnter(Collision collision) {
        if (!_onEnter) return;
        DisableCollider();
    }

    private void OnTriggerEnter(Collider other) {
        if (!_onEnter) return;
        DisableCollider();
    }

    private void OnCollisionExit(Collision collision) {
        if (_onEnter) return;
        DisableCollider();
    }

    private void OnTriggerExit(Collider other) {
        if (_onEnter) return;
        DisableCollider();
    }
}
