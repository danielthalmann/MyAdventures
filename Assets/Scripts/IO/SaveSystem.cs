using System.Collections;
using System.IO;
using UnityEngine;

public class SaveSystem
{
    private static SaveData saveData = new SaveData();

    public static string SaveFileName()
    {
        string saveFile = Application.persistentDataPath + "/save" + ".save";
        return saveFile;
    }

    public static void Save()
    {
        HandleSaveData();

        File.WriteAllText(SaveFileName(), JsonUtility.ToJson(saveData, true));
    }

    private static void HandleSaveData()
    {
        GameManager.instance.Save(ref saveData);

    }

    public static void Load()
    {
        string saveContent = File.ReadAllText(SaveFileName());

        saveData = JsonUtility.FromJson<SaveData>(saveContent);

        HandleLoadData();

    }

    private static void HandleLoadData()
    {
        GameManager.instance.Load(saveData);
    }

}