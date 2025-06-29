using UnityEditor;
using UnityEngine;


public class MoveCredits : MonoBehaviour {
    [SerializeField] private float _stepSize;

    private void Update() {
        Vector3 newPos = transform.position;
        newPos.y += _stepSize;
        transform.position = newPos;

        if (transform.position.y >= 2740f) {
            Application.Quit();
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#endif
        }
    }
}
