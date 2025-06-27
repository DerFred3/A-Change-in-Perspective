using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class CollisionForwarding : MonoBehaviour {
    [HideInInspector] public UnityEvent<Collision> CollisionEntered = new UnityEvent<Collision>();
    [HideInInspector] public UnityEvent<Collision> CollisionStayed = new UnityEvent<Collision>();
    [HideInInspector] public UnityEvent<Collision> CollisionExited = new UnityEvent<Collision>();

    [HideInInspector] public UnityEvent<Collider> TriggerEntered = new UnityEvent<Collider>();
    [HideInInspector] public UnityEvent<Collider> TriggerStayed = new UnityEvent<Collider>();
    [HideInInspector] public UnityEvent<Collider> TriggerExited = new UnityEvent<Collider>();

    private void OnCollisionEnter(Collision collision) {
        CollisionEntered.Invoke(collision);
    }

    private void OnCollisionStay(Collision collision) {
        CollisionStayed.Invoke(collision);
    }

    private void OnCollisionExit(Collision collision) {
        CollisionExited.Invoke(collision);
    }

    private void OnTriggerEnter(Collider other) {
        TriggerEntered.Invoke(other);
    }

    private void OnTriggerStay(Collider other) {
        TriggerStayed.Invoke(other);
    }

    private void OnTriggerExit(Collider other) {
        TriggerExited.Invoke(other);
    }
}
