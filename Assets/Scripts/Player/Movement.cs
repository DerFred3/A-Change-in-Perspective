using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour {
    // Apply gravity change by rotating by 180 degrees

    [SerializeField] private float _playerHeight;

    [Header("Settings")]
    [SerializeField] private float _playerStandStrength;
    [SerializeField] private float _playerStandDamping;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _maxMovementSpeed;
    [SerializeField] private float _lookingPitchSpeed;
    [SerializeField] private float _lookingYawSpeed;
    [SerializeField] private float _lookingUpMax;
    [SerializeField] private float _lookingDownMax;

    [Header("References")]
    [SerializeField] private Transform _camTransform;
    [SerializeField] private InputActionReference _actionMove;
    [SerializeField] private InputActionReference _actionLook;

    private Rigidbody _rb;
    private int _groundLayer;

    private void Awake() {
        _rb = GetComponent<Rigidbody>();
        _groundLayer = LayerMask.GetMask(new string[] { "Ground" });
    }

    private void OnEnable() {
        _actionMove.action.Enable();
        _actionLook.action.Enable();
    }

    private void OnDisable() {
        _actionMove.action.Disable();
        _actionLook.action.Disable();
    }

    private void Update() {
        HandleLooking();
    }

    private void FixedUpdate() {
        HandleGroundOffset();
        HandleMovement();
    }

    private void HandleGroundOffset() {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, -transform.up);
        if (!Physics.Raycast(ray, out hit, float.MaxValue, _groundLayer)) {
            return;
        }

        float offset = (hit.point.y + (hit.normal.normalized.y * _playerHeight)) - transform.position.y;
        float dampingForce = Vector3.Project(_rb.linearVelocity, ray.direction.normalized).y * _playerStandDamping;
        float appliedForce = offset * _playerStandStrength - dampingForce;
        _rb.AddForce(Vector3.up * appliedForce, ForceMode.Impulse);
    }

    private void HandleMovement() {
        Vector2 movement = _actionMove.action.ReadValue<Vector2>();
        float sideways = movement.x;
        float forward = movement.y;

        Vector3 movementForce = Vector3.zero;
        Vector3 localVelocity = transform.InverseTransformDirection(_rb.linearVelocity);

        sideways *= Mathf.Clamp01(1f - Mathf.Abs(localVelocity.x) / _maxMovementSpeed);
        forward *= Mathf.Clamp01(1f - Mathf.Abs(localVelocity.z) / _maxMovementSpeed);

        movementForce += transform.right * sideways;
        movementForce += transform.forward * forward;
        movementForce *= _movementSpeed;

        if (movement.x == 0f) {
            _rb.AddForce(-transform.right * localVelocity.x, ForceMode.Impulse);
        }

        if (movement.y == 0f) {
            _rb.AddForce(-transform.forward * localVelocity.z, ForceMode.Impulse);
        }

        _rb.AddForce(movementForce, ForceMode.Impulse);
    }

    private void HandleLooking() {
        Vector2 looking = _actionLook.action.ReadValue<Vector2>();
        float yaw = looking.x;
        float pitch = looking.y;

        Vector3 newRotation = transform.localEulerAngles;
        newRotation.y += yaw * _lookingYawSpeed;
        transform.localEulerAngles = newRotation;

        Vector3 newCamRotation = _camTransform.eulerAngles;
        newCamRotation.x += -pitch * _lookingPitchSpeed;
        Mathf.Clamp(newCamRotation.x, -_lookingDownMax, _lookingUpMax);
        _camTransform.eulerAngles = newCamRotation;
    }
}
