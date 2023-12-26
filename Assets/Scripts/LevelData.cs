using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class Point
{
    public int X;
    public int Y;

    /// <summary>
    /// Converts coordinates to match unity space
    /// </summary>
    /// <returns>converted coordinates in Vector2 form</returns>
    public Vector2 ToUnityCoordinates()
    {
        return new Vector2(X, 1000f - Y);
    }
}

[Serializable]
public class LevelData
{
    public List<string> level_data;

    /// <summary>
    /// Convers string data to points
    /// </summary>
    /// <returns>point array with x and y data</returns>
    public Point[] GetPoints()
    {
        if (level_data == null || level_data.Count % 2 != 0)
        {
            Debug.LogError("Invalid level data format.");
            return null;
        }

        Point[] points = new Point[level_data.Count / 2];
        for (int i = 0, j = 0; i < level_data.Count; i += 2, j++)
        {
            points[j] = new Point
            {
                X = Convert.ToInt32(level_data[i]),
                Y = Convert.ToInt32(level_data[i + 1])
            };
        }

        return points;
    }
}

[Serializable]
public class LevelDataList
{
    public List<LevelData> levels;
}