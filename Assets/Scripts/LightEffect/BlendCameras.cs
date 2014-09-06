#pragma strict

using UnityEngine;
using System.Collections;

/// <summary>
/// This component allows for blending together of multiple camera renders, 
/// using a 'mask shader' that specifies how the two images should be combined.
/// Attach this to the main camera and assign the 'second camera' to this component.
/// </summary>
public class BlendCameras : MonoBehaviour {

    public Camera secondCamera;
    public Material blendMaterial;

    void OnPreRender()
    {
        // Make sure the second camera has rendered before we complete this one
        secondCamera.Render();
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        // First copy the main camera image to the screen
        Graphics.Blit(source, destination);
        // Then copy over the second camera's result over, using the blendMaterial as a 'mask shader'
        Graphics.Blit(secondCamera.targetTexture, destination, blendMaterial);
    }
}
