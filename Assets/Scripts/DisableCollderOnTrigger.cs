using UnityEngine;

public class DisableCollderOnTrigger : MonoBehaviour {
    [SerializeField] private Collider _trigger;
    [SerializeField] private Collider _collider;
    [SerializeField] private MeshRenderer _renderer;

    private void OnTriggerEnter(Collider other) {
        _collider.enabled = false;
        _renderer.enabled = false;
    }

    private void OnTriggerExit(Collider other) {
        _collider.enabled = true;
        _renderer.enabled = true;
    }
}
