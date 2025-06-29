using UnityEngine;

public class TeleportPlayer : MonoBehaviour {
    [SerializeField] private Transform _teleportDestination;
    [SerializeField] private bool _keepOrientation;
    
    private void OnTriggerStay(Collider other) {
        Transform player = ReferenceManager.Instance.PlayerTransform;
        Vector3 triggerToPlayer = player.position - transform.position;
        player.position = _teleportDestination.position + triggerToPlayer;
        if (!_keepOrientation) player.rotation = _teleportDestination.rotation;
    }
}
