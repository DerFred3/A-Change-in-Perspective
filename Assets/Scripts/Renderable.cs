using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Renderable : MonoBehaviour {
    private Renderer _renderer;
    private void Awake() {
        _renderer = GetComponent<Renderer>();
    }

    private void OnEnable() {
        RendererManager.Instance.RegisterRenderer(_renderer);
    }

    private void OnDisable() {
        RendererManager.Instance.UnregsiterRenderer(_renderer);
    }
}
