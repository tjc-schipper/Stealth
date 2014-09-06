using UnityEngine;
using System.Collections;

public class TargetZone : MonoBehaviour
{

    public event TargetZoneEvent HackCompleted;
    public event TargetZoneEvent HackStopped;
    public event TargetZoneEvent HackStarted;
    public delegate void TargetZoneEvent(TargetZone tz, float percent);

    public Player playerHacking;
    bool _beingHacked;
    public bool beingHacked
    {
        get { return _beingHacked; }
        set
        {
            if (_beingHacked != value)
            {
                _beingHacked = value;
                tickTimer = 0f;
                if (value)
                {
                    if (HackStarted != null) HackStarted(this, hackProgress);
                    DEBUG_ChangeModelColor(true);
                }
                else
                {
                    if (HackStopped != null) HackStopped(this, hackProgress);
                    DEBUG_ChangeModelColor(false);
                }
            }
        }
    }
    public float hackProgress = 0f;
    private bool hackCompleted = false;

    public float timeBetweenHackTicks = 1f;
    private float lossStepSize = 0.1f;
    private float tickTimer = 0f;

    void Update()
    {
        // Decreasing if not being hacked
        if (!hackCompleted && !beingHacked && hackProgress > 0f)
        {
            tickTimer += Time.deltaTime;
            if (tickTimer >= timeBetweenHackTicks)
            {
                hackProgress -= lossStepSize;
                tickTimer = 0f;
            }
        }
        if (beingHacked)
        {
            if (hackProgress >= 1f)
            {
                if (HackCompleted != null) HackCompleted(this, hackProgress);
            }
        }
    }

    void DEBUG_ChangeModelColor(bool hacking)
    {
        if (hacking)
        {
            renderer.material.color = new Color32(200, 220, 255, 100);
        }
        else
        {
            renderer.material.color = new Color32(255, 220, 200, 100);
        }
    }

}
