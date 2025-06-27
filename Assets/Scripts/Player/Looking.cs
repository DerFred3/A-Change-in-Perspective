using Unity.Hierarchy;
using UnityEngine;

public class Looking : MonoBehaviour {
    [SerializeField] private Transform _camTransform;
    [SerializeField] private GameObject _lookingAt;
    [SerializeField] private string[] _lookableLayerNames;
    [SerializeField] private Renderer _renderer;

    private LayerMask _layerMask;

    private void Awake() {
        _layerMask = LayerMask.GetMask(_lookableLayerNames);
    }

    private void Update() {
        RaycastHit hit;
        Ray ray = new Ray(_camTransform.position, _camTransform.forward);
        if (!Physics.Raycast(ray, out hit, float.MaxValue, _layerMask)) {
            return;
        }

        if (_lookingAt == null || hit.collider.gameObject != _lookingAt) {
            _lookingAt = hit.collider.gameObject;
        }
    }
}
