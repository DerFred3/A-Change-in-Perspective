using UnityEngine;

[RequireComponent(typeof(Collider))]
public class AllowOneWay : MonoBehaviour {
    [Tooltip("Do not allow passing through from the `inside`")]
    [SerializeField] private Transform _relativeInside;
    [SerializeField] private Collider _colliderToToggle;

    private void OnTriggerEnter(Collider other) {
        ToggleCollider(other);
    }

    private void OnTriggerExit(Collider other) {
        ToggleCollider(other);
    }
    
    private void ToggleCollider(Collider _) {
        Transform playerTransform = ReferenceManager.Instance.PlayerTransform;
        Vector3 triggerToPlayer = playerTransform.position - transform.position;

        Vector3 directionToInside = _relativeInside.position - playerTransform.position;
        directionToInside.y = 0f;
        directionToInside = directionToInside.normalized;
        float sideOfTrigger = Vector3.Dot(triggerToPlayer, directionToInside);
        if (sideOfTrigger > 0f) {
            _colliderToToggle.isTrigger = false;
        } else {
            _colliderToToggle.isTrigger = true;
        }
    }
}
