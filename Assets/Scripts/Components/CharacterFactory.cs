using UnityEngine;
using System.Collections;

public class CharacterFactory : Photon.MonoBehaviour
{

    public GameObject baseSpyPrefab;
    public GameObject baseGuardPrefab;

    public GameObject GetSpy()
    {
        GameObject spy = GetBaseSpy();
        spy.GetComponentInChildren<Renderer>().material.color = Color.blue;
        return spy;
    }
    private GameObject GetBaseSpy()
    {
        GameObject baseSpy = PhotonNetwork.Instantiate(baseSpyPrefab.name, Vector3.zero, Quaternion.identity, 0);
        return baseSpy;
    }

    public GameObject GetGuard()
    {
        GameObject guard = GetSpy();
        guard.GetComponentInChildren<Renderer>().material.color = Color.green;
        return guard;
    }
}
