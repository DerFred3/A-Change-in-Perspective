using Unity.Hierarchy;
using UnityEngine;
using UnityEngine.Events;

public class Looking : MonoBehaviour {
    [SerializeField] private Transform _camTransform;
    [SerializeField] public GameObject LookingAt;
    [SerializeField] private string[] _lookableLayerNames;

    private LayerMask _layerMask;

    [HideInInspector] public UnityEvent LookAtChanged = new UnityEvent();

    private void Awake() {
        _layerMask = LayerMask.GetMask(_lookableLayerNames);
    }

    private void Update() {
        RaycastHit hit;
        Ray ray = new Ray(_camTransform.position, _camTransform.forward);
        if (!Physics.Raycast(ray, out hit, float.MaxValue, _layerMask)) {
            return;
        }

        if (LookingAt == null || hit.collider.gameObject != LookingAt) {
            LookingAt = hit.collider.gameObject;
            LookAtChanged.Invoke();
        }
    }
}
