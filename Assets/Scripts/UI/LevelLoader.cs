using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private LevelSelectionButtonUI levelSelectionButtonPrefab;

    private void Start()
    {
        SpawnLevelButtons();
    }
    /// <summary>
    /// Spawns buttons based on data and sets the correct indexes
    /// </summary>
    private void SpawnLevelButtons()
    {
        int levelCount = GameData.instance.levelDataList.levels.Count;

        for(int i = 0; i < levelCount; i++)
        {
            LevelSelectionButtonUI levelSelectionButtonUI = Instantiate(levelSelectionButtonPrefab, transform);
            levelSelectionButtonUI.SetupLevelButton(i);
        }
    }
}
