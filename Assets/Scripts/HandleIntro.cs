using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HandleIntro : MonoBehaviour {
    [SerializeField] private Image _blackscreen;
    [SerializeField] private Dialogue _dialoguesWhileFade;
    [SerializeField] private Dialogue _dialoguesAfterFade;
    [SerializeField] private AudioSource _ambientSound;
    [SerializeField] private AudioSource _ambientSound2;

    private Movement _playerMovement;

    private UnityEvent IntroDialogEnded = new UnityEvent();
    private UnityEvent FadeCompleted = new UnityEvent();

    private void Awake() {
        Cursor.visible = false;

        _playerMovement = ReferenceManager.Instance.PlayerTransform.GetComponent<Movement>();

        _blackscreen.color = Color.black;
        _playerMovement.enabled = false;

        IntroDialogEnded.AddListener(OnIntroDialogEnded);
        FadeCompleted.AddListener(OnFadeCompleted);

        StartCoroutine(IntroDialog());
    }

    private IEnumerator IntroDialog() {
        yield return new WaitForSeconds(1f);

        DialogueHandle handle = DialogueHandle.Instance;
        for (int i = 0; i < _dialoguesWhileFade.Entries.Length; i++) {
            handle.EnqueueDialogue(_dialoguesWhileFade.Entries[i]);
        }
        yield return new WaitUntil(() => handle.IsDisplaying);
        yield return new WaitUntil(() => !handle.IsDisplaying);
        IntroDialogEnded.Invoke();
    }

    private void OnIntroDialogEnded() {
        StartCoroutine(FadeBlackscreen());
        StartCoroutine(FadeAmbientSound());
    }

    private IEnumerator FadeBlackscreen() {
        while (_blackscreen.color.a >= 0) {
            yield return new WaitForSeconds(0.01f);
            Color newColor = _blackscreen.color;
            newColor.a -= 1f / 200f;
            _blackscreen.color = newColor;
        }

        FadeCompleted.Invoke();
    }

    private IEnumerator FadeAmbientSound() {
        _ambientSound.volume = 0f;
        _ambientSound.Play();
        _ambientSound2.volume = 0f;
        _ambientSound2.Play();

        while (_ambientSound.volume < 1f) {
            yield return new WaitForSeconds(0.01f);
            _ambientSound.volume += 0.6f / 200f;
            _ambientSound2.volume += 0.4f / 200f;
        }
    }

    private void OnFadeCompleted() {
        _playerMovement.enabled = true;

        DialogueHandle handle = DialogueHandle.Instance;
        for (int i = 0; i < _dialoguesAfterFade.Entries.Length; i++) {
            handle.EnqueueDialogue(_dialoguesAfterFade.Entries[i]);
        }
    }
}
