using UnityEngine;

public class OneShotDialogue : MonoBehaviour {
    [SerializeField] private Dialogue _dialogue;

    private void OnTriggerEnter(Collider other) {
        GetComponent<Collider>().enabled = false;
        DialogueHandle handle = DialogueHandle.Instance;
        for (int i = 0; i < _dialogue.Entries.Length; i++) {
            handle.EnqueueDialogue(_dialogue.Entries[i]);
        }
    }
}
