using UnityEngine;

public class BallPathLoader : MonoBehaviour {

    private readonly string FAIL_SAFE_PATH_DATA = "{\"x\":[0,1],\"y\":[0,1],\"z\":[0,1]}";

    [Tooltip("JSON file that contains precalculated animation path data")]
    public TextAsset pathJson;

    private BallPath rawPathData;
    private int pathPointsCount;

    private Vector3[] path;

    private void Awake() {
        if (pathJson == null) {
            Debug.Log("Path JSON must be specified.");
        }

        Init();
        path = ConvertBallPathRawData(rawPathData);
    }

    private void Init() {
        rawPathData = ExtractBallPathData(pathJson != null ? pathJson.text : FAIL_SAFE_PATH_DATA);
        ValidatePathData();
    }

    private void ValidatePathData() {
        pathPointsCount = rawPathData.x.Length;
        if (pathPointsCount < 2 || pathPointsCount != rawPathData.y.Length || pathPointsCount != rawPathData.z.Length) {
            Debug.LogError("Ball path data loaded from JSON is corrupted. Using fail-safe path data.");
            rawPathData = ExtractBallPathData(FAIL_SAFE_PATH_DATA);
            pathPointsCount = 1;
        }
    }

    private Vector3[] ConvertBallPathRawData(BallPath rawPathData) {
        Vector3[] path = new Vector3[pathPointsCount];
        for (int i = 0; i < pathPointsCount; i++) {
            path[i] = new Vector3(rawPathData.x[i], rawPathData.y[i], rawPathData.z[i]);
        }

        return path;
    }

    private BallPath ExtractBallPathData(string json) {
        return JsonUtility.FromJson(json, typeof(BallPath)) as BallPath;
    }

    public Vector3[] GetPath() {
        return path;
    }

    [System.Serializable]
    public class BallPath {

        public float[] x;
        public float[] y;
        public float[] z;

    }

}
