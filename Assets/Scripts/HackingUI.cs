using UnityEngine;
using System.Collections;

public class HackingUI : MonoBehaviour {

    public RoundManager roundManager
    {
        get
        {
            return GameObject.FindObjectOfType<RoundManager>();
        }
    }
    bool showHackingBar = false;
    bool showHackingComplete = false;
    float progress;

    // Use this for initialization
	void Start () {
        roundManager.HackCompleted += (tz, p) =>
        {
            showHackingBar = false;
            showHackingComplete = true;
        };
        roundManager.HackStarted += (tz, p) =>
        {
            progress = p;
            showHackingBar = true;
        };
        roundManager.HackStopped += (tz, p) =>
        {
            progress = p;
            showHackingBar = false;
        };
        roundManager.HackPercentageChanged += (tz, p) =>
        {
            progress = p;
        };
	}

    void OnGUI()
    {
        if (showHackingBar)
            GUI.Label(new Rect(64, 64, 300, 20), "Zone is being hacked: " + progress.ToString() + "%!");
        if (showHackingComplete)
            GUI.Label(new Rect(64, 84, 300, 20), "Hack complete! Spies win!");
    }
}
