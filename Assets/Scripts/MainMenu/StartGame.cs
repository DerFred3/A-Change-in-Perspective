using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour {
    private Button _button;

    private void Awake() {
        _button = GetComponent<Button>();
    }

    private void OnEnable() {
        _button.onClick.AddListener(ApplicationStart);
    }

    private void OnDisable() {
        _button.onClick.RemoveListener(ApplicationStart);
    }

    public void ApplicationStart() {
        SceneManager.LoadScene(1);
    }
}
