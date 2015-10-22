using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// This is the Manager for running plays in the simulation. It makes players
/// and gives them information to help them decide what to do. It also
/// helps recording and loading stats and attributes. Important to remember
/// 1 yard is equal to 46 spaces!and our field is 100 yards long and 53 yards wide!
/// </summary>

public class PlayManager 
{
    // reference to the gameManager
    GameManager _myGameManager { get; set; }

    //_________________________________________________
    // The following properties define the maximum and minimum values for
    // moving on our "field" and also the maximum and miniumum values for
    // moving on our field without being out of bounds, or in the endzone
    //_________________________________________________

    // the smallest row value you can have and still be on the field!
    const int minimumOnFieldRow = 0;

    // the largest row value you can have and still be on the field
    const int maximumOnFieldRow = 5520;


    // the smallest column value you can have and still be on the field
    const int minimumOnFieldColumn = 0;

    // the largest column value you can have and still be on the field
    const int maximumOnFieldColumn = 2714;


    // the smallest row value you can have and not be in an ENDZONE
    const int minimumNotInEndZoneRow = 460;

    // the largest row value you can have and not be in an ENDZONE
    const int maximumNotInEndZoneRow = 5060;


    // the smallest column value you can have and NOT be OUT OF BOUNDS (3 yards of out of bounds play available on both ends)
    const int minimumNotOutOfBoundsColumn = 138;

    // in the largest column value you can have and NOT be OUT OF BOUNDS(3 yards of out of bounds play available on both ends)
    const int maximumNotOutOfBoundsColumn = 2576;


    //_________________________________________________
    // The following properties deal with actual game data, what down is it, is the offense moving up? etc
    //_________________________________________________

    // indicates if the offense is moving up(true), or down(false). moivng up means to go towards the endzone your ROW
    // must INCREASE. while moving down means you get closer to the endzone as your row DECREASES!
    bool offenseMovingUp;

    // are we on the 1st, 2nd, 3rd, of 4th down?
    int currentDown;



    //_________________________________________________
    // The following properties have to do with plays
    //_________________________________________________

    // an array of every different play we can run
    ParentFootballPlay[] allPossiblePlays;

    // the play we are currently running
    ParentFootballPlay currentPlay;


    //_________________________________________________
    // The following properties have to do with PLAYERS
    //_________________________________________________

    // an array holding all of the players who are currently actively playing the game on our field!
    PlayerParent[] allPlayersOnField;


    //_________________________________________________
    // The following properties have to do with the ball
    //_________________________________________________

    // the position the ball is currently at
    Position ballPosition;

    PlayerParent playerWithTheBall;

    bool aPlayerCurrentlyHasTheBall;





    //_________________________________________________
    // Start our functions and constructors
    //_________________________________________________

    // Constructor for this class!
    public PlayManager(GameManager x)
    {
        _myGameManager = x;
    }


    // a function to instantiate each play and put each one
    // into our allPossiblePlays array
    public void CreateAllPlays()
    {
        Debug.Log("Created plays called, does nothing currently");
    }

    // a function to instantiate 7/11(depending on version) players from each team
    // depending on whether they are on Offense/Defense and which players are the
    // best on the team!
    public void CreateAllPlayers()
    {
        Debug.Log("Created all PLAYERS, does nothing currently");
    }


    // a function that returns an array of all the players, except for the
    // player passed to it as a paramater
    public PlayerParent[] GetAllOtherPlayers(PlayerParent y)
    {
        PlayerParent[] temp = new PlayerParent[21];
        for(int i = 0; i < allPlayersOnField.Length; i++)
        {
            if(allPlayersOnField[i] != y)
            {
                temp[i] = allPlayersOnField[i];
            }
        }
        return temp;
    }

    // a function to get a player based on what index number they are!
    public PlayerParent GetPlayer(int index)
    {
        return allPlayersOnField[index];
    }



    // a function to check if a player is out of bounds. By out of bounds
    // I mean is it still a valid position, but is it in the parts of
    // the field that we assume to be out of bounds.
    public bool IsThisPosInBounds(Position pos, int width)
    {
        if(pos.column - ((width - 1) / 2) >= minimumNotOutOfBoundsColumn && pos.column + ((width - 1) / 2) <= maximumNotOutOfBoundsColumn &&
            pos.column - ((width - 1) / 2) >= minimumOnFieldColumn && pos.column + ((width - 1) / 2) <= maximumOnFieldColumn)
        {
            return true;
        }
        else
        {
            return true;
        }
    }

    // a function to check if a player is NOT in a valid position.
    // if not that means the player can absolutely not move to that position for any
    // reason!
    public bool IsThisPosValid(Position pos, int width, int length)
    {
        if(pos.column - ((width - 1) / 2) >= minimumOnFieldColumn && pos.column + ((width - 1) / 2) <= maximumOnFieldColumn &&
            pos.row - ((length - 1) /2) >= minimumOnFieldRow && pos.row + ((length - 1) / 2) <= maximumOnFieldRow)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // a function that returns true if a player could move to the
    // position passed to this without being inside of another 
    // player! row and column offset should always be equal to
    // 1,0, or 1   !!!!!!
    public bool IsThereAPlayerInThisSpot(PlayerParent x, int rowOffset, int columnOffset)
    {
        Position pos = new Position(x.myPosition.row + rowOffset, x.myPosition.column + columnOffset);
        int width = ((x.myWidth - 1) / 2);
        int length = ((x.myLength - 1) / 2);

        PlayerParent[] temp = GetAllOtherPlayers(x);
        for(int i = 0; i < temp.Length; i++)
        {
            Position pos1 = temp[i].myPosition;
            int nWidth = ((temp[i].myWidth - 1) / 2);
            int nLength = ((temp[i].myLength - 1) / 2);

            // check if a player is inside of pos, if so return false
            if ((pos.column >= pos1.column && pos.column - width <= pos1.column + nWidth) || (pos.column + width >= pos1.column - nWidth))
            {
                // the players horizontally are touching, so we also have to check rows to see if they are actually in the same place
                if ((pos.row >= pos1.row && pos.row - width <= pos1.row + nWidth)|| pos.row + width >= pos1.row - nWidth)
                {
                    return false;
                } 
            }           
        }
        return true;
    }


    // return true if a player has entered the endzone they are trying to get to!
    public bool AmIInTheEndZone(Position pos)
    {
        if(pos.column >= minimumNotOutOfBoundsColumn && pos.column <= maximumNotOutOfBoundsColumn)
        {
            if(offenseMovingUp)
            {
                // are we in the top endzone? if so we scored!
                if (pos.row > maximumNotInEndZoneRow && pos.row <= maximumOnFieldRow)
                {
                    return true;
                } 
            }
            else
            {
                // are we in the bottom endzone? if so we scored!
                if(pos.row < minimumNotInEndZoneRow && pos.row >= minimumOnFieldRow)
                {
                    return true;
                }
            }
        }
        return false;
    }





}
