using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO; // input/output

public class GameDataController : MonoBehaviour
{
    // public static reference to this singleton
    public static GameDataController gameDataController;

    PlayerData gameData;
    // set up our singleton!
    void Awake()
    {
        if(gameDataController == null)
        {
            gameDataController = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // saves data out to a file

    // could check if file already exists, and then merge the new data with the old data to just
    // save that! Probably would be easier since i don't wana have to re save thousands of ints each
    // time, i'd rather just save ones that changed...? is that actually better!
    public void Save(Teams[] allExistingTeams)
    {
        // create a file and push data to it
        BinaryFormatter bf = new BinaryFormatter();        //playerInfo.dat is our file name!
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");

        PlayerData data = new PlayerData(); // instantiate a new instance of our class we are going to save!
     //   data.health = this.health;
     //   data.experience = this.experience;

        data.myTeams = allExistingTeams;

        bf.Serialize(file, data); // take the serializable class data and save it to our file
        file.Close(); // Close the file now that we have saved the data to it!
    }

    // load the data 
    public void Load()
    {
        // make sure the file actually exists before we try to read data from it!

        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file); // create an object out of the data! It needs to be cast to the type of data that we have!
            file.Close();

            //      health = data.health;
            //      experience = data.experience;
            gameData = data;
        }
    }


    // delete the data
    public void Delete()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            File.Delete(Application.persistentDataPath + "/playerInfo.dat");
        }
    }


    // return all teams in our game data! (Could return null!)
    public Teams[] GetAllTeams()
    {
        return gameData.myTeams;
    }
    // return a specific team based on index, index 0 should always be the player's team!
    public Teams GetTeamWithIndex(int index)
    {
        return gameData.myTeams[index];
    }


    void OnGUI()
    {
     //   GUI.Label(new Rect(10, 10, 100, 30), "Health: " + health);
     //   GUI.Label(new Rect(10, 40, 150, 30), "Experience: " + experience);

        if(GUI.Button(new Rect(10,70,150,30), "Health up"))
        {
     //       health += 10;
        }
        if (GUI.Button(new Rect(10, 100, 150, 30), "Health down"))
        {
     //       health -= 10;
        }
        if (GUI.Button(new Rect(10, 130, 150, 30), "Experience up"))
        {
      //      experience += 10;
        }
        if (GUI.Button(new Rect(10, 160, 150, 30), "Experience down"))
        {
       //     experience -= 10;
        }
        if (GUI.Button(new Rect(10, 190, 150, 30), "Save"))
        {
            Save(new Teams[1]);
        }
        if (GUI.Button(new Rect(10, 220, 150, 30), "Load"))
        {
            Load();
        }
        if (GUI.Button(new Rect(10, 250, 150, 30), "Delete"))
        {
            Delete();
        }
    }
}


// new class which will actually store the data(can be in the same class as the class that handles
// storing data!

[Serializable] // this tag tells unity that this class can be saved to a file!
class PlayerData
{
    public Teams[] myTeams;
    //int health;
}

// my data could have an array that would hopefully store team classes and each of those classes
// would have their own array of player classes and each of those players will have all their
// stats and names stored on them! hopefully that will work!

