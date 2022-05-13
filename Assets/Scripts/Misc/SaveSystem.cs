using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public static class SaveSystem
{
    private static string dataType = ".lol";
    public static bool allowLoading = false;

    public static void SavePosition(Checkpoint checkpoint) //this is to save the postion of the player, so we can load it back up later
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player" + dataType;

        Debug.Log("Remembering landmark...");
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(checkpoint);

        formatter.Serialize(stream, data);
        stream.Close();

        Debug.Log("Position saved!");

    }

    public static void SaveObjects(levelState things)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/objects" + dataType;

        Debug.Log("Packing things...");
        FileStream stream = new FileStream(path, FileMode.Create);

        ObjectData data = new ObjectData(things);

        formatter.Serialize(stream, data);
        stream.Close();

        Debug.Log("Objects saved!");
    }

    public static void SaveScene()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/scene" + dataType;

        Debug.Log("Memorizing dimension...");
        FileStream stream = new FileStream(path, FileMode.Create);

        string data = SceneManager.GetActiveScene().name;

        formatter.Serialize(stream, data);
        stream.Close();

        Debug.Log("Scene saved! (The level you're in)");
    }

    public static PlayerData LoadPosition() //this is to load the position of the player back in again.
    {
        string path = Application.persistentDataPath + "/player" + dataType;
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();

            Debug.Log("Putting the boy in his place...");

            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = (PlayerData)formatter.Deserialize(stream);
            stream.Close();

            Debug.Log("Position loaded!");
            return data;
        }
        else
        {
            Debug.LogError("No idea where you're trying to go. No save data found at: " + path);
            return null;
        }
    }

    public static ObjectData LoadObjects()
    {
        string path = Application.persistentDataPath + "/objects" + dataType;
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();

            Debug.Log("Checking backpack...");

            FileStream stream = new FileStream(path, FileMode.Open);

            ObjectData data = (ObjectData)formatter.Deserialize(stream);
            stream.Close();

            Debug.Log("Objects loaded!");
            return data;
        }
        else
        {
            Debug.LogError("Looks like we lost our stuff. No save data found at: " + path);
            return null;
        }
    }

    public static string LoadScene()
    {
        string path = Application.persistentDataPath + "/scene" + dataType;
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();

            Debug.Log("Remembering Dimension");

            FileStream stream = new FileStream(path, FileMode.Open);

            string data = (string)formatter.Deserialize(stream);
            stream.Close();

            Debug.Log("Scene loaded!");
            Debug.Log("going to scene: " + data);
            return data;
        }
        else
        {
            Debug.LogError("No idea where you're trying to go. No save data found at: " + path);
            return null;
        }
    }

    public static void EraseData()
    {
        Debug.Log("Starting save data removal...");
        string path = Application.persistentDataPath + "/player" + dataType;

        // check if file exists
        if (!File.Exists(path))
        {
            Debug.LogError("can't delete nothing. No save file found at: " + path);
        }
        else
        {
            Debug.Log("Found file! Deleting" + path);

            File.Delete(path);
        }

        path = Application.persistentDataPath + "/objects" + dataType;

        // check if file exists
        if (!File.Exists(path))
        {
            Debug.LogError("can't delete nothing. No save file found at: " + path);
        }
        else
        {
            Debug.Log("Found file! Deleting" + path);

            File.Delete(path);
        }

        path = Application.persistentDataPath + "/scene" + dataType;

        // check if file exists
        if (!File.Exists(path))
        {
            Debug.LogError("can't delete nothing. No save file found at: " + path);
        }
        else
        {
            Debug.Log("Found file! Deleting" + path);

            File.Delete(path);
        }
    }
}
