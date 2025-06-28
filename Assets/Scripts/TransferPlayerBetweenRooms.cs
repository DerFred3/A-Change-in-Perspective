using System;
using System.Collections;
using UnityEngine;

public class TransferPlayerBetweenRooms : MonoBehaviour {
    [SerializeField] private Transform _teleportRoomReference;
    [SerializeField] private Collider _trigger;
    [SerializeField] private Collider _destination;

    private Vector3 _directionToRoom;

    private void Awake() {
        _directionToRoom = _teleportRoomReference.position - _trigger.transform.position;
        _directionToRoom.y = 0f;
    }

    private void OnTriggerEnter(Collider other) {
        Transform playerTransform = ReferenceManager.Instance.PlayerTransform;
        Vector3 triggerToPlayer = playerTransform.position - transform.position;

        Quaternion triggerToPlayerRotation = Quaternion.FromToRotation(-transform.forward, playerTransform.forward);
        //Quaternion triggerToDestinationRotation = Quaternion.FromToRotation(_trigger.transform.rotation, );

        float sideOfTrigger = Vector3.Dot(triggerToPlayer, _directionToRoom);
        if (sideOfTrigger > 0f) {
            // Entering the room
            playerTransform.position = _destination.transform.position + triggerToPlayer;
            playerTransform.rotation = _destination.transform.rotation * triggerToPlayerRotation;
        }
    }
}
