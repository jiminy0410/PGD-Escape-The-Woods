﻿using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    private static string dataType = ".lol";

    public static void SavePosition(Checkpoint checkpoint) //this is to save the postion of the player, so we can load it back up later
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player" + dataType;

        Debug.Log("Finding landmark...");
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(checkpoint);

        formatter.Serialize(stream, data);
        stream.Close();

        Debug.Log("Position saved!");

    }

    public static PlayerData loadPlayer() //this is to load the position of the player back in again.
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
            Debug.LogError("save file not found. there's no such thing as: " + path);
            return null;
        }
    }
}