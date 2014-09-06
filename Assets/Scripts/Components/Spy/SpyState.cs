using UnityEngine;
using System.Collections;

public class SpyState : Photon.MonoBehaviour
{

    public event StealthEventTypes.Vector3Event OnSyncPositionReceived;
    public event StealthEventTypes.FloatEvent OnSyncAngleReceived;
    public event StealthEventTypes.StringEvent OnSyncPostureChanged;
    public bool inited = false;

    public Player player;
    
    // Synced properties
    public Vector3 Position;
    public float Angle;
    public Vector3 Velocity;
    public string Posture
    {
        get
        {
            return posture;
        }
        set
        {
            if (value != posture)
            {
                posture = value;
                if (photonView.isMine)
                    photonView.RPC("SetPosture", PhotonTargets.OthersBuffered, Posture);
            }
        }
    }

    #region Syncing

    public float lastSyncTime = 0f;    // Timestamp when last update received
    public float syncDelay = 0f;       // Last time between updates
    public float syncTime = 0f;        // Time since last update received
    public float SyncLerpValue
    {
        get 
        {
            if (!syncDelay.Equals(0f))
                return syncTime / syncDelay;
            else return 0f;
        }
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            // Sending
            /*
             * position
             * velocity
             * facingangle
             */
            stream.SendNext(Position);
            stream.SendNext(Velocity);
            stream.SendNext(Angle);
        }
        else
        {
            // Receiving
            Position = (Vector3)stream.ReceiveNext();
            Velocity = (Vector3)stream.ReceiveNext();
            Angle = (float)stream.ReceiveNext();

            Debug.Log("Received - P: " + Position.ToString() + " - A : " + Angle.ToString());
            
            // Fire basic movement events
            if (OnSyncPositionReceived != null)
                OnSyncPositionReceived(Position);
            
            if (OnSyncAngleReceived != null)
                OnSyncAngleReceived(Angle);
            
            // Reset interpolation variables
            syncTime = 0f;
            syncDelay = Time.time - lastSyncTime;
            lastSyncTime = Time.time;
        }
    }

    #endregion

    #region Posture
    private string posture;

    [RPC]
    public void SetPosture(string p)
    {
        Posture = p;
        if (OnSyncPostureChanged != null)
            OnSyncPostureChanged(p);
    }
    #endregion


}
