using UnityEngine;
using System.Collections;

public class DoHack : MonoBehaviour {

    RoundManager roundManager;
    TargetZone hackingZone;
    float hackingSpeed = 0.1f;

    float tickTimer = 0f;

    void Awake()
    {
        roundManager = GameObject.FindObjectOfType<RoundManager>();
        if (roundManager == null)
            Debug.LogError("No RoundManager found for DoHack component!");
    }

    void OnTriggerEnter(Collider other)
    {
        TargetZone zone = other.GetComponent<TargetZone>();
        if (zone != null)
        {
            /*if (!zone.beingHacked)
            {*/
                RegisterAsHacker(zone);
            /*}*/
        }
    }

    void OnTriggerExit(Collider other)
    {
        TargetZone zone = other.GetComponent<TargetZone>();
        if (zone != null)
        {
            if (zone == hackingZone)
            {
                UnRegisterAsHacker(zone);
            }
        }
    }

    void Update()
    {
        if (hackingZone != null)
        {
            // We're currently hacking
            tickTimer += Time.deltaTime;
            if (tickTimer >= hackingZone.timeBetweenHackTicks)
            {
                // Add a tick
                hackingZone.hackProgress += hackingSpeed;
                tickTimer = 0f;
                OnHackTick();
            }
        }
    }

    void RegisterAsHacker(TargetZone zone)
    {
        zone.HackCompleted += OnHackCompleted;
        zone.HackStopped += OnHackStopped;
        zone.HackStarted += OnHackStarted;
        hackingZone = zone;
        zone.playerHacking = NetworkManager.localPlayer;
        zone.beingHacked = true;
    }

    void UnRegisterAsHacker(TargetZone zone)
    {
        zone.HackCompleted -= OnHackCompleted;
        zone.HackStopped -= OnHackStopped;
        zone.HackStarted -= OnHackStarted;
        hackingZone = null;
        zone.beingHacked = false;
        zone.playerHacking = null;
    }

    // Forward these onto RoundManager so the rest gets synced as well!
    void OnHackStopped(TargetZone tz, float percent)
    {
        roundManager.OnHackStopped(percent);
    }
    void OnHackCompleted(TargetZone tz, float percent)
    {
        roundManager.OnHackCompleted();
    }
    void OnHackStarted(TargetZone tz, float percent)
    {
        roundManager.OnHackStarted(percent);
    }
    void OnHackTick()
    {
        roundManager.OnHackPercentageChange(hackingZone.hackProgress);
    }
}
