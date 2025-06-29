using System.Collections;
using UnityEngine;

public class FadeInAudio : MonoBehaviour {
    [SerializeField] private float _fadeInTime;
    [SerializeField] private float _volume;

    private AudioSource _source;
    
    private void Awake() {
        _source = GetComponent<AudioSource>();
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn() {
        _source.volume = 0f;
        _source.Play();
        while (_source.volume < _volume) {
            yield return new WaitForSeconds(0.01f);
            _source.volume += _volume / (100f * _fadeInTime);
        }
    }
}
