using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Canvas))]
public class DialogueHandle : MonoBehaviour {
    public static DialogueHandle Instance;

    [Header("Settings")]
    [SerializeField] private float _delayBetweenCharacters;
    [SerializeField] private float _delayBeforeClear;
    [SerializeField] private float _delayBetweenDialogues;

    [Header("References")]
    [SerializeField] private TextMeshProUGUI _textUI;

    private Queue<string> dialogueQueue = new Queue<string>();
    private Coroutine _workerCoroutine;
    private Canvas _canvas;

    private const int MAX_DISPLAY_LENGTH = 128;
    private const float DIALOGUE_FETCH_IDLE_TIME = 1f;

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
        }
        Instance = this;

        _canvas = GetComponent<Canvas>();
    }

    private void Start() {
        _workerCoroutine = StartCoroutine(WorkQueue());
    }

    public void EnqueueDialogue(string dialogue) {
        dialogueQueue.Enqueue(dialogue);
    }

    private IEnumerator WorkQueue() {
        while (true) {
            if (dialogueQueue.Count == 0) {
                _canvas.enabled = false;
                _textUI.text = "";
                yield return new WaitForSeconds(DIALOGUE_FETCH_IDLE_TIME);
                continue;
            }

            _canvas.enabled = true;
            string currentItem = dialogueQueue.Dequeue();
            string[] currentItemWords = currentItem.Split(' ');
            _textUI.text = "";
            for (int i = 0; i < currentItemWords.Length; i++) {
                if (_textUI.text.Length + currentItemWords[i].Length + 1 > MAX_DISPLAY_LENGTH - 3) {
                    _textUI.text += ".";
                    yield return new WaitForSeconds(_delayBetweenCharacters);
                    _textUI.text += ".";
                    yield return new WaitForSeconds(_delayBeforeClear);
                    _textUI.text = "";
                    _textUI.text += ".";
                    yield return new WaitForSeconds(_delayBetweenCharacters);
                    _textUI.text += ".";
                    yield return new WaitForSeconds(_delayBetweenCharacters);
                }
                for (int j = 0; j < currentItemWords[i].Length; j++) {
                    _textUI.text += currentItemWords[i][j];
                    yield return new WaitForSeconds(_delayBetweenCharacters);
                }
                _textUI.text += " ";
                yield return new WaitForSeconds(_delayBetweenCharacters);
            }

            yield return new WaitForSeconds(_delayBetweenDialogues);
        }
    }
}
