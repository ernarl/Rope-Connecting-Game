using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectionButtonUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private int levelIndex;
    private void Awake()
    {
        Button button = GetComponent<Button>();

        button.onClick.AddListener(() => {           
            LoadLevel();
        });
    }

    /// <summary>
    /// Loads the level based on set index
    /// </summary>
    private void LoadLevel()
    {
        GameData.instance.selectedLevel = levelIndex;
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// Setups the level button, sets index and text
    /// </summary>
    /// <param name="index">button index</param>
    public void SetupLevelButton(int index)
    {
        SetLevelIndex(index);
        SetLevelText((index + 1).ToString());
    }

    /// <summary>
    /// Sets the level index on button
    /// </summary>
    /// <param name="index">level index</param>
    private void SetLevelIndex(int index)
    {
        levelIndex = index;
    }

    /// <summary>
    /// Sets the level text for the button
    /// </summary>
    /// <param name="text">text for button</param>
    private void SetLevelText(string text)
    {
        levelText.text = text;
    }
}
