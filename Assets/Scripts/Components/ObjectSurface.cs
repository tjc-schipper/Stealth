using UnityEngine;
using System.Collections;

public class ObjectSurface : MonoBehaviour {

    [HideInInspector] public Vector3 start
    {
        get
        {
            return new Vector3(StartPoint.x, Floor, StartPoint.y);
        }
        set
        {
            StartPoint = new Vector2(value.x, value.z);
        }
    }
    [HideInInspector] public Vector3 end
    {
        get
        {
            return new Vector3(EndPoint.x, Floor, EndPoint.y);
        }
        set
        {
            EndPoint = new Vector2(value.x, value.z);
        }
    }
    public int Floor;

    // Connection
    public ObjectSurface ConnectedToStart;
    public ObjectSurface ConnectedToEnd;
    
    // Editor
    public Vector2 StartPoint;
    public Vector2 EndPoint;
    private Color DEBUG_planeColor = new Color32(185, 185, 255, 255);
    private static float DEBUG_gizmoHeight = 1f;
    private void DEBUG_DrawSurfacePlane()
    {
        // Along the surface
        Gizmos.DrawLine(
            start,
            end
            );
        Gizmos.DrawLine(
            start + new Vector3(0f, DEBUG_gizmoHeight, 0f),
            end + new Vector3(0f, DEBUG_gizmoHeight, 0f)
            );
        // Verticals
        Gizmos.DrawLine(
            start,
            start + new Vector3(0f, DEBUG_gizmoHeight, 0f)
            );
        Gizmos.DrawLine(
            end,
            end + new Vector3(0f, DEBUG_gizmoHeight, 0f)
            );
    }
    void OnDrawGizmos()
    {
        Gizmos.color = DEBUG_planeColor;
        DEBUG_DrawSurfacePlane();
        Gizmos.DrawWireSphere(start, 0.1f);
        Gizmos.DrawWireSphere(end, 0.1f);
        Gizmos.color = Color.white;
    }

    /// <summary>
    /// Returns a value between 0 and 1, indicatating how far along the edge the perpendicular passing through 'pos' is. In other words, how far along the surface is this position?
    /// </summary>
    /// <param name="pos">The point to projet onto the surface</param>
    /// <returns></returns>
    public float GetProgressAlong(Vector3 pos)
    {
        // Dot product of start->pos over the edge
        Vector3 edge = (end-start).normalized;
        Vector3 pointing = (pos - start);
        // Divide dot by edge magnitude and clamp
        return Mathf.Clamp01(Vector3.Dot(pointing, edge) / (end-start).magnitude);
    }
    
    /// <summary>
    /// Returns a point along the bottom edge of the surface.
    /// </summary>
    /// <param name="progress">How far along the surface</param>
    /// <returns></returns>
    public Vector3 GetPointAlong(float progress)
    {
        return Vector3.Lerp(start, end, progress);
    }
}
