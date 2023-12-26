using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Assign Components")]
    [SerializeField] private Transform buttonStorageTransform;

    [Header("Prefabs")]
    [SerializeField] private GameObject clickablePoint;

    [Header("GameState")]
    [SerializeField] private int clickedPointIndex = 0;
    [SerializeField] private int animatedPoints = 0;
    [SerializeField] private int lastPointIndex;

    private ConnectingPoint firstPoint;
    private List<ConnectingPoint> pointsToAnimate = new List<ConnectingPoint>();
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        SpawnLevelPoints();
    }

    /// <summary>
    /// Changes scene when level is finished
    /// </summary>
    private void OnLevelFinished()
    {
        SceneManager.LoadScene(0);
    }
    /// <summary>
    /// Checks if the pressed button can be activated
    /// </summary>
    /// <param name="sentIndex">the button index</param>
    /// <returns>true if next button can be activated</returns>
    public bool CheckIfThisIsNextIndex(int sentIndex)
    {
        if(clickedPointIndex == sentIndex)
        {
            clickedPointIndex++;
            return true;
        }
        return false;
    }

    /// <summary>
    /// Spawns buttons based on screen size, anchored from middle of the screen
    /// </summary>
    private void SpawnLevelPoints()
    {
        LevelDataList levelDataList = GameData.instance.levelDataList;

        Point[] points = levelDataList.levels[GameData.instance.selectedLevel].GetPoints();

        if (points != null)
        {
            int screenWidth = Screen.width;
            int screenHeight = Screen.height;

            int shorterSide = Mathf.Min(screenWidth, screenHeight);

            ConnectingPoint lastPoint = null;
            for (int i = 0; i < points.Length; i++)
            {
                Point point = points[i];
                Vector2 unityCoordinatesFromPoint = point.ToUnityCoordinates();
                float mappedX = Map(unityCoordinatesFromPoint.x, 0f, 1000f, 0f, shorterSide);
                float mappedY = Map(unityCoordinatesFromPoint.y, 0f, 1000f, 0f, shorterSide);

                Vector2 anchoredPosition = new Vector2(mappedX, mappedY);

                GameObject newPointGo = Instantiate(clickablePoint, buttonStorageTransform);
                ConnectingPoint connectingPoint = newPointGo.GetComponent<ConnectingPoint>();
                RectTransform rectTransform = newPointGo.GetComponent<RectTransform>();

                if (rectTransform != null)
                {
                    rectTransform.anchoredPosition = anchoredPosition;
                }

                connectingPoint.SetIndexNumber(i);

                if (lastPoint != null)
                {
                    connectingPoint.SetConnectingPoint(lastPoint);
                }
                else
                {
                    firstPoint = connectingPoint;
                }
                if (i == points.Length - 1)
                {
                    lastPointIndex = i;
                    firstPoint.SetConnectingPoint(connectingPoint);
                }
                lastPoint = connectingPoint;
            }
        }
    }

    /// <summary>
    /// Converts data value to match screen size position accordingly
    /// </summary>
    /// <param name="value">value to convert</param>
    /// <param name="fromMin">minimum value from conversion</param>
    /// <param name="fromMax">maximum value from conversion</param>
    /// <param name="toMin">minimum value to conversion</param>
    /// <param name="toMax">maximum value to conversion</param>
    /// <returns>converted value to match the screen</returns>
    private float Map(float value, float fromMin, float fromMax, float toMin, float toMax)
    {
        return (value - fromMin) * (toMax - toMin) / (fromMax - fromMin) + toMin - (toMax / 2);
    }
    /// <summary>
    /// Starts rope animation from one button to another, if one is playing add to list to animato later
    /// </summary>
    /// <param name="pointToAnimate">button to animate</param>
    public void StartRopeAnimation(ConnectingPoint pointToAnimate)
    {
        pointsToAnimate.Add(pointToAnimate);
        if(pointsToAnimate.Count == 1)
        {
            pointToAnimate.PlayRopeAnimation();
        }

        if (pointToAnimate.GetIndex() == lastPointIndex)
            pointsToAnimate.Add(firstPoint);
    }
    /// <summary>
    /// Removes already animated button from list and animates another if the list is not empty, if its last button finishes the level
    /// </summary>
    public void OnFinishRopeAnimation()
    {
        pointsToAnimate.RemoveAt(0);
        if (pointsToAnimate.Count > 0)
            pointsToAnimate[0].PlayRopeAnimation();
        if (animatedPoints++ == lastPointIndex)
            Invoke("OnLevelFinished", 1.5f);
    }
}
