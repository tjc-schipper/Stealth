using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{

    public enum PlayerTeams
    {
        SPY,
        GUARD
    }
    public PlayerTeams PlayerTeam;
    public GameObject Character;
    public string PlayerName;
    public int PhotonID;

    public Player(string name, int id, PlayerTeams team)
    {
        this.PlayerName = name;
        this.PhotonID = id;
        this.PlayerTeam = team;
    }

    void OnGUI()
    {
        if (Character != null)
        {
            Vector3 scrPos = Camera.main.WorldToScreenPoint(Character.transform.position);
            GUI.Label(new Rect(scrPos.x, scrPos.y, 200, 20), "PLAYER");
        }
    }

}
