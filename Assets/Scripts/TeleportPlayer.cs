using UnityEngine;

public class TeleportPlayer : MonoBehaviour {
    [SerializeField] private Transform _teleportDestination;
    [SerializeField] private bool _keepOrientation;
    
    private void OnTriggerStay(Collider other) {
        Transform player = ReferenceManager.Instance.PlayerTransform;
        player.position = _teleportDestination.position;
        if (!_keepOrientation) player.rotation = _teleportDestination.rotation;
    }
}
