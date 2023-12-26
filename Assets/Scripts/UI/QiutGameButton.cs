using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QiutGameButton : MonoBehaviour
{
    /// <summary>
    /// Qiuts the game on button click
    /// </summary>
    private void Awake()
    {
        Button button = gameObject.GetComponent<Button>();

        button.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }
}
