using System.IO;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int highScore;
    public int lastScore;
    public string[] unlockedWeapons;
}

public static class SaveSystem
{
    public static void Save(SaveData data, string fileName = "save_game.json")
    {
        string json = JsonUtility.ToJson(data, true);
        string path = Path.Combine(Application.persistentDataPath, fileName);
        File.WriteAllText(path, json);
        Debug.Log($"Saved game to: {path}");
    }

    public static SaveData Load(string fileName = "save_game.json")
    {
        string path = Path.Combine(Application.persistentDataPath, fileName);
        if (!File.Exists(path))
        {
            Debug.Log("Save file not found: " + path);
            return null;
        }
        string json = File.ReadAllText(path);
        SaveData data = JsonUtility.FromJson<SaveData>(json);
        Debug.Log($"Loaded save from: {path}");
        return data;
    }
}