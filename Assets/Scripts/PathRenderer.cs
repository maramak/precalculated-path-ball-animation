using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class PathRenderer : MonoBehaviour {

    private LineRenderer lr;

    private void Awake() {
        lr = GetComponent<LineRenderer>();
        Clear();
    }

    public void UpdateLastPathPoint(Vector3 pos) {
        lr.SetPosition(lr.positionCount - 1, pos);
    }

    public void AddPathPoint(Vector3 pos) {
        lr.positionCount++;
        lr.SetPosition(lr.positionCount - 1, pos);
    }

    public void Clear() {
        lr.positionCount = 0;
    }

}
