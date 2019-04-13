using UnityEngine;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour {

    [Tooltip("Link to UI text component that will indicate on which ball camera is focused")]
    public Text activeBallLable;

    [Tooltip("Array of balls")]
    public BallController[] ballControllers;

    [Tooltip("Material that will be applied to the ball which camera is focused on")]
    public Material activeBallMaterial;

    [Tooltip("Material applied to the ball that is not in camera focus")]
    public Material inactiveBallMaterial;

    private CameraController cameraController;
    private Slider speedControllingSlider;

    private int ballControllersCount;
    private int activeBallControllerIndex = 0;
    private BallController activeBallController;

    private Material ballMaterial;

    private void Awake() {
        if (ballControllers.Length == 0) {
            Debug.Log("At leasn one ball must be prepent.");
        }

        ballControllersCount = ballControllers.Length;
        activeBallController = ballControllers[activeBallControllerIndex];

        cameraController = Camera.main.GetComponent<CameraController>();

        speedControllingSlider = FindObjectOfType<Slider>();
        print(speedControllingSlider.name);
    }

    private void Start() {
        ActivateBall(activeBallControllerIndex);
    }

    private void RestoreSpeedControllingSliderValue() {
        speedControllingSlider.value = activeBallController.savedCurrentSpeed / activeBallController.maxSpeed;
    }

    private void ActivateBall(int index) {
        activeBallController.savedCurrentSpeed = activeBallController.currentSpeed;
        activeBallController.currentSpeed = 0.0f;
        activeBallController.isInCameraFocus = false;
        activeBallController.GetComponent<MeshRenderer>().material = inactiveBallMaterial;

        activeBallController = ballControllers[index];
        RestoreSpeedControllingSliderValue();
        activeBallController.currentSpeed = activeBallController.savedCurrentSpeed;
        activeBallController.isInCameraFocus = true;
        cameraController.followTarget = activeBallController.transform;
        activeBallController.GetComponent<MeshRenderer>().material = activeBallMaterial;

        Vector3 camRelativePos = cameraController.transform.localPosition;
        cameraController.transform.parent = activeBallController.transform;
        cameraController.transform.localPosition = camRelativePos;

        activeBallLable.text = activeBallController.name + " of " + ballControllersCount;
    }

    public void SetNextBallController() {
        if (activeBallControllerIndex < ballControllersCount - 1) {
            ActivateBall(++activeBallControllerIndex);
        }
    }

    public void SetPreviuosBallController() {
        if (activeBallControllerIndex > 0) {
            ActivateBall(--activeBallControllerIndex);
        }
    }

    public void SetActiveBallCurrentSpeed(float value) {
        activeBallController.currentSpeed = activeBallController.maxSpeed * value;
    }
}
