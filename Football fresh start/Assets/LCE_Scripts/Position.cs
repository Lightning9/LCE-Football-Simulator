using UnityEngine;
using System.Collections;

/// <summary>
/// This class is for positions on the field, it also contains
/// a few helper methods and constructors for itself!
/// </summary>

public class Position
{
    // the row of the position
    public int row { get; set; }
	
    // the column of the position
    public int column { get; set; }

    // the height of the position(Optional not often used)
    public int height { get; set; }

    // 3 different constructors for this class! (OVERLOADING UP IN HUR)
    public Position ()
    {

    }
    public Position (int x, int y)
    {
        row = x;
        column = y;
    }
    public Position(int x, int y, int z)
    {
        row = x;
        column = y;
        height = z;
    }
}
