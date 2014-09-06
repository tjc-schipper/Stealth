using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NetworkManager : Photon.MonoBehaviour
{

    const string roomName = "RoomName";
    RoomInfo[] roomsList;
    public GameObject playerPrefab;
    List<Player> players;
    public static Player localPlayer;

    void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());

        if (!PhotonNetwork.connected) { }
        else if (PhotonNetwork.room == null)
        {
            // Create room
            if (GUI.Button(new Rect(100, 100, 250, 100), "Start Server"))
            {
                RoomOptions opts = new RoomOptions();
                opts.isVisible = true;
                opts.isOpen = true;
                opts.maxPlayers = 2;
                PhotonNetwork.CreateRoom(roomName + "_ThijsStuff", opts, null);
                PhotonNetwork.SetMasterClient(PhotonNetwork.player);
            }

            if (roomsList != null)
            {
                for (int i = 0; i < roomsList.Length; i++)
                {
                    if (GUI.Button(new Rect(100, 250 + (110 * i), 250, 100), "Join " + roomsList[i].name))
                        PhotonNetwork.JoinRoom(roomsList[i].name);
                }
            }
        }
    }

    // Use this for initialization
    void Awake()
    {
        players = new List<Player>();
    }

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings("0.1");
    }
    void OnReceivedRoomListUpdate()
    {
        roomsList = PhotonNetwork.GetRoomList();
    }

    void OnJoinedRoom()
    {
        // Nothing anymore? Perhaps show a waiting menu or something?

    }

    void OnPhotonPlayerConnected()
    {
        RoundManager roundManager = GameObject.FindObjectsOfType<RoundManager>()[0];

        if (PhotonNetwork.room.playerCount == PhotonNetwork.room.maxPlayers)
        {
            Debug.LogError("NumConnected: " + PhotonNetwork.room.playerCount);

            if (!(roundManager.currentRound != null && roundManager.currentRound.started))
            {
                photonView.RPC("CreatePlayerObjects", PhotonTargets.AllBuffered);
                if (PhotonNetwork.isMasterClient)
                {
                    roundManager.StartNewRound(players);
                }
            }
        }
    }

    [RPC]
    public void CreatePlayerObjects()
    {
		// Take the currently connected PhotonPlayers and create matching Player objects for them
        players.Clear();
        
        for (int i = 0; i < PhotonNetwork.playerList.Length; i++)
        {
            PhotonPlayer p = PhotonNetwork.playerList[i];

            Player.PlayerTeams team;
            if (p.isMasterClient)
            {
                team = Player.PlayerTeams.SPY;
            }
            else
            {
                team = Player.PlayerTeams.GUARD;
            }

            Player newPlayer = new Player(p.name, p.ID, team);
            if (PhotonNetwork.player == p)
                NetworkManager.localPlayer = newPlayer;
            players.Add(newPlayer);
        }
    }

    public void DEBUG_StartGameRound()
    {
        RoundManager roundManager = GameObject.FindObjectsOfType<RoundManager>()[0];
        if (!(roundManager.currentRound != null && roundManager.currentRound.started))
        {
            if (PhotonNetwork.isMasterClient)
            {
                roundManager.StartNewRound(players);
            }
        }
    }
}
