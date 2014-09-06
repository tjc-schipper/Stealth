using UnityEngine;
using System.Collections;

public static class ExtensionMethods {

    public static bool IsNaN(this Vector3 v)
    {
       return double.IsNaN(v.x) || double.IsNaN(v.y) || double.IsNaN(v.z);
    }
}
