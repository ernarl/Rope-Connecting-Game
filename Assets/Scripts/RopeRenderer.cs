using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeRenderer : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();      
    }

    /// <summary>
    /// Sets two line renderer points for rope animation
    /// </summary>
    /// <param name="startingPoint">rope start point</param>
    /// <param name="endingPoint">rope end point</param>
    public void SetRopePoints(Vector3 startingPoint, Vector3 endingPoint)
    {
        lineRenderer.positionCount = 2;

        lineRenderer.SetPosition(0, startingPoint);
        lineRenderer.SetPosition(1, endingPoint);
    }

    /// <summary>
    /// Starts playing rope animation
    /// </summary>
    public void PlayAnimation()
    {
        StartCoroutine(LineDraw());
    }

    /// <summary>
    /// Draws a line betweens first two line points
    /// </summary>
    IEnumerator LineDraw()
    {
        Vector3 orig = lineRenderer.GetPosition(0);
        Vector3 orig2 = lineRenderer.GetPosition(1);

        float t = 0;
        float time = Vector3.Distance(orig, orig2) / 4;

        lineRenderer.SetPosition(1, orig);
        Vector3 newpos;
        for (; t < time; t += Time.deltaTime)
        {
            newpos = Vector3.Lerp(orig, orig2, t / time);
            lineRenderer.SetPosition(1, newpos);
            yield return null;
        }
        lineRenderer.SetPosition(1, orig2);
        GameManager.instance.OnFinishRopeAnimation();
    }
}
