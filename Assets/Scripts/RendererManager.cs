using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RendererManager : MonoBehaviour {
    public static RendererManager Instance;

    private List<Renderer> renderers;

    private void Awake() {
        renderers = new List<Renderer>();
        if (Instance != null && Instance != this) {
            Destroy(this);
        }
        Instance = this;
    }

    public void RegisterRenderer(Renderer renderer) {
        if (renderers.Contains(renderer)) {
            Debug.Log("Not adding renderer of " + renderer.gameObject.name);
            return;
        }
        renderers.Add(renderer);
    }

    public void UnregsiterRenderer(Renderer renderer) {
        renderers.Remove(renderer);
    }

    public Renderer[] GetRenderers() {
        return renderers.ToArray();
    }

    public Renderer[] GetRenderersInView() {
        List<Renderer> visibleRenderers = new List<Renderer>();
        Renderer[] allRenderers = GetRenderers();
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);

        for (int i = 0; i < allRenderers.Length; i++) {
            if (GeometryUtility.TestPlanesAABB(planes, allRenderers[i].bounds)) {
                visibleRenderers.Add(allRenderers[i]);
            }
        }

        return visibleRenderers.ToArray();
    }

    public GameObject[] GetGameObjectsInView() {
        Renderer[] renderers = GetRenderersInView();
        return renderers.Select(x => x.gameObject).ToArray();
    }
}
