using UnityEngine;
using System.Collections;

public class UseReplacementShader : MonoBehaviour {

    public Material replacement;

    void OnPreRender()
    {
        camera.SetReplacementShader(replacement.shader, string.Empty);
    }
}
