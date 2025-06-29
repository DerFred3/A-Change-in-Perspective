using UnityEngine;

public class CamMovement : MonoBehaviour {
    [SerializeField] private Transform _rotateAround;
    [SerializeField] private float _offset;
    [SerializeField] private float _speedMultipler;

    private Vector3 _initialPosition;

    private void Start() {
        _initialPosition = transform.position;
    }

    private void Update() {
        // Cos(0) = 1;
        // Cos(1/2 pi) = 0;
        // Cos(pi) = -1;
        // Cos(3/2 pi) = 0;
        // Cos(2pi) = 1;

        float t = Time.time * _speedMultipler;
        Vector3 newPos = _rotateAround.position + new Vector3(Mathf.Cos(t + Mathf.PI * 0.5f) * _offset, 0f, Mathf.Cos(t) * _offset);
        newPos.y = _initialPosition.y;
        transform.position = newPos;

        transform.LookAt(_rotateAround);
    }
}
