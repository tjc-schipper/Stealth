using UnityEngine;
using System.Collections;

public class DEBUG_MANUALSTART : MonoBehaviour {

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            NetworkManager nman = GetComponent<NetworkManager>();
            nman.CreatePlayerObjects();
            nman.DEBUG_StartGameRound();
        }
	}
}
