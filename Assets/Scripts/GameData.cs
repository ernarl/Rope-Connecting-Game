using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData instance;
    public LevelDataList levelDataList;
    public int selectedLevel;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
            DataLoader.LoadData();
        }
    }
    public void SetGameData(LevelDataList loadedData)
    {
        levelDataList = loadedData;
    }
}
