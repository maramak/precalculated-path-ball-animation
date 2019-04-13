using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour {

    [Tooltip("Camera rotation sensitivity")]
    [Range(50.0f, 500.0f)]
    public float rotationSpeed = 200.0f;

    public Transform followTarget { get; set; }

    private InputManager inputManager;
    private Transform thisTransform;

    private void Awake() {
        inputManager = InputManager.instance;
        thisTransform = transform;
    }

    private void Update() {
        if (followTarget == null) {
            return;
        }

        if (inputManager.IsRightMouseButtonPressed()) {
            ApplyRotation(inputManager.GetMouseAxis());
        }

        thisTransform.LookAt(followTarget);
    }

    public void ApplyRotation(Vector3 rotation) {
        thisTransform.RotateAround(followTarget.position, rotation, rotationSpeed * Time.deltaTime);
    }

}
