using UnityEngine;
using System.Collections;

public class LightFollowCursor : MonoBehaviour
{
    public float cameraDistance = 10f;

    Vector3 lookDirection;
    Vector3 wsMousePos;

    void Update()
    {
        wsMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0f, cameraDistance, 0f));
        lookDirection = wsMousePos - transform.position;
		lookDirection.Normalize ();
    }

    void OnDrawGizmos()
    {
        Ray r = new Ray(transform.position, wsMousePos);
        Gizmos.DrawRay(r);
		Gizmos.DrawWireSphere(wsMousePos, 0.1f);
    }

    void OnGUI()
    {
        GUI.Label(new Rect(20, 20, 200, 20), "SCREEN: " + Input.mousePosition.ToString());
        GUI.Label(new Rect(20, 40, 200, 20), "WORLD: " + wsMousePos.ToString());
    }
}
