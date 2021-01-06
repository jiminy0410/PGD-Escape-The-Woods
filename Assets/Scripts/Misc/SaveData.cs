using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//stuff we want to save:
//the player position/checkpoint (basically the same): Float array of 3 floats DONE
//the stuff collected DONE
//trees destroyed? (same as stuff collected?) DONE
//double jump or not DONE
//campfires lit DONE
//the plants that have been turned on?

[System.Serializable]
public class PlayerData
{
    
    public float[] position; // this will be the position of the last checkpoint the player touched.
    
    //public bool[] plants; // if everything goes right, this wil be an enourmous list of booleans that say what plants the player has passed or not.

    public PlayerData(Checkpoint checkpoint) //save the position of the respawn point for the player here.
    {
        position = new float[3];
        position[0] = checkpoint.resetPoint.position.x;
        position[1] = checkpoint.resetPoint.position.y;
        position[2] = checkpoint.resetPoint.position.z;
    }
}

[System.Serializable]
public class ObjectData
{
    public string[] objects; // this will be a list of names of objects the player has collected.
    public ObjectData(levelState things) //save the names for all the things the player has collected (and the trees he has destroyed) here
    {
        objects = new string[things.items.Count];
        for (int i = 0; i < things.items.Count; i++)
        {
            objects[i] = things.items[i].name;
            Debug.Log("now saving object: " + things.items[i].name);
        }
    }
}

[System.Serializable]
public class SceneData
{
    public string currentScene;

    public SceneData()
    {
        currentScene = SceneManager.GetActiveScene().name;
    }
}
