using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ExitGame : MonoBehaviour {
    private Button _button;

    private void Awake() {
        _button = GetComponent<Button>();
    }

    private void OnEnable() {
        _button.onClick.AddListener(ApplicationQuit);
    }

    private void OnDisable() {
        _button.onClick.RemoveListener(ApplicationQuit);
    }

    public void ApplicationQuit() {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
