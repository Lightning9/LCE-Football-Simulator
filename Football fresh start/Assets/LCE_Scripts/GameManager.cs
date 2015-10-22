using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// This is the GameManager for our game. It is in charge
/// of making sure everything runs correctly!
/// </summary>

public class GameManager : MonoBehaviour
{
    PlayManager _playManager;

    Teams[] allTeams;
	// Use this for initialization
	void Start ()
    {
        // should this be here?
        _playManager = new PlayManager(this);

        // try to get saved data!
        attemptToGetLoadedData();
        if(allTeams == null)
        {
            // this means that we have no game data, most likely this is because it is our first time launching the game.
            // alternatievely someone could have delete their game data or it could be some horrible error i'm not dealing with.
            // ... the point is we need to actually create our teams and players!

            // call a method to create teams!
        }
	}

    void attemptToGetLoadedData()
    {
        GameDataController.gameDataController.Load();
        allTeams = GameDataController.gameDataController.GetAllTeams();
    }
	
	
}
