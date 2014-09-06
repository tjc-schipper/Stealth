using UnityEngine;
using System.Collections;

/// <summary>
/// This is run once when the object awakens, and configures the spy depending on if it's local or not.
/// It deletes itself after its work is done.
/// </summary>
public class InitBaseSpy : Photon.MonoBehaviour
{

    public Light omniLight;
    public Light2D flatlight;

    void Awake()
    {
        if (photonView.isMine)
        {
            gameObject.AddComponent<SpyMovement>();
            gameObject.AddComponent<SpyPostures>();
            gameObject.AddComponent<DirectionFollowMouse>();
            gameObject.AddComponent<DoHack>();
            ConfigureLight();
        }
        else
        {
            gameObject.AddComponent<SyncSpyMovement>();
            gameObject.AddComponent<SpyPostures>();
            ConfigureLight();
        }
        Destroy(this);
    }

    void ConfigureLight()
    {
        if (omniLight != null && flatlight != null)
        {
            flatlight.LightRadius = omniLight.range;
        }
    }
}
