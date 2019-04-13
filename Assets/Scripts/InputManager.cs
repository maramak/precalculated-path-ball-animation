using System.Collections;
using UnityEngine;

public class InputManager : MonoBehaviour {

    private static readonly string MOUSE_X_AXIS = "Mouse X";
    private static readonly string MOUSE_Y_AXIS = "Mouse Y";

    [Tooltip("Double click sensitivity (seconds)")]
    [Range(0.1f, 1.0f)]
    public float doubleClickDelay = 0.2f;

    public static InputManager instance = null;

    private float firstClickTime;
    private float timeBetweenClicks;
    private int clickCounter;
    private bool coroutineAllowed;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else if (instance == this) {
            Destroy(gameObject);
        }

        ResetDoubleClickParams();
    }

    public Vector3 GetMouseAxis() {
        return new Vector3(Input.GetAxis(MOUSE_Y_AXIS), -Input.GetAxis(MOUSE_X_AXIS), 0);
    }

    public bool IsRightMouseButtonPressed() {
        return Input.GetMouseButton(1);
    }

    public bool IsDoubleClickPerformed() {
        if (Input.GetMouseButtonDown(0)) {
            clickCounter++;

            if (clickCounter == 1 && coroutineAllowed) {
                StartCoroutine(DoubleClickDetection());
            }
        }

        return clickCounter == 2;
    }

    private IEnumerator DoubleClickDetection() {
        coroutineAllowed = false;
        firstClickTime = Time.time;

        while (Time.time < firstClickTime + doubleClickDelay && clickCounter < 2) {
            yield return null;
        }

        ResetDoubleClickParams();
    }

    private void ResetDoubleClickParams() {
        clickCounter = 0;
        firstClickTime = 0.0f;
        coroutineAllowed = true;
    }
}
