using UnityEngine;

public class PlayerStencilTransfer : MonoBehaviour {
    [SerializeField] private CollisionForwarding _trigger;
    [SerializeField] private GameObject _lookAtCondition;
    [SerializeField] private Transform _objectStart;
    [SerializeField] private Transform _objectDestination;

    private void Start() {
        _trigger.TriggerEntered.AddListener(TryTransfer);
    }

    private void TryTransfer(Collider other) {
        Transform playerTransform = ReferenceManager.Instance.PlayerTransform;
        if (playerTransform.GetComponent<Looking>().LookingAt.name != _lookAtCondition.name) return;

        Vector3 distanceToObject = playerTransform.position - _objectStart.position;
        distanceToObject *= _objectDestination.localScale.x / _objectStart.localScale.x;
        playerTransform.position = _objectDestination.position + distanceToObject;
    }
}
