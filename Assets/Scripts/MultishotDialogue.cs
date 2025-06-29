using UnityEngine;

public class MultishotDialogue : MonoBehaviour {
    [SerializeField] private Dialogue[] _dialogues;

    private int idx;

    private void Awake() {
        idx = 0;
    }

    private void OnTriggerEnter(Collider other) {
        DialogueHandle handle = DialogueHandle.Instance;
        for (int i = 0; i < _dialogues[idx].Entries.Length; i++) {
            handle.EnqueueDialogue(_dialogues[idx].Entries[i]);
        }
        idx += 1;

        if (idx >= _dialogues.Length) {
            GetComponent<Collider>().enabled = false;
        }
    }
}
