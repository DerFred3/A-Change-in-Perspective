using UnityEngine;

public class ActivateColliderWhenPlayerLooking : MonoBehaviour {
    [SerializeField] private GameObject _lookAtCondition;
    [SerializeField] private Collider _colliderToToggle;

    private void OnEnable() {
        ReferenceManager.Instance.PlayerTransform.GetComponent<Looking>().LookAtChanged.AddListener(CheckCondition);
    }

    private void OnDisable() {
        ReferenceManager.Instance.PlayerTransform.GetComponent<Looking>().LookAtChanged.RemoveListener(CheckCondition);
    }

    private void CheckCondition() {
        _colliderToToggle.enabled = ReferenceManager.Instance.PlayerTransform.GetComponent<Looking>().LookingAt == _lookAtCondition;
    }
}
