using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/// <summary>
/// The parent for our football players. All of our actual
/// players inherit from this object, it contains most of
/// the attributes for players and LOADS of functions for them!
/// </summary>

[System.Serializable] // this tag tells unity that this class can be saved to a file!
public class PlayerParent 
{
    protected PlayManager playManager; // reference to the play manager!

    //_________________________________________________
    // Below properties contain base Important attributes for the player
    //_________________________________________________

    // the name of the player
    public string myName { get; set; }

    // the age of the player
    public int myAge { get; set; }

    // the height of the player
    public float myHeight { get; set; }

    // the weight of our player
    public int myWeight { get; set; }

    // the Strength of the player!(used in many multiple calculations)
    public int myStrength { get; set; }

    // the index number for this player, accessed by Plays to tell which player what to do! (assigned when created by PlayManager)
    public int myIndex { get; set; }

    //_________________________________________________
    // Below properties are related to how many actions this player can perfrom, per iteration!
    //_________________________________________________

    // the player's maximum actions
    public int mySpeed { get; set; }

    // the player's stamina 
    public int myStamina { get; set; }

    // the player's agility
    public int myAgility { get; set; }

    // the players acceleration
    public int myAcceleration { get; set; }

    // the players current fatigue level
    public int myFatigueLevel { get; set; }

    // Ability to jump in the air, used when the ball is thrown, all players on both teams can use this and their height
    // to try and catch the ball!
    public int myJumping { get; set; }

    //_________________________________________________
    // Below properties/variables are related to Plays
    //_________________________________________________

    // This is a reference to the Play the player currently has
    public ParentFootballPlay myPlay;

    // Is the player still following the play, or using standard reactive behavior
    public bool isStillFollowingPlay { get; set; }

    //_________________________________________________
    // Following properties are related to the ball
    //_________________________________________________

    // Does the player have the ball or not? (true = yes we have the ball)
    public bool hasTheBall { get; set; }

    //_________________________________________________
    // the following properties relate to the player's position, and width/length
    //_________________________________________________

    // the position of the player
    public Position myPosition { get; set; }

    // the horizontal width of the players(always an ODD NUMBER!!!)
    public int myWidth
    {
        get { return myWidth; }
        set
        {
            if (myWidth % 2 == 0)
            {
                Debug.Log("this should never happen because value should always be odd");
                if (Random.Range(1, 3) == 1)
                {
                    myWidth = value + 1;
                }
                else
                {
                    myWidth = value - 1;
                }
            }
            else
            {
                myWidth = value;
            }
        }
    }

    // the vertical length of the player(always an ODD NUMBER!!)
    // This property is a little special, it is read only, and cannot be manually set!
    public int myLength
    {
        get { return Mathf.FloorToInt(myWidth / 2); }
    }

    //_________________________________________________
    // the following properties have to do with determining If the player is blocking someone
    // being blocked by someone, covering someone, or being covered by someone!
    //_________________________________________________

    // is the playing covering another player
    public bool isCoveringPlayer { get; set; }

    // a reference to the player Being covered(null if not covering anyone)
    public PlayerParent playerIAmCovering { get; set; }

    // is this player BEING covered by antoher player
    public bool isBeingCovered { get; set; }

    // a reference to the player who is covering this player
    public PlayerParent playerWhoIsCoveringMe { get; set; }

    // is the player Blocking another player
    public bool isBLockingPlayer { get; set; }

    // a reference to the player Being blocked(null if not covering anyone)
    public PlayerParent playerIAmBlocking { get; set; }

    // is this player BEING blocked by antoher player
    public bool isBeingBlocked { get; set; }

    // a reference to the player who is blocking this player
    public PlayerParent playerWhoIsBlockingMe { get; set; }


    //_________________________________________________
    // The following propeties deal with the Durability of the player
    //_________________________________________________

    // ability to avoid injury
    public int myInjuryAvoidance { get; set; }

    // ability to recover quickly/fully from injury, also effects ability to play through
    // minor injuries, and determines the effect those injries have on their play
    public int myToughness { get; set; }


    //_________________________________________________
    // Start our functions and constructors
    //_________________________________________________

    // Constructor for our PlayerParent!
    public PlayerParent(PlayManager x, int startingRow, int startingColumn)
    {
        // instantiate a new position for us!
        myPosition = new Position(startingRow,startingColumn);
        playManager = x;
    }


    // This function gets called after each play to reset properties
    // or modify certain properties(ex tell the player he no longer has the ball)
    public virtual void ResetAfterPlay()
    {

    }


    // This is where the player actually plays the game
    public virtual void PlayFootball()
    {
        int actionsRemaining = CalculateMaximumActions(); 
        while(actionsRemaining > 0)
        {
            // do foot ball stuff here
            if(isStillFollowingPlay)
            {
                // follow the play

                // at the end check if we should stop following the play!
            }
            else
            {
                // our own standard logic for dealing with chaos!
            }
            actionsRemaining--; // decrease how many actions we have left!
        }
    }

    // this calculates our total maximum moves
    protected int CalculateMaximumActions()
    {
        int x = mySpeed - myFatigueLevel;

        return x;
    }


    // This function moves the player! IT SHOUD ONLY EVER BE CALLED
    // after checking CanIMoveHere to make sure we are allowed to go to this spot!
    protected void MoveHere(int rowChange, int columnChange)
    {
        myPosition.row += rowChange;
        myPosition.column += columnChange;
    }


    // takes a row offset and a column offset which should always be 1 or 0 or -1.
    // it then akes sure the spot is valid, and that the spot is not currently
    // being occupied by another player! While this determines whehter or not you have permission to move somewhere
    // it does not take into consideration if that spot is out of bounds(but still a valid spot)!
    protected bool CanIMoveHere(int rowChange, int columnChange)
    {
        Position newPos = new Position(myPosition.row + rowChange, myPosition.column + columnChange);

        if (playManager.IsThisPosValid(newPos, myWidth, myLength) && playManager.IsThereAPlayerInThisSpot(this, rowChange, columnChange))
        {
            return true;
        }
        else
        {
            return false;
        }
    }





}
