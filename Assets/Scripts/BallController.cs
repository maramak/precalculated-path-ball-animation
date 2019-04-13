using UnityEngine;

[RequireComponent(typeof(BallPathLoader))]
[RequireComponent(typeof(PathRenderer))]
public class BallController : MonoBehaviour {

    [Tooltip("Maximum animanion speed")]
    [Range(1.0f, 50.0f)]
    public float maxSpeed = 10.0f;

    private InputManager inputManager;
    private Vector3[] path;

    public float currentSpeed { get; set; }
    public float savedCurrentSpeed { get; set; }
    public bool isInCameraFocus { get; set; }

    private PathRenderer pathRenderer;
    private bool isAnimating;
    private int movingToPositionIndex;
    private int pathPointsCount;
    private Vector3 animationStartPosition;
    private Vector3 animationEndPosition;
    private Vector3 movingToPosition;
    private Transform thisTransform;

    private void Awake() {
        inputManager = InputManager.instance;
        pathRenderer = GetComponent<PathRenderer>();
        thisTransform = transform;
        isInCameraFocus = false;
    }

    private void Start() {
        path = GetComponent<BallPathLoader>().GetPath();
        currentSpeed = maxSpeed / 2.0f;
        savedCurrentSpeed = currentSpeed;

        ResetBallAnimation();
    }

    private void Update() {
        if (!isAnimating || currentSpeed == 0 || thisTransform.position == path[pathPointsCount - 1]) {
            return;
        }

        if (thisTransform.position != movingToPosition) {
            thisTransform.position = Vector3.MoveTowards(thisTransform.position, movingToPosition, currentSpeed * Time.deltaTime);

            pathRenderer.UpdateLastPathPoint(thisTransform.position);
        } else {
            movingToPositionIndex = Mathf.Clamp(++movingToPositionIndex, 0, pathPointsCount - 1);
            movingToPosition = path[movingToPositionIndex];

            pathRenderer.AddPathPoint(thisTransform.position);
        }
    }

    private void OnMouseDown() {
        if (!isInCameraFocus) {
            return;
        }

        if (isAnimating && inputManager.IsDoubleClickPerformed()) {
            ResetBallAnimation();
        } else if (thisTransform.position == animationStartPosition || thisTransform.position == animationEndPosition) {
            isAnimating = true;
        }
    }

    private void ResetBallAnimation() {
        isAnimating = false;

        pathPointsCount = path.Length;
        movingToPositionIndex = 1;

        animationStartPosition = path[0];
        animationEndPosition = path[pathPointsCount - 1];
        movingToPosition = path[movingToPositionIndex];

        thisTransform.position = animationStartPosition;

        pathRenderer.Clear();
        pathRenderer.AddPathPoint(thisTransform.position);
        pathRenderer.AddPathPoint(thisTransform.position);
    }

    public Vector3[] GetPath() {
        return path;
    }
}
