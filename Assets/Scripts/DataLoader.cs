using UnityEngine;
using System.IO;

public class DataLoader : MonoBehaviour
{
    /// <summary>
    /// Loads the data from given path, sets it on GameData singleton
    /// </summary>
    public static void LoadData()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("level_data");

        if (jsonFile == null)
        {
            Debug.Log("File doesn't exist in given location!");
        }
        string jsonString = jsonFile.text;

        LevelDataList levelDataList = JsonUtility.FromJson<LevelDataList>(jsonString);

        GameData.instance.SetGameData(levelDataList);
    }
}