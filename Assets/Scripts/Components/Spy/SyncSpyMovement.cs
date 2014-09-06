using UnityEngine;
using System.Collections;

public class SyncSpyMovement : Movement
{

    // Used for lerping
    private Vector3 startPos;
    private Vector3 endPos;
    private float startAngle;
    private float endAngle;
    private const float lerpFactor = 0.5f;

    void Start()
    {
        state.OnSyncPositionReceived += SyncPositionHandler;
        state.OnSyncAngleReceived += SyncAngleHandler;
        state.inited = true;

        // Reset values to prevent weird start lerps
        startPos = Position;
        endPos = Position;
        startAngle = Angle;
        endAngle = Angle;
    }
    void OnDestroy()
    {
        state.OnSyncPositionReceived -= SyncPositionHandler;
        state.OnSyncAngleReceived -= SyncAngleHandler;
    }

    void Update()
    {
        Vector3 newPos = Vector3.Lerp(
            Position,
            endPos,
            lerpFactor
            );
        Position = newPos;

        float newAngle = Mathf.Lerp(
            Angle,
            endAngle,
            lerpFactor
            );
        Angle = newAngle;
    }

    private void SyncPositionHandler(Vector3 p)
    {
        if (p.IsNaN())
        {
            Debug.LogError("Invalid/NaN position vector synced. Ignoring.");
        }
        else
        {
            endPos = p;
            startPos = Position;
            //Debug.Log("Updated: endpos=" + endPos.ToString() + " - startpos=" + startPos.ToString());
        }
    }

    private void SyncAngleHandler(float a)
    {
        endAngle = a;
        startAngle = Angle;
    }

    void Awake()
    {
        state = GetComponent<SpyState>();
    }
}
