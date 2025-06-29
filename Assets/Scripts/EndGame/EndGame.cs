using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour {
    [SerializeField] private Canvas _blackscreen;
    [SerializeField] private float _waitTimeBeforeCredits;
    [SerializeField] private AudioSource[] _ambients;

    private void OnEnable() {
        for (int i = 0; i < _ambients.Length; i++) {
            _ambients[i].Stop();
        }
        ReferenceManager.Instance.PlayerTransform.GetComponentInChildren<Movement>().enabled = false;
        _blackscreen.enabled = true;
        StartCoroutine(PlayCredits());
    }

    private IEnumerator PlayCredits() {
        yield return new WaitForSeconds(_waitTimeBeforeCredits);
        SceneManager.LoadScene(2);
    }
}
