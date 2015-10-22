using UnityEngine;
using System.Collections;

/// <summary>
/// This class holds all of our ENUMS FOR THE ENTIRE GAME!!
/// </summary>

public class Enums : MonoBehaviour
{
    // covers various states for the Play(not just the play class but the entire play from start to end)
    public enum PlayState
    {
        Punting,
        Kicking_Off,
        Throwing,
        Running,
        Kicking_FieldGoal
    }
	
    // covers various states for players
    public enum PlayerState
    {
        Normal,
        Injured,
        Dazed,
        Tackled,
        Blocked,
        Covered
    }
}
