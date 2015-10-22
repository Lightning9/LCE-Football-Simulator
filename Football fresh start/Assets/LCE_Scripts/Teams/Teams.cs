using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// This class stores while be instantiated for each team, and stores an array of ALL the players on the team!
/// </summary>

[System.Serializable] // this tag tells unity that this class can be saved to a file! 
public class Teams 
{
    // a list of all the players on this team!
    public List<PlayerParent> playersOnThisTeam;

    // the name of the team!
    public string teamName;

    // Make an object to store GameData(win/loss, scores, which team they fought, season, etc).

    // store info about the team's Manager,coach, scouter, etc...

    // constructor!
	public Teams()
    {
        playersOnThisTeam = new List<PlayerParent>();
    }

    // get how many players are on this team!
    public int GetHowManyPlayersAreOnTheTeam()
    {
        return playersOnThisTeam.Count;
    }
}
