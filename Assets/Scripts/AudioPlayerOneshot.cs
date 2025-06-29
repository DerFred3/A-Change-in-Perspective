using System.Collections;
using UnityEngine;

public class AudioPlayerOneshot : MonoBehaviour {
    private AudioSource _source;

    private void Awake() {
        _source = GetComponent<AudioSource>();
        float pitch = Random.Range(0.8f, 1.1f);
        _source.pitch = pitch;
        _source.Play();
        StartCoroutine(Cleanup());
    }

    private IEnumerator Cleanup() {
        yield return new WaitUntil(() => _source.isPlaying);
        yield return new WaitUntil(() => !_source.isPlaying);
        Destroy(gameObject);
    }
}
