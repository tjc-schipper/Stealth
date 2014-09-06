using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GameRound : MonoBehaviour
{
    public List<Player> players;
    public int guardScore;
    public int spyScore;
    public bool ended = false;
    public bool started = false;
    
    void OnGUI()
    {
        if (started && !ended)
        {
            GUI.Label(new Rect(16, 16, 100, 16), "PLAYING!");
        }
        else if (ended)
        {
            string winningTeam = (spyScore > guardScore) ? "Spies" : "Guards";
            GUI.Label(new Rect(16, 40, 100, 16), winningTeam + " win!");
        }
    }
}
