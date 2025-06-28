using UnityEngine;

public class DeactivateOnLeave : MonoBehaviour {
    [SerializeField] private GameObject _toDeactivate;

    private void OnTriggerExit(Collider other) {
        _toDeactivate.SetActive(false);
    }
}
