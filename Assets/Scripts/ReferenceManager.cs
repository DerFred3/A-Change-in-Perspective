using UnityEngine;

public class ReferenceManager : MonoBehaviour {
    public static ReferenceManager Instance;

    [Header("References")]
    public Transform PlayerTransform;
    public Transform CameraTransform;

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
        }

        Instance = this;
    }
}
