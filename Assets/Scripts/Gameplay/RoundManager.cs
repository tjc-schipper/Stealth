using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoundManager : Photon.MonoBehaviour
{
    public event TargetZone.TargetZoneEvent HackStarted;
    public event TargetZone.TargetZoneEvent HackStopped;
    public event TargetZone.TargetZoneEvent HackPercentageChanged;
    public event TargetZone.TargetZoneEvent HackCompleted;
    
    
    
    public GameRound currentRound;
    
    [RPC]
    public void RespawnCharacter()
    {
        CharacterFactory fact = GameObject.FindObjectsOfType<CharacterFactory>()[0];
        SpawnPoint spawn = GetSpawnPoints(NetworkManager.localPlayer.PlayerTeam)[0];

        // Create character
        if (NetworkManager.localPlayer.PlayerTeam == Player.PlayerTeams.GUARD)
            NetworkManager.localPlayer.Character = fact.GetGuard();
        else
            NetworkManager.localPlayer.Character = fact.GetSpy();

        // Move to spawn point
        SpyMovement move = NetworkManager.localPlayer.Character.GetComponent<SpyMovement>();
        move.Position = spawn.transform.position;
        spawn.Available = false;
    }

    [RPC]
    public void InitRound()
    {
		ResetSpawnPoints();
        RespawnCharacter();
        currentRound = new GameRound();
        currentRound.started = true;
    }

    private void ResetSpawnPoints()
    {
        SpawnPoint[] spawns = GameObject.FindObjectsOfType<SpawnPoint>();
        foreach (SpawnPoint spawn in spawns)
            spawn.Available = true;
    }
    private List<SpawnPoint> GetSpawnPoints(Player.PlayerTeams team)
    {
        SpawnPoint[] allSpawns = GameObject.FindObjectsOfType<SpawnPoint>();
        List<SpawnPoint> spawns = new List<SpawnPoint>();
        for (int i = 0; i < allSpawns.Length; i++)
        {
            if (allSpawns[i].Team == team)
            {
                if (allSpawns[i].Available)
                {
                    spawns.Add(allSpawns[i]);
                }
            }
        }
        return new List<SpawnPoint>(spawns);
    }

    /// <summary>
    ///  Called by the (local) master client to start the round. Tells other clients to do the same
    ///  Note: players list must be intact at this point!
    /// </summary>
    /// <param name="players"></param>
    public void StartNewRound(List<Player> players)
    {
        if (PhotonNetwork.isMasterClient)
            photonView.RPC("InitRound", PhotonTargets.AllBuffered);
    }

    
    public void SetHackingPercentUpdate(TargetZone tz, float percentComplete)
    {
        photonView.RPC("OnHackPercentageChange", PhotonTargets.OthersBuffered, percentComplete);
        OnHackPercentageChange(percentComplete);
    }
    public void SetHackingStopped(TargetZone tz, float percentComplete)
    {
        photonView.RPC("OnHackStopped", PhotonTargets.OthersBuffered, percentComplete);
        OnHackStopped(percentComplete);
    }
    public void SetHackingStarted(TargetZone tz, float percentComplete)
    {
        photonView.RPC("OnHackStarted", PhotonTargets.OthersBuffered, percentComplete);
        OnHackStarted(percentComplete);
    }
    public void SetHackCompleted(TargetZone tz)
    {
        photonView.RPC("OnHackCompleted", PhotonTargets.OthersBuffered);
        OnHackCompleted();
        
    }
    [RPC]
    public void OnHackPercentageChange(float percentComplete)
    {
        if (HackPercentageChanged != null)
            HackPercentageChanged(null, percentComplete);
    }
    [RPC]
    public void OnHackStopped(float percentComplete)
    {
        if (HackStopped != null)
            HackStopped(null, percentComplete);
    }
    [RPC]
    public void OnHackStarted(float percentComplete)
    {
        if (HackStarted != null)
            HackStarted(null, percentComplete);
    }
    [RPC]
    public void OnHackCompleted()
    {
        if (HackCompleted != null)
            HackCompleted(null, 1.0f);
    }

}
