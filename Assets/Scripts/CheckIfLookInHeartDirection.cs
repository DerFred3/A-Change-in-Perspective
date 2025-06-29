using UnityEngine;

public class CheckIfLookInHeartDirection : MonoBehaviour {
    [SerializeField] private Transform _fromDirection;
    [SerializeField] private Transform _toDirection;
    [SerializeField] private Dialogue _lookAtHeart;

    private void OnTriggerStay(Collider other) {
        Transform camTransform = ReferenceManager.Instance.CameraTransform;

        if (Vector3.Dot(camTransform.forward, _toDirection.position - _fromDirection.position) > 0) {
            // One-shot dialogue
            GetComponent<Collider>().enabled = false;
            DialogueHandle handle = DialogueHandle.Instance;
            for (int i = 0; i < _lookAtHeart.Entries.Length; i++) {
                handle.EnqueueDialogue(_lookAtHeart.Entries[i]);
            }
        }
    }
}