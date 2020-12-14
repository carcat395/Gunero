using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveGame (GameManager gm)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/GameData.gnr";
        FileStream stream = new FileStream(path, FileMode.Create);

        GameData data = new GameData(gm);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static GameData LoadGame()
    {
        string path = Application.persistentDataPath + "/GameData.gnr";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            GameData data = formatter.Deserialize(stream) as GameData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }

    public static void ResetFile(GameManager gm)
    {
        string path = Application.persistentDataPath + "/GameData.gnr";

        if (CheckSaveFile())
        {
            File.Delete(path);
            SaveGame(gm);
            Debug.Log("Reset Success");
        }
        else
        {
            Debug.LogError("Save file not found");
            return;
        }
    }

    public static bool CheckSaveFile()
    {
        string path = Application.persistentDataPath + "/GameData.gnr";
        return File.Exists(path);
    }
}
